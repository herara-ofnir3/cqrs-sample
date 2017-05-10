using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsSample.Core.Config
{
	public static class ContainerProvider
	{
		private static IContainer _container;

		public static void Init(IContainer container)
		{
			_container = container;
		}

		public static IContainer Container
		{
			get { return _container; }
		}
	}
}
