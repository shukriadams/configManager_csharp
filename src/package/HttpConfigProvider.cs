namespace ConfigManager
{ 
    public class HttpConfigProvider : IConfigProvider{

        /// <summary>
        /// Path to download config string from
        /// </summary>
        public string Url { get; set; }

        public string IsValid()
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
}