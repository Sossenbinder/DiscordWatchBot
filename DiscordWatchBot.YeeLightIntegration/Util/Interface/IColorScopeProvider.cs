using System.Threading.Tasks;

namespace DiscordWatchBot.YeeLightIntegration.Util.Interface
{
	internal interface IColorScopeProvider
	{
		Task<ColorScope> GetColorScope();
	}
}