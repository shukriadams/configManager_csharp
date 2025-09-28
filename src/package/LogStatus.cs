namespace ConfigManager
{

    /// <summary>
    /// Logs status based on verbosity. Will always log out config updates. In verbose mode
    /// will log out scan attempts etc.
    /// </summary>
    /// <param name="signal"></param>
    public delegate void LogStatus(string signal);
}