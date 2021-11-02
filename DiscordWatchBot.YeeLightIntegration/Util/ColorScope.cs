using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiscordWatchBot.Common.Utils;
using DiscordWatchBot.YeeLightIntegration.DataTypes;
using DiscordWatchBot.YeeLightIntegration.Extensions;
using YeelightAPI;
using YeelightAPI.Models;

namespace DiscordWatchBot.YeeLightIntegration.Util
{
	public class ColorScope : IAsyncDisposable
	{
		private readonly Rgb _originalRgb;

		private readonly int _colorTemperature;

		private readonly Device _device;

		private ColorScope(
			Rgb originalRgb,
			int colorTemperature,
			Device device)
		{
			_originalRgb = originalRgb;
			_colorTemperature = colorTemperature;
			_device = device;
		}

		public static async Task<ColorScope> Construct(Device device)
		{
			var (originalRgb, originalColorTemperature) = await (device.GetCurrentRgb(), device.GetProp(PROPERTIES.ct));
			return new ColorScope(originalRgb, int.Parse((originalColorTemperature as string)!), device);
		}

		public async ValueTask DisposeAsync()
		{
			var (red, green, blue) = _originalRgb;

			var tasks = new List<Task>
			{
				_device.SetRGBColor(red, green, blue, 1),
				_device.SetColorTemperature(_colorTemperature)
			};

			await Task.WhenAll(tasks);
		}
	}
}