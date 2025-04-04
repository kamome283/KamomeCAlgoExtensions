using Discord;
using Discord.WebSocket;

namespace KamomeCAlgoExtensions.Discord;

public static class DiscordCreationHelper
{
    public static async Task<(DiscordSocketClient, SocketGuild, SocketTextChannel)> CreateAsync(
        string token, ulong guildId, ulong channelId, TimeSpan timeout
    )
    {
        var config = new DiscordSocketConfig { GatewayIntents = GatewayIntents.AllUnprivileged };
        var client = new DiscordSocketClient(config);
        var waitForReady = new AutoResetEvent(false);
        client.Ready += () =>
        {
            waitForReady.Set();
            return Task.CompletedTask;
        };
        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();
        waitForReady.WaitOne(timeout);
        var guild = client.GetGuild(guildId) ?? throw new ArgumentOutOfRangeException(nameof(guildId));
        var channel = guild.GetTextChannel(channelId) ?? throw new ArgumentOutOfRangeException(nameof(channelId));
        return (client, guild, channel);
    }

    public static async Task<(DiscordSocketClient, SocketGuild, SocketTextChannel)> CreateAsync(
        string token, string guildId, string channelId, TimeSpan timeout
    )
    {
        var ulongGuildId = ulong.Parse(guildId);
        var ulongChannelId = ulong.Parse(channelId);
        return await CreateAsync(token, ulongGuildId, ulongChannelId, timeout);
    }
}