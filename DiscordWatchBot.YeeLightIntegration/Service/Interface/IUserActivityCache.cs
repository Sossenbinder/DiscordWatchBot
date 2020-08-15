using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordWatchBot.YeeLightIntegration.Service.Interface
{
	internal interface IUserActivityCache
	{
		bool IsUserEligibleForNotification(ulong userId);

		void ReportUserActivity(ulong userId);
	}
}