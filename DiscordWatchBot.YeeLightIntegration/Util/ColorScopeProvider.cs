using System.Threading.Tasks;
using DiscordWatchBot.YeeLightIntegration.Extensions;
using DiscordWatchBot.YeeLightIntegration.Util.Interface;
using YeelightAPI;

namespace DiscordWatchBot.YeeLightIntegration.Util
{
	public class ColorScopeProvider : IColorScopeProvider
	{
		private readonly Device _device;

		public ColorScopeProvider(Device device)
		{
			_device = device;
		}

		public Task<ColorScope> GetColorScope() => ColorScope.Construct(_device);
	}
}