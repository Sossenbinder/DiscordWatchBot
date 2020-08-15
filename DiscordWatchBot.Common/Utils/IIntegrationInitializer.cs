using System.Threading.Tasks;

namespace DiscordWatchBot.Common.Utils
{
	public interface IIntegrationInitializer
	{
		Task Initialize();
	}
}