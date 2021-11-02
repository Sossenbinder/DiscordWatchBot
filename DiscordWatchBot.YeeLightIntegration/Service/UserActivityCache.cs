using System;
using System.Collections.Generic;
using DiscordWatchBot.YeeLightIntegration.Service.Interface;

namespace DiscordWatchBot.YeeLightIntegration.Service
{
	internal class UserActivityCache : IUserActivityCache
	{
		private readonly TimeSpan _timeBetweenNotifications = TimeSpan.FromMinutes(1);

		private readonly IDictionary<ulong, DateTime> _userActivityCache;

		public UserActivityCache()
		{
			_userActivityCache = new Dictionary<ulong, DateTime>();
		}

		public bool IsUserEligibleForNotification(ulong userId)
		{
			var isUserKnown = _userActivityCache.TryGetValue(userId, out var lastActivity);

			if (!isUserKnown)
			{
				Console.WriteLine("User not known yet");
				return true;
			}

			Console.WriteLine($"User known. Current time: {DateTime.UtcNow}. Last activity: {lastActivity}. Time between: {_timeBetweenNotifications}");
			var isEligible = DateTime.UtcNow > lastActivity + _timeBetweenNotifications;

			Console.WriteLine($"User eligible? {isEligible}");
			return isEligible;
		}

		public void ReportUserActivity(ulong userId)
		{
			_userActivityCache[userId] = DateTime.UtcNow;
		}
	}
}