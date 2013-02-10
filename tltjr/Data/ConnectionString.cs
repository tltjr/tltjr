using System.Configuration;

namespace tltjr.Data
{
	public static class ConnectionString
	{
		public static string MongoLab
		{
			get { return ConfigurationManager.AppSettings.Get("MONGOLAB_URI"); }
		}
	}
}