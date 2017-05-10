using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsSample.Core.Collections
{
	/// <summary>
	/// ページ情報を表します。
	/// </summary>
	public struct Page
	{
		/// <summary>
		/// ページ情報を生成します。
		/// </summary>
		/// <param name="number">ページ番号</param>
		/// <param name="perPage">1ページあたりの件数</param>
		public Page(int number, int perPage)
		{
			if (number <= 0) throw new ArgumentOutOfRangeException("number");
			if (perPage <= 0) throw new ArgumentOutOfRangeException("perPage");
			_number = number;
			_perPage = perPage;
		}

		private readonly int _number;
		private readonly int _perPage;

		/// <summary>
		/// ページ番号
		/// </summary>
		public int Number { get { return _number; } }

		/// <summary>
		/// 1ページあたりの件数
		/// </summary>
		public int PerPage { get { return _perPage; } }

		/// <summary>
		/// オフセット
		/// </summary>
		public int Offset
		{
			get { return (Number - 1) * PerPage; }
		}
	}
}
