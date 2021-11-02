using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DiscordWatchBot.DiscordIntegration.Module;
using DiscordWatchBot.Service;
using DiscordWatchBot.YeeLightIntegration.Module;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordWatchBot
{
	internal class Program
	{
		public static async Task Main(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.AddEnvironmentVariables()
				.Build();

			var containerBuilder = new ContainerBuilder();

			containerBuilder
				.Register(_ => configuration)
				.As<IConfiguration>();

			containerBuilder.RegisterModule<YeeLightIntegrationModule>();
			containerBuilder.RegisterModule<DiscordIntegrationModule>();

			containerBuilder.RegisterType<CompositeIntegrationInitializer>()
				.AsSelf()
				.SingleInstance();

			var serviceProvider = new AutofacServiceProvider(containerBuilder.Build());

			var integrationInitializer = serviceProvider.GetRequiredService<CompositeIntegrationInitializer>();

			await integrationInitializer.Initialize();

			await Task.Delay(Timeout.Infinite);
		}
	}
}