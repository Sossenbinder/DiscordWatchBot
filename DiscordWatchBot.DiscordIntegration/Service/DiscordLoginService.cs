using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordWatchBot.Common.Utils;
using Microsoft.Extensions.Configuration;

namespace DiscordWatchBot.DiscordIntegration.Service
{
	public class DiscordLoginService : IIntegrationInitializer
	{
		private readonly IConfiguration _configuration;

		private readonly DiscordSocketClient _discordSocketClient;

		public DiscordLoginService(
			IConfiguration configuration,
			DiscordSocketClient discordSocketClient)
		{
			_configuration = configuration;
			_discordSocketClient = discordSocketClient;
		}

		public async Task Initialize()
		{
			var tcs = new TaskCompletionSource<object>();

			await _discordSocketClient.LoginAsync(TokenType.Bot, _configuration["DiscordBotToken"]);
			await _discordSocketClient.StartAsync();

			_discordSocketClient.Connected += () =>
			{
				tcs.SetResult(null);
				return Task.CompletedTask;
			};

			await tcs.Task;
		}
	}
}