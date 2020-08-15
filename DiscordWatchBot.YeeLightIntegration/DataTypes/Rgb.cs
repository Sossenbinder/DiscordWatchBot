namespace DiscordWatchBot.YeeLightIntegration.DataTypes
{
	public class Rgb
	{
		public int Red { get; set; }

		public int Green { get; set; }

		public int Blue { get; set; }

		public Rgb()
		{
		}

		public Rgb(int red, int green, int blue)
		{
			Red = red;
			Green = green;
			Blue = blue;
		}

		public static Rgb FromInt(int rgb)
		{
			var red = (rgb >> 16) & 0xFF;
			var green = (rgb >> 8) & 0xFF;
			var blue = rgb & 0xFF;

			return new Rgb(red, green, blue);
		}

		public void Deconstruct(out int red, out int green, out int blue)
		{
			red = Red;
			green = Green;
			blue = Blue;
		}

		public static Rgb Default => new Rgb(127, 127, 127);
	}
}