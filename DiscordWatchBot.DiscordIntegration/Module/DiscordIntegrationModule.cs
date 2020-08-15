using Autofac;
using Discord.WebSocket;
using DiscordWatchBot.Common.Utils;
using DiscordWatchBot.DiscordIntegration.Events;
using DiscordWatchBot.DiscordIntegration.Events.Interface;
using DiscordWatchBot.DiscordIntegration.Service;
using DiscordWatchBot.DiscordIntegration.Service.Interface;
using DiscordWatchBot.DiscordIntegration.Util;

namespace DiscordWatchBot.DiscordIntegration.Module
{
	public class DiscordIntegrationModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<DiscordClientFactory>()
				.AsSelf()
				.SingleInstance();

			builder.RegisterType<DiscordLoginService>()
				.As<IIntegrationInitializer>()
				.SingleInstance();

			builder.RegisterType<MembersOnlineStateService>()
				.As<IMembersOnlineStateService>()
				.AutoActivate()
				.SingleInstance();

			builder.RegisterType<DiscordEventHub>()
				.As<IDiscordEventHub>()
				.SingleInstance();

			builder.Register(ctx => ctx.Resolve<DiscordClientFactory>().Get())
				.As<DiscordSocketClient>()
				.SingleInstance();
		}
	}
}