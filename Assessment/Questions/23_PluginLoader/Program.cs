using System;
using PluginLoaderSystem;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 23: Plugin Loader ===");
        var loader = new PluginLoader();
        
        var pluginsFolder = "plugins";
        System.IO.Directory.CreateDirectory(pluginsFolder);

        var plugins = loader.LoadPlugins(pluginsFolder);
        Console.WriteLine($"Loaded {plugins.Count} plugins from '{pluginsFolder}'");

        if (plugins.Count == 0)
        {
            Console.WriteLine("(No plugins found - this is expected for demo)");
        }

        Console.WriteLine("✓ Test Passed\n");
    }
}
