using System;
namespace SampleWebAPI.services
{
	public class TimeService : ITimeService
	{
		public TimeService()
		{
		}

        public string GetCurrentTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }
}

