using System;
using System.Threading.Tasks;
using DiscordWatchBot.Common.Utils;
using Microsoft.Extensions.Configuration;
using YeelightAPI;

namespace DiscordWatchBot.YeeLightIntegration.Service
{
	internal class YeeLightConnectionService : IIntegrationInitializer
	{
		private readonly Device _device;

		public YeeLightConnectionService(Device device)
		{
			_device = device;
		}

		public async Task Initialize()
		{
			await _device.Connect();
		}
	}
}