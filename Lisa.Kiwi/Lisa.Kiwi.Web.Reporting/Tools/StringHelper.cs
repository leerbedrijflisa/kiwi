using System.Web;

namespace Lisa.Kiwi.Tools
{
	public class Utils
	{
		public static string GetIP()
		{
			string ipAddress;
			HttpContext context = HttpContext.Current;
			ipAddress = context.Request.ServerVariables["REMOTE_ADDR"];
			if (!string.IsNullOrEmpty(ipAddress))
			{
				return ipAddress;
			}

			ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

			if (!string.IsNullOrEmpty(ipAddress))
			{
				string[] addresses = ipAddress.Split(',');
				if (addresses.Length != 0)
				{
					return addresses[0];
				}
			}

			return null;
		}
	}
}