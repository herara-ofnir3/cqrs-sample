using CqrsSample.Core.Config;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;

namespace CqrsSample.Domain.Data
{
	/// <summary>
	/// NHibernate のセッションを管理します。
	/// </summary>
	public class NHSessionProvider
	{
		private static ISessionFactory CreateSessionFactory()
		{
			return Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2012
					.ConnectionString(AppConfigProvider.Config.DefaultConnection))
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHSessionProvider>())
				.BuildSessionFactory();
		}

		private static ISessionFactory _sessionFactory;

		private static ISessionFactory SessionFactory()
		{
			if (_sessionFactory == null)
			{
				try
				{
					_sessionFactory = CreateSessionFactory();
				}
				catch (Exception e)
				{
					throw;
				}
			}

			return _sessionFactory;
		}

		/// <summary>
		/// 新しいセッションを開始します。
		/// </summary>
		/// <returns></returns>
		public static ISession OpenSession()
		{
			return SessionFactory().OpenSession();
		}
	}
}
