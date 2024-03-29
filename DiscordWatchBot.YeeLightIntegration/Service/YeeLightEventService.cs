﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

		private readonly List<ulong>? _excludedUserIds;

		private readonly SemaphoreSlim _lightLock;

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
			_lightLock = new SemaphoreSlim(1);

			var excludedUserIdString = configuration["ExcludedUserIds"];

			if (excludedUserIdString != null)
			{
				_excludedUserIds = excludedUserIdString
					.Split(";")
					.Select(ulong.Parse)
					.ToList();
			}

			discordEventHub.OnUserJoined.Register(OnUserJoined);
			discordEventHub.OnUserLeft.Register(OnUserLeft);
		}

		private Task OnUserJoined(ulong user)
		{
			Console.WriteLine($"User with id {user} joined.");
			return PerformFlash(user, 0, 255, 0);
		}

		private Task OnUserLeft(ulong user)
		{
			Console.WriteLine($"User with id {user} left.");
			return PerformFlash(user, 180, 80, 80);
		}

		private async Task PerformFlash(ulong user, int red, int green, int blue)
		{
			if (_excludedUserIds?.Contains(user) ?? false)
			{
				Console.WriteLine("Won't trigger. User is excluded");
				return;
			}

			if (!_userActivityCache.IsUserEligibleForNotification(user))
			{
				Console.WriteLine("Won't trigger. User is not eligible");
				return;
			}

			if (_device.IsConnected && await _device.IsTurnedOn())
			{
				await RunLight(user, red, green, blue);
			}
			else
			{
				Console.WriteLine("Device not connected or not turned on. Fail.");
				if (await _device.Connect())
				{
					Console.WriteLine("Reconnected, retrying...");
					await RunLight(user, red, green, blue);
				}
				else
				{
					Console.WriteLine("Couldn't reconnect");
				}
			}
		}

		private async Task RunLight(ulong user, int red, int green, int blue)
		{
			Console.WriteLine("Running");
			await _lightLock.WaitAsync();

			await using (await _colorScopeProvider.GetColorScope())
			{
				await _device.SetRGBColor(red, green, blue, 1);

				await Task.Delay(1000);
			}

			_userActivityCache.ReportUserActivity(user);

			_lightLock.Release();
			Console.WriteLine("Unlocking");
		}
	}
}