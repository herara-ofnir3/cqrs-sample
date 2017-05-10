namespace CqrsSample.Core.Config
{
	/// <summary>
	/// アプリケーション設定のインスタンスを管理します。
	/// </summary>
	public static class AppConfigProvider
	{
		public static IAppConfig _config;

		public static void Init(IAppConfig config)
		{
			_config = config;
		}

		public static IAppConfig Config
		{
			get { return _config; }
		}
	}
}
