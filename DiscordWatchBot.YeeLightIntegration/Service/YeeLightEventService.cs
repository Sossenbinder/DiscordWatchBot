using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscordWatchBot.DiscordIntegration.Events.Interface;
using DiscordWatchBot.YeeLightIntegration.Extensions;
using DiscordWatchBot.YeeLightIntegration.Service.Interface;
using DiscordWatchBot.YeeLightIntegration.Util.Interface;
using Microsoft.Extensions.Configuration;
using YeelightAPI;

namespace DiscordWatchBot.YeeLightIntegration.Service
{
	internal class YeeLightEventService : IYeetLightEventService
	{
		private readonly IColorScopeProvider _colorScopeProvider;

		private readonly IUserActivityCache _userActivityCache;

		private readonly Device _device;

		private List<ulong> _excludedUserIds;

		public YeeLightEventService(
			IConfiguration configuration,
			IDiscordEventHub discordEventHub,
			IColorScopeProvider colorScopeProvider,
			IUserActivityCache userActivityCache,
			Device device)
		{
			_colorScopeProvider = colorScopeProvider;
			_userActivityCache = userActivityCache;
			_device = device;

			_excludedUserIds = configuration["ExcludedUserIds"]
				.Split(";")
				.Select(ulong.Parse)
				.ToList();

			discordEventHub.OnUserJoined.Register(OnUserJoined);
			discordEventHub.OnUserLeft.Register(OnUserLeft);
		}

		private Task OnUserJoined(ulong user)
		{
			return PerformFlash(user, 0, 255, 0);
		}

		private Task OnUserLeft(ulong user)
		{
			return PerformFlash(user, 180, 80, 80);
		}

		private async Task PerformFlash(ulong user, int red, int green, int blue)
		{
			if (_excludedUserIds.Contains(user))
			{
				return;
			}

			if (!_userActivityCache.IsUserEligibleForNotification(user))
			{
				return;
			}

			if (_device.IsConnected && await _device.IsTurnedOn())
			{
				await using (await _colorScopeProvider.GetColorScope())
				{
					await _device.SetRGBColor(red, green, blue, 1);

					await Task.Delay(1000);
				}

				_userActivityCache.ReportUserActivity(user);
			}
		}
	}
}