//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Single-file mode for ConfigManager. Add this to your project, create an instance of type Store with required config,
/// and start it. Then call GetConfig() to get the most up-to-date version of your config, source from whatever source
/// you set.
/// 
/// Shukri Adams (contact@shukriadams.com)
/// https://github.com/shukriadams/configManager_csharp
/// MIT License (MIT) Copyright (c) 2025 Shukri Adams   
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace ConfigManager;

public class Store<T>
{
    #region FIELDS

    private readonly DeserializeConfig<T> _configDeserializer;

    private readonly IConfigProvider _configProvider;

    private readonly string _cacheLocation;

    #endregion

    #region CTORS

    public Store(DeserializeConfig<T> configDeserializer,
        IConfigProvider configProvider,
        string cacheLocation,
        LogError logError,
        LogStatus logStatus,
        bool logVerbose)
    {
        _configDeserializer = configDeserializer;
        _configProvider = configProvider;
        _cacheLocation = cacheLocation;
    }

    #endregion

    #region METHODS

    /// <summary>
    /// Returns true if 
    /// </summary>
    /// <returns></returns>
    public string ValidateSetup()
    {
        if (!Directory.Exists(_cacheLocation))
            return $"Directory for cache location {_cacheLocation} does not exist.";

        return _configProvider.ValidateSetup();
    }

    public string ValidateReachable(int timeout)
    {
        return _configProvider.ValidateReachable(timeout);
    }

    public void Start()
    {

    }

    public void ForceUpdate()
    {
       
    }

    /// <summary>
    /// Returns null if config is currently valid. Else returns 
    /// </summary>
    /// <returns></returns>
    public ConfigStateChange GetCurrentFailingState()
    {
        return null;
    }

    /// <summary>
    /// Gets currently available configuarion
    /// </summary>
    /// <returns></returns>
    public ConfigResponse<T> GetConfig()
    {
        string rawConfig = _configProvider.Read();
        T config = _configDeserializer.Invoke(rawConfig);
        ConfigStateChange change = new ConfigStateChange { }; // get from log
        
        return new ConfigResponse<T>
        {
            Config = config,
            Change = change
        };
    }

    #endregion
}

public class ConfigResponse<T>
{
    public T Config { get; set; }

    public ConfigStateChange Change { get; set; }
}

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

/// <summary>
/// A function which when called, converts raw config into the config Type managed by this system.
/// </summary>
/// <param name="rawConfig"></param>
/// <returns></returns>
public delegate T DeserializeConfig<T>(string rawConfig);

/// <summary>
/// Logs errors, including failed config updates from providers
/// </summary>
/// <param name="error"></param>
public delegate void LogError(string error, object arg);

/// <summary>
/// Logs status based on verbosity. Will always log out config updates. In verbose mode
/// will log out scan attempts etc.
/// </summary>
/// <param name="signal"></param>
public delegate void LogStatus(string signal);

public interface IConfigProvider
{
    string Read();

    void Update();

    bool IsValid();

    /// <summary>
    /// Call to ensure basic configuration of provider is valid. Returns a null string if config valid, else
    /// string contains description of error. If not not null, config providing is not possible.
    /// </summary>
    string ValidateSetup();

    /// <summary>
    /// Tries to validate provider endpoint is reachable within given timeout. Returns empty string if reachable
    /// or description of error if not.
    /// </summary>
    /// <param name="timeout"></param>
    /// <returns></returns>
    string ValidateReachable(int timeout);
}

/// <summary>
/// 
/// </summary>
public enum GitConfigUpdateStrategies
{
    Commit,
    Tag
}

public class GitConfigProvider : IConfigProvider
{
    public string Remote { get; init; }

    public string Branch { get; set; }

    public string CheckoutPath { get; set; }

    /// <summary>
    /// Path within checkout path location to read config file from.
    /// </summary>
    public string LocalFilePath { get; set; }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public string Read()
    {
        return string.Empty;
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public string ValidateReachable(int timeout)
    {
        throw new NotImplementedException();
    }

    public string ValidateSetup()
    {
        throw new NotImplementedException();
    }
}


public class HttpConfigProvider : IConfigProvider{

    /// <summary>
    /// Path to download config string from
    /// </summary>
    public string Url { get; set; }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public string Read() {
        return string.Empty;
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public string ValidateReachable(int timeout)
    {
        throw new NotImplementedException();
    }

    public string ValidateSetup()
    {
        throw new NotImplementedException();
    }
}

public class FileSystemConfigProvider : IConfigProvider
{

    /// <summary>
    /// Path to config file on some locally accessible file system.
    /// </summary>
    public string Path { get; set; }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    public string Read()
    {
        return string.Empty;
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public string ValidateReachable(int timeout)
    {
        throw new NotImplementedException();
    }

    public string ValidateSetup()
    {
        throw new NotImplementedException();
    }
}