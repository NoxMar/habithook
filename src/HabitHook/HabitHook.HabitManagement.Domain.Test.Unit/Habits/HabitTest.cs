using HabitHook.HabitManagement.Domain.Core.Habits.DeleteHabit;

namespace HabitHook.HabitManagement.Domain.Test.Unit.Habits;

using Fakers;

public class HabitTest
{
    [Fact]
    public void Create_ValidData_ReturnValidCreatedHabit()
    {
        // Arrange
        var forCreation = new FakeHabitForCreation().Generate();
        
        // Act
        var habit = Habit.Create(forCreation);
        
        // Assert
        habit.Name.Should().Be(forCreation.Name);
        habit.TargetValue.Should().Be(forCreation.TargetValue);
    }

    [Fact]
    public void Delete__ShouldCreateHabitDeletedDomainEventAsLastEvent()
    {
        // Arrange
        var sut = Habit.Create(new FakeHabitForCreation().Generate());

        // Act
        sut.Delete();

        // Assert
        var lastEvent = sut.DomainEvents.LastOrDefault();
        lastEvent.Should().NotBeNull();
        lastEvent.Should().BeOfType<HabitDeleted>();
    }
    
    [Fact]
    public void Delete__ShouldCreateHabitDeletedDomainEventWithCorrectHabit()
    {
        // Arrange
        var sut = Habit.Create(new FakeHabitForCreation().Generate());

        // Act
        sut.Delete();

        // Assert
        var lastEvent = (HabitDeleted)sut.DomainEvents.LastOrDefault()!;
        lastEvent.Habit.Should().BeEquivalentTo(sut);
    }
}