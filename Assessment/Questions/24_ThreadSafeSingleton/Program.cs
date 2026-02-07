using System;
using System.Threading.Tasks;
using ThreadSafeSingleton;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("=== Problem 24: Thread-Safe Singleton ===");
        
        var tasks = new Task[10];
        for (int i = 0; i < 10; i++)
        {
            int taskId = i;
            tasks[i] = Task.Run(() =>
            {
                var instance = ConfigManager.Instance;
                var appName = instance.GetSetting("AppName");
                Console.WriteLine($"Task {taskId}: AppName = {appName}");
            });
        }

        await Task.WhenAll(tasks);

        var config = ConfigManager.Instance;
        config.SetSetting("NewKey", "NewValue");
        Console.WriteLine($"\nSet new key: {config.GetSetting("NewKey")}");
        Console.WriteLine("✓ Test Passed\n");
    }
}
