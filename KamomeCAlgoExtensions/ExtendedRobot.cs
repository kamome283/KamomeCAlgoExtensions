using cAlgo.API;
using Discord.WebSocket;
using KamomeCAlgoExtensions.Debug;
using KamomeCAlgoExtensions.Discord;

namespace KamomeCAlgoExtensions;

public abstract class ExtendedRobot : Robot, IDiscordProperty
{
    protected virtual bool LaunchDebugger => false;

    [Parameter("Discord API Token", Group = "Discord")]
    public string DiscordApiToken { get; set; } = null!;

    [Parameter("Discord Guild ID", Group = "Discord")]
    public string DiscordGuildId { get; set; } = null!;

    [Parameter("Discord Channel ID", Group = "Discord")]
    public string DiscordChannelId { get; set; } = null!;

    [Parameter("Discord Timeout Seconds for Ready", Group = "Discord", DefaultValue = 10)]
    public int DiscordReadyTimeoutSecs { get; set; } = 10;

    public DiscordSocketClient DiscordClient { get; private set; } = null!;
    public SocketGuild DiscordGuild { get; private set; } = null!;
    public SocketTextChannel DiscordChannel { get; private set; } = null!;

    protected override void OnStart()
    {
        base.OnStart();
        var task = SetDiscordPropertiesAsync();
        task.Wait();
        if (LaunchDebugger) DebugHelper.LaunchDebugger(Print);
    }

    private async Task SetDiscordPropertiesAsync()
    {
        var timeout = TimeSpan.FromSeconds(DiscordReadyTimeoutSecs);
        var (client, guild, channel) =
            await DiscordCreationHelper.CreateAsync(DiscordApiToken, DiscordGuildId, DiscordChannelId, timeout);
        DiscordClient = client;
        DiscordGuild = guild;
        DiscordChannel = channel;
    }
}