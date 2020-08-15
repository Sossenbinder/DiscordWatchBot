using System.Threading.Tasks;
using DiscordWatchBot.YeeLightIntegration.DataTypes;
using YeelightAPI;
using YeelightAPI.Models;

namespace DiscordWatchBot.YeeLightIntegration.Extensions
{
	public static class DeviceExtensions
	{
		public static async Task<bool> IsTurnedOn(this Device device)
		{
			var powerState = await device.GetProp(PROPERTIES.power);

			return powerState.Equals("on");
		}

		public static async Task<Rgb> GetCurrentRgb(this Device device)
		{
			var rgb = await device.GetProp(PROPERTIES.rgb);

			var wasParsed = int.TryParse((string)rgb, out var rgbParsed);

			return !wasParsed ? new Rgb() : Rgb.FromInt(rgbParsed);
		}
	}
}