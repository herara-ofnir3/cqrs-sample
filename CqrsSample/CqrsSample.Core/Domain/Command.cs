using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsSample.Core.Domain
{
	public interface ICommand
	{
		Guid Id { get; }

		DateTime IssuedAt { get; }
	}

	public abstract class Command : ICommand
	{
		protected Command()
		{
			Id = Guid.NewGuid();
			IssuedAt = Clock.Now;
		}

		public Guid Id { get; private set; }

		public DateTime IssuedAt { get; private set; }
	}
}
