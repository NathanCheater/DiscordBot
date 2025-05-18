using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

class Program
{
    private DiscordSocketClient _client;

    public static async Task Main(string[] args)
        => await new Program().MainAsync();

    public async Task MainAsync()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.All | GatewayIntents.MessageContent
        };

        _client = new DiscordSocketClient(config);

        _client.Log += Log;
        _client.MessageReceived += HandleMessageAsync;

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

    private async Task HandleMessageAsync(SocketMessage message)
    {
        if (message.Author.IsBot) return;

        Console.WriteLine($"[DEBUG] ได้รับข้อความ: {message.Content}");

        if (message.Content.ToLower() == "!ping")
        {
            await message.Channel.SendMessageAsync("pong!");
        }

        if (message.Content.ToLower() == "!hello")
        {
            await message.Channel.SendMessageAsync("หวัดดีน้าาา 😎");
        }

    }
}