using System;
using System.Threading.Tasks;
using DiscordWatchBot.YeeLightIntegration.DataTypes;
using DiscordWatchBot.YeeLightIntegration.Extensions;
using YeelightAPI;

namespace DiscordWatchBot.YeeLightIntegration.Util
{
	public class ColorScope : IAsyncDisposable
	{
		private readonly Rgb _originalRgb;

		private readonly Device _device;

		private ColorScope(
			Rgb originalRgb,
			Device device)
		{
			_originalRgb = originalRgb;
			_device = device;
		}

		public static async Task<ColorScope> Construct(Device device)
		{
			var originalRgb = await device.GetCurrentRgb();
			return new ColorScope(originalRgb, device);
		}

		public async ValueTask DisposeAsync()
		{
			var (red, green, blue) = _originalRgb;

			await _device.SetRGBColor(red, green, blue, 1);
		}
	}
}