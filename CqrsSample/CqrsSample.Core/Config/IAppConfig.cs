namespace CqrsSample.Core.Config
{
	/// <summary>
	/// アプリケーションの設定を表します。
	/// </summary>
	public interface IAppConfig
	{
		/// <summary>
		/// 既定の接続文字列
		/// </summary>
		string DefaultConnection { get; }
	}
}
