using Discord;
using Discord.WebSocket;

class Program
{
    private DiscordSocketClient _client;

    public static async Task Main(string[] args)
        => await new Program().MainAsync();

    public async Task MainAsync()
    {
        var config = new DiscordSocketConfig { GatewayIntents = GatewayIntents.All };
        _client = new DiscordSocketClient(config);

        _client.Log += Log;

        string? token = Environment.GetEnvironmentVariable("DISCORD_TOKEN");

        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Environment variable DISCORD_TOKEN not found!");
            return;
        }

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}