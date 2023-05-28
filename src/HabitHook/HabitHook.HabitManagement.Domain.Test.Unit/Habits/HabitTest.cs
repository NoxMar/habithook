using HabitHook.HabitManagement.Domain.Contracts.Habits.AddHabit;

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
}