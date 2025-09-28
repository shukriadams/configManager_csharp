namespace ConfigManager
{
    /// <summary>
    /// A function which when called, converts raw config into the config Type managed by this system.
    /// </summary>
    /// <param name="rawConfig"></param>
    /// <returns></returns>
    public delegate T DeserializeConfig<T>(string rawConfig);
}