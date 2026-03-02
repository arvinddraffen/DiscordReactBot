using Discord.Interactions;
using Discord.WebSocket;
using DiscordReactBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var socketConfig = new DiscordSocketConfig
{
    GatewayIntents = Discord.GatewayIntents.AllUnprivileged | Discord.GatewayIntents.GuildMessageReactions | Discord.GatewayIntents.MessageContent
};

var client = new DiscordSocketClient(socketConfig);

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(AppContext.BaseDirectory).AddJsonFile("config.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton(client);

        // Pass the client directly into the InteractionService constructor
        services.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()));

        services.AddHostedService<InteractionHandlingService>();
        services.AddHostedService<StartupService>();
    })
    .Build();

await host.RunAsync();
