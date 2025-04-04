using Discord.WebSocket;

namespace KamomeCAlgoExtensions.Discord;

public interface IDiscordProperty
{
    public DiscordSocketClient DiscordClient { get; }
    public SocketGuild DiscordGuild { get; }
    public SocketTextChannel DiscordChannel { get; }
}