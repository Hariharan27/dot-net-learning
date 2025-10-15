using System;
namespace WeekOneApplication
{
	public static class AppConfig
	{
		public static string Appname;

		public static string AppVersion;

	     static AppConfig()
		{
			Appname = "Spice";
			AppVersion = "1.0.5";
		}

		public static void DisplayDetail()
		{
			Console.WriteLine($"Appname:{Appname}, AppVersion:{AppVersion}");
		}

		public static void changeAppVersionName(string version)
		{
			AppVersion = version;
		}

	}
}

