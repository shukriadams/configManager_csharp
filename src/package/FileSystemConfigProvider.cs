namespace ConfigManager
{
     public class FileSystemConfigProvider : IConfigProvider
    {

        /// <summary>
        /// Path to config file on some locally accessible file system.
        /// </summary>
        public string Path { get; set; }

        public string IsValid()
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