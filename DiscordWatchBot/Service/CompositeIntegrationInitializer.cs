using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscordWatchBot.Common.Utils;

namespace DiscordWatchBot.Service
{
	internal class CompositeIntegrationInitializer : IIntegrationInitializer
	{
		private readonly IEnumerable<IIntegrationInitializer> _integrationInitializers;

		public CompositeIntegrationInitializer(IEnumerable<IIntegrationInitializer> integrationInitializers)
		{
			_integrationInitializers = integrationInitializers;
		}

		public Task Initialize()
		{
			return Task.WhenAll(_integrationInitializers.Select(x => x.Initialize()));
		}
	}
}