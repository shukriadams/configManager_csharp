//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Shukri Adams (contact@shukriadams.com)
/// https://github.com/shukriadams/configManager_csharp
/// MIT License (MIT) Copyright (c) 2025 Shukri Adams   
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace ConfigManager
{ 
    public class ConfigManager<T>
    {
        #region FIELDS

        private readonly IConfigProvider _configProvider;

        private readonly string _cacheLocation;

        #endregion

        #region CTORS

        public ConfigManager(IConfigProvider configProvider,
            string cacheLocation,
            LogError logError,
            LogStatus logStatus,
            bool logVerbose)
        {
            _configProvider = configProvider;
            _cacheLocation = cacheLocation;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Returns true if setup is valid
        /// </summary>
        /// <returns></returns>
        public string ValidateSetup()
        {
            if (!Directory.Exists(_cacheLocation))
                return $"Directory for cache location {_cacheLocation} does not exist.";

            return _configProvider.ValidateSetup();
        }

        /// <summary>
        /// Returns true if setup can be used to round trip against store.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public string ValidateReachable(int timeout)
        {
            return _configProvider.ValidateReachable(timeout);
        }

        /// <summary>
        /// Starts the config store's internal background thread. Store will peridiocally update itself based on its defined source.
        /// To control the update cycle directly, call ForceUpdate() from your own timer.
        /// </summary>
        public void Start()
        {

        }

        /// <summary>
        /// Bypasses period update and updates immediately.
        /// </summary>
        public void ForceUpdate()
        {
            _configProvider.Update();
        }

        /// <summary>
        /// Returns null if config is currently valid. Else returns the last failed config. use this to determine 
        /// cause for failed config changes.
        /// </summary>
        /// <returns></returns>
        public ConfigStateChange GetCurrentFailingState()
        {
            return null;
        }

        /// <summary>
        /// Gets currently good configuarion. Returns null if config has never been in a working state. This is what you want to call 
        /// </summary>
        /// <returns></returns>
        public ConfigResponse<T> GetCurrentGoodConfig()
        {
            string rawConfig = _configProvider.Read();
            ConfigStateChange change = new ConfigStateChange { }; // get from log
            
            return new ConfigResponse<T>
            {
                Change = change
            };
        }

        #endregion
    }
}
