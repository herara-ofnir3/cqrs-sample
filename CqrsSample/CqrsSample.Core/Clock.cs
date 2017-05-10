using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsSample.Core
{
	/// <summary>
	/// 現在時刻を提供します。
	/// </summary>
	public sealed class Clock : IDisposable
	{
		private Clock() { }

		public void Dispose()
		{
			_now = null;
		}

		private static DateTime? _now;

		public static DateTime Now
		{
			get { return _now ?? DateTime.Now; }
		}

		/// <summary>
		/// このクラスが提供する時刻を指定した値で固定します。
		/// 主にテストのための機能です。
		/// </summary>
		/// <param name="now"></param>
		/// <returns></returns>
		public static IDisposable NowIs(DateTime now)
		{
			_now = now;
			return new Clock();
		}
	}
}
