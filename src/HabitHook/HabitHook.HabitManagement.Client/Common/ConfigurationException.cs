namespace HabitHook.HabitManagement.Client.Common;

public class ConfigurationException : Exception
{
    public ConfigurationException(string message) : base(message)
    {
    }

    public ConfigurationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}