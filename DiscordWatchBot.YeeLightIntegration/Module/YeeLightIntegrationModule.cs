using Autofac;
using DiscordWatchBot.Common.Utils;
using DiscordWatchBot.YeeLightIntegration.Service;
using DiscordWatchBot.YeeLightIntegration.Service.Interface;
using DiscordWatchBot.YeeLightIntegration.Util;
using DiscordWatchBot.YeeLightIntegration.Util.Interface;
using YeelightAPI;

namespace DiscordWatchBot.YeeLightIntegration.Module
{
	public class YeeLightIntegrationModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<DeviceFactory>()
				.AsSelf()
				.SingleInstance();

			builder.Register(ctx => ctx.Resolve<DeviceFactory>().Get())
				.As<Device>()
				.SingleInstance();

			builder.RegisterType<YeeLightEventService>()
				.As<IYeetLightEventService>()
				.SingleInstance()
				.AutoActivate();

			builder.RegisterType<YeeLightConnectionService>()
				.As<IIntegrationInitializer>()
				.SingleInstance();

			builder.RegisterType<ColorScopeProvider>()
				.As<IColorScopeProvider>()
				.SingleInstance();

			builder.RegisterType<UserActivityCache>()
				.As<IUserActivityCache>()
				.SingleInstance();
		}
	}
}