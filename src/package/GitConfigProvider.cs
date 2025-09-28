namespace ConfigManager
{
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
}