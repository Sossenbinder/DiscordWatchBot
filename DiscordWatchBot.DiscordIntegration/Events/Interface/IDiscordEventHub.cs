using DiscordWatchBot.Common.Eventing;

namespace DiscordWatchBot.DiscordIntegration.Events.Interface
{
	public interface IDiscordEventHub
	{
		AsyncEvent<ulong> OnUserJoined { get; }

		AsyncEvent<ulong> OnUserLeft { get; }
	}
}