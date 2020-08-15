using Discord.WebSocket;
using DiscordWatchBot.Common.Utils;

namespace DiscordWatchBot.DiscordIntegration.Util
{
	public class DiscordClientFactory : BaseLazyFactory<DiscordSocketClient>
	{
		public DiscordClientFactory()
			: base(PrepareClient)
		{
		}

		private static DiscordSocketClient PrepareClient()
		{
			var client = new DiscordSocketClient();

			return client;
		}
	}
}