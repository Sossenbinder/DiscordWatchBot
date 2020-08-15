using DiscordWatchBot.Common.Eventing;
using DiscordWatchBot.DiscordIntegration.Events.Interface;

namespace DiscordWatchBot.DiscordIntegration.Events
{
	internal class DiscordEventHub : IDiscordEventHub
	{
		public AsyncEvent<ulong> OnUserJoined { get; } = new AsyncEvent<ulong>();

		public AsyncEvent<ulong> OnUserLeft { get; } = new AsyncEvent<ulong>();
	}
}