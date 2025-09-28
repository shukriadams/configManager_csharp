namespace ConfigManager
{ 
    public class ConfigStateChange
    {
    /// <summary>
    /// When the config change happened
    /// </summary>
    public DateTime OccurredUtc { get; set; }

    /// <summary>
    /// Hash of config object
    /// </summary>
    public string Hash { get; set; }

    /// <summary>
    /// If state change failed, error description
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// Unique of config, if config provider supports. Git for example supports tag or commit hash.
    /// Else is the same as hash.
    /// </summary>
    public string UID { get; set; }
    }
};