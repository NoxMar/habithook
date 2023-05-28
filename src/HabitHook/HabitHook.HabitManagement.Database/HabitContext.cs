using System.Linq.Expressions;
using HabitHook.Common.Services;
using HabitHook.Domain.Common;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HabitHook.HabitManagement.Database;

public class HabitContext : DbContext
{
    private readonly IMediator _mediator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICurrentUserService _currentUserService;
    
    public HabitContext(DbContextOptions<HabitContext> options, IMediator mediator, IDateTimeProvider dateTimeProvider, ICurrentUserService currentUserService) : base(options)
    {
        _mediator = mediator;
        _dateTimeProvider = dateTimeProvider;
        _currentUserService = currentUserService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.FilterSoftDeletedRecords();
        
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateAuditFields();
        var result = await base.SaveChangesAsync(cancellationToken);
        await _dispatchDomainEvents();
        return result;
    }
    
    private async Task _dispatchDomainEvents()
    {
        var domainEventEntities = ChangeTracker.Entries<BaseEntity>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();

        foreach (var entity in domainEventEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.DomainEvents.Clear();
            foreach (var entityDomainEvent in events)
                await _mediator.Publish(entityDomainEvent);
        }
    }

    private void UpdateAuditFields()
    {
        var now = _dateTimeProvider.DateTimeUtcNow;
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.UpdateCreationProperties(now, _currentUserService.UserId);
                    entry.Entity.UpdateModifiedProperties(now, _currentUserService.UserId);
                    break;
                
                case EntityState.Modified:
                    entry.Entity.UpdateModifiedProperties(now, _currentUserService.UserId);
                    break;
                
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.UpdateModifiedProperties(now, _currentUserService.UserId);
                    entry.Entity.UpdateIsDeleted(true);
                    break;
            }
        }
    }
}

public static class Extensions
{
    public static void FilterSoftDeletedRecords(this ModelBuilder modelBuilder)
    {
        Expression<Func<BaseEntity, bool>> notDeletedFilter = e => !e.IsDeleted;
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                     .Where(e => e.ClrType.IsAssignableTo(typeof(BaseEntity))))
        {
            var parameter = Expression.Parameter(entityType.ClrType);
            var body = ReplacingExpressionVisitor
                .Replace(notDeletedFilter.Parameters.First(), parameter, notDeletedFilter.Body);
            var lambdaExpression = Expression.Lambda(body, parameter);

            // set filter
            entityType.SetQueryFilter(lambdaExpression);
        }
    }
}