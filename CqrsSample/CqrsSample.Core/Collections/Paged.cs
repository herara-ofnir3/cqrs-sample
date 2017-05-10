using System.Collections;
using System.Collections.Generic;

namespace CqrsSample.Core.Collections
{
	/// <summary>
	/// ページ処理済みのコレクションを表します。
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IPaged<out T> : IEnumerable<T>, IPaged
	{
	}

	public interface IPaged : IEnumerable
	{
		int TotalCount { get; }
	}

	/// <summary>
	/// ページ処理済みのコレクションです。
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Paged<T> : IPaged<T>
	{
		/// <summary>
		/// ページ処理済みのコレクションを生成します。
		/// </summary>
		/// <param name="collection">元になるコレクション</param>
		/// <param name="totalCount">総件数</param>
		public Paged(IEnumerable<T> collection, int totalCount)
		{
			_collection = collection;
			_totalCount = totalCount;
		}

		private readonly IEnumerable<T> _collection;
		private readonly int _totalCount;

		/// <summary>
		/// 総件数
		/// </summary>
		public int TotalCount
		{
			get { return _totalCount; }
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _collection.GetEnumerator();
		}
	}
}
