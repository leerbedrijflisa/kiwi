using System;
using System.Configuration;
using System.Web.Configuration;

namespace Lisa.Kiwi.Web
{
	public static class ConfigHelper
	{
		private static Uri GetUri(string key)
		{
			var config = WebConfigurationManager.OpenWebConfiguration("~");
			var urlSetting = config.AppSettings.Settings[key];

			if (urlSetting == null)
			{
				throw new ConfigurationErrorsException(key + " is missing in given configuration.");
			}

			return new Uri(urlSetting.Value);
		}

		public static Uri GetODataUri()
		{
			return GetUri("KiwiODataUrl");
		}

        public static Uri GetSignalRUri()
        {
            return GetUri("KiwiSignelRUrl");
        }

        public static Uri GetAuthUri()
        {
	        return GetUri("KiwiAuthUrl");
        }

		public static Uri GetUserControllerUri()
		{
			return GetUri("KiwiUserControllerUrl");
		}
	}
}