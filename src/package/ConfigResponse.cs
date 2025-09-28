namespace ConfigManager
{ 
    public class ConfigResponse<T>
    {
        public IEnumerable<ConfigFile> Items { get; set; }

        public ConfigStateChange Change { get; set; }
    }
}

