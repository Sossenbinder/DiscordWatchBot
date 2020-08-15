using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordWatchBot.DiscordIntegration.Events.Interface;
using DiscordWatchBot.DiscordIntegration.Service.Interface;

namespace DiscordWatchBot.DiscordIntegration.Service
{
	internal class MembersOnlineStateService : IMembersOnlineStateService
	{
		private readonly IDiscordEventHub _discordEventHub;

		public MembersOnlineStateService(
			DiscordSocketClient discordClient,
			IDiscordEventHub discordEventHub)
		{
			_discordEventHub = discordEventHub;
			discordClient.UserVoiceStateUpdated += OnUserVoiceStateUpdated;
		}

		private async Task OnUserVoiceStateUpdated(SocketUser socketUser, SocketVoiceState previousState, SocketVoiceState nextState)
		{
			if (previousState.VoiceChannel == null && nextState.VoiceChannel != null)
			{
				await _discordEventHub.OnUserJoined.Raise(socketUser.Id);
			}
			else if (previousState.VoiceChannel != null && nextState.VoiceChannel == null)
			{
				await _discordEventHub.OnUserLeft.Raise(socketUser.Id);
			}
		}
	}
}