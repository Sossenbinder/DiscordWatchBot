using DiscordWatchBot.Common.Utils;
using Microsoft.Extensions.Configuration;
using YeelightAPI;

namespace DiscordWatchBot.YeeLightIntegration.Util
{
	public class DeviceFactory : BaseLazyFactory<Device>
	{
		public DeviceFactory(IConfiguration configuration)
			: base(() => CreateDevice(configuration))
		{
		}

		private static Device CreateDevice(IConfiguration configuration)
		{
			var device = new Device(configuration["YeeLightIpAddress"]);

			return device;
		}
	}
}