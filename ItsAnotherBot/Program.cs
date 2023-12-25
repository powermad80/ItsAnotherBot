using Discord;
using Discord.WebSocket;

public class Program
{
    private DiscordSocketClient _client;
    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();
        var token = File.ReadAllText("token.txt");

        _client.Ready += ClientReady;
        _client.SlashCommandExecuted += SlashCommandHandler;

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    public async Task ClientReady()
    {
        var globalCommand = new SlashCommandBuilder();
        globalCommand.WithName("test-command");
        globalCommand.WithDescription("a test global command for testing purposes");
        await _client.CreateGlobalApplicationCommandAsync(globalCommand.Build());
    }

    private async Task SlashCommandHandler(SocketSlashCommand command)
    {
        await command.RespondAsync("You done did the testing command.");
    }
}