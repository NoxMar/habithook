namespace HabitHook.Common.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
}

public class MockCurrentUserService : ICurrentUserService
{
    public string UserId  => "example@example.com";
}