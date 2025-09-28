namespace ConfigManager
{
    /// <summary>
    /// Logs errors, including failed config updates from providers
    /// </summary>
    /// <param name="error"></param>
    public delegate void LogError(string error, object arg);
}