using Discord;
using Discord.WebSocket;
using ItsAnotherBot;

public class Program
{
    private DiscordSocketClient _client;
    public static Task Main(string[] args) => new Program().MainAsync();

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();
        var token = Environment.GetEnvironmentVariable("BOT_TOKEN");

        _client.Ready += ClientReady;
        _client.SlashCommandExecuted += SlashCommandHandler;

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    public async Task ClientReady()
    {

        var existingCommands = await _client.GetGlobalApplicationCommandsAsync();
        List<SlashCommandBuilder> commands = new List<SlashCommandBuilder>() { };

        var globalCommand = new SlashCommandBuilder();
        globalCommand.WithName("test-command");
        globalCommand.WithDescription("a test global command for testing purposes");
        commands.Add(globalCommand);

        var globalCommand2 = new SlashCommandBuilder();
        globalCommand2.WithName("toast");
        globalCommand2.WithDescription("if you can do this, the deployment pipeline worked");
        commands.Add(globalCommand2);


        foreach (SlashCommandBuilder newCommand in commands)
        {
            if (!commands.Exists(cmd => cmd.Name == newCommand.Name))
            {
                await _client.CreateGlobalApplicationCommandAsync(newCommand.Build());
            }
        }
    }

    private async Task SlashCommandHandler(SocketSlashCommand command)
    {

        switch (command.Data.Name)
        {
            case "fortune":
                await command.RespondAsync(Fortune.fortune());
                break;
            default:
                await command.RespondAsync("Not implemented yet sorry");
                break;
        }
    }
        
}