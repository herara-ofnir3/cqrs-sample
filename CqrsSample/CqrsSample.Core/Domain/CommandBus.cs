using CqrsSample.Core.Config;
using CqrsSample.Core.Data;
using CqrsSample.Core.UnitOfWork;
using Dapper;
using Newtonsoft.Json;
using StructureMap;
using System.Collections.Generic;
using System.Data;

namespace CqrsSample.Core.Domain
{
	public interface ICommandBus
	{
		void Send(ICommand command);

		void Send(IEnumerable<ICommand> commands);
	}

	public class DefaultCommandBus : ICommandBus
	{
		public DefaultCommandBus(IUnitOfWork uow, IDbConnectionFactory dbConnectionFactory)
		{
			_container = ContainerProvider.Container;
			_uow = uow;
			_dbConnectionFactory = dbConnectionFactory;
		}

		private readonly IContainer _container;
		private readonly IUnitOfWork _uow;
		private readonly IDbConnectionFactory _dbConnectionFactory;

		public void Send(ICommand command)
		{
			Send(new ICommand[] { command });
		}

		public void Send(IEnumerable<ICommand> commands)
		{
			using (var transaction = _uow.BeginTransaction())
			{
				foreach (var command in commands)
				{
					// コマンドの型から適切なハンドラを取得してコマンドを実行させます。
					var genericHandlerType = typeof(ICommandHandler<>);
					var handlerType = genericHandlerType.MakeGenericType(command.GetType());

					dynamic handler = _container.GetInstance(handlerType);
					handler.Handle((dynamic)command);
				}
				transaction.Commit();
			}

			// 発行されたコマンドをログに記録します。
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				connection.Open();
				using (var transaction = connection.BeginTransaction())
				{
					foreach (var command in commands)
					{
						var sql = "insert CommandLogs (Id, IssuedAt, Type, Body) values (@id, @issuedAt, @type, @body)";
						var json = JsonConvert.SerializeObject(command);
						connection.Execute(sql, new { id = command.Id, issuedAt = command.IssuedAt, type = command.GetType().ToString(), body = json }, transaction);
					}
					transaction.Commit();
				}
			}
		}

		private IDbConnection CreateConnection()
		{
			return _dbConnectionFactory.CreateConnection();
		}
	}
}
