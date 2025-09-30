using YamlDotNet.Serialization;

namespace ConfigManager;

/// <summary>
/// Some trivial config to test on
/// </summary>
public class MyConfig
{
    public string Name { get; set; }
}

/// <summary>
/// Test program to run from. Don't include this in your project.
/// </summary>
public class Program
{

    static void Main(string[] args)
    {
        // set up data directory for Store, it needs a place to keep its own copy 
        string cacheDirectory = "./settings_cache";
        Directory.CreateDirectory(cacheDirectory);

        // init the store, this is where the magic happens.
        ConfigManager<MyConfig> store = new ConfigManager<MyConfig>(
            configProvider: new GitConfigProvider
            {
                Remote = "<REMOTE FROM ENV FILE HERE>",
                Branch = "master",
            },
            cacheLocation: cacheDirectory,
            logError: new LogError(LogError),
            logStatus: new LogStatus(LogStatus),
            logVerbose: true);

        // handle initialize errors if any
        string configErrors = store.ValidateSetup();
        if (configErrors != null)
        {
            Console.WriteLine(configErrors);
            System.Environment.Exit(1);
        }

        string contactErrors = store.ValidateReachable(timeout: 5000);
        if (contactErrors != null)
        {
            Console.WriteLine(contactErrors);
            System.Environment.Exit(1);
        }

        // start the store, it will run on its own thread and do its thing
        Console.WriteLine("Watching for config changes. Use CTRL-C to exit.");
        store.Start(5000);

        // loop forever and check config updates. Changes made to the config available
        // in provider should appear here, including errors and fixes.
        while (true)
        {
            ConfigStateChange failingChange = store.GetCurrentFailingState();
            if (failingChange != null)
            {
                Console.WriteLine($"Last config change failed: {failingChange.Error}");
            }

            Thread.Sleep(5000);
        }
    }

    private static void LogError(string error, object arg)
    {
        if (arg == null)
            Console.WriteLine(error);
        else
            Console.WriteLine(error, arg);
    }

    private static void LogStatus(string status)
    {
        Console.WriteLine(status);
    }
}    
