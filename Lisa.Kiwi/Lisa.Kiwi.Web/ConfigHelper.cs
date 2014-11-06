using System;
using System.Configuration;
using System.Web.Configuration;

namespace Lisa.Kiwi.Web
{
	public static class ConfigHelper
	{
		public static Uri GetODataUri()
		{
			var config = WebConfigurationManager.OpenWebConfiguration("~");
			var urlSetting = config.AppSettings.Settings["KiwiODataUrl"];

			if (urlSetting == null)
			{
				throw new ConfigurationErrorsException("KiwiODataUrl is missing in given configuration.");
			}

			return new Uri(urlSetting.Value);
		}
	}
}