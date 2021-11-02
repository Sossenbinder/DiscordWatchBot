using System;
using System.Threading.Tasks;
using DiscordWatchBot.Common.Utils;
using DiscordWatchBot.DiscordIntegration.Events.Interface;
using Microsoft.Extensions.Configuration;
using YeelightAPI;

namespace DiscordWatchBot.YeeLightIntegration.Service
{
	internal class YeeLightConnectionService : IIntegrationInitializer
	{
		private readonly IDiscordEventHub _eventHub;
		private readonly Device _device;

		public YeeLightConnectionService(
			IDiscordEventHub eventHub,
			Device device)
		{
			_eventHub = eventHub;
			_device = device;
		}

		public async Task Initialize()
		{
			await _device.Connect();
		}
	}
}