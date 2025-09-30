//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Shukri Adams (contact@shukriadams.com)
/// https://github.com/shukriadams/configManager_csharp
/// MIT License (MIT) Copyright (c) 2025 Shukri Adams   
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Timers;

namespace ConfigManager
{
    public class ConfigManager<T>
    {
        #region FIELDS

        private readonly IConfigProvider _configProvider;

        private readonly string _cacheLocation;

        private System.Timers.Timer _timer;

        private int _interval;

        private bool _busy;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Optional event hook for config changes
        /// </summary>
        public EventHandler OnConfigUpdate;

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
        public void Start(int interval)
        {
            if (_timer != null)
                _timer.Stop();

            _interval = interval;

            _timer = new System.Timers.Timer();
            _timer.Elapsed += new ElapsedEventHandler(PollUpdate);
            _timer.Interval = interval;
            _timer.Enabled = true;
            _timer.Start();
        }

        public void Stop()
        {
            if (_timer == null)
                return;

            _timer.Stop();
            _timer = null;
        }

        private void PollUpdate(object source, ElapsedEventArgs e)
        {
            try
            {
                if (_busy)
                    return;

                _busy = true;

                _configProvider.Update();
            }
            finally
            {
                _busy = false;
            }
        }

        /// <summary>
        /// Bypasses period update and updates immediately.
        /// </summary>
        public void ForceUpdate()
        {
            _configProvider.Update();

            if (_interval != 0)
                this.Start(_interval);
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
            ConfigStateChange change = new ConfigStateChange { }; // get from log

            return new ConfigResponse<T>
            {
                Change = change
            };
        }

        #endregion
    }
}
