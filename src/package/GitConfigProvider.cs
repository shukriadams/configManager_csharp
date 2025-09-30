using ConfigManager.Porter_Packages.MadScience_Shell;

namespace ConfigManager
{
    public class GitConfigProvider : IConfigProvider
    {
        private string _pluginWorkDir = "git_checkout";

        private string _historyDir = "status";

        public string Remote { get; init; }

        public string Branch { get; set; }

        public string IsValid()
        {
            // ensure git is available
            Shell shell = new Shell("git");
            int result = shell.Run();
            if (result != 0)
                return $"git likely not installed ({shell.Err})";

            return string.Empty;
        }

        public string Read()
        {
            return string.Empty;
        }

        public void Update()
        {
            if (Directory.Exists(_pluginWorkDir))
            {
                Shell shell = new Shell("git reset --hard && git clean -dfx && git pull");
                shell.WorkingDirectory = _pluginWorkDir;
            }
            else
            {
                // git clone
                Shell shell = new Shell($"git clone {Remote} {_pluginWorkDir}");
                int result = shell.Run();
                if (result != 0)
                    Console.WriteLine($"{shell.Err}");
            }

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