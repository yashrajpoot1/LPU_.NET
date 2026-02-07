using System.Reflection;

namespace PluginLoaderSystem
{
    public interface IPlugin
    {
        string Name { get; }
        void Execute();
    }

    public class PluginLoader
    {
        public List<IPlugin> LoadPlugins(string folderPath)
        {
            var plugins = new List<IPlugin>();

            if (!Directory.Exists(folderPath))
                return plugins;

            var dllFiles = Directory.GetFiles(folderPath, "*.dll");

            foreach (var dllFile in dllFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dllFile);
                    var pluginTypes = assembly.GetTypes()
                        .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    foreach (var type in pluginTypes)
                    {
                        var plugin = Activator.CreateInstance(type) as IPlugin;
                        if (plugin != null)
                        {
                            plugins.Add(plugin);
                        }
                    }
                }
                catch (Exception)
                {
                    // Skip invalid DLLs
                }
            }

            return plugins;
        }
    }
}
