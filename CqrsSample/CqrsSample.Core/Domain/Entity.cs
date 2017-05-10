using System;

namespace CqrsSample.Core.Domain
{
	public abstract class Entity<TId> : IEquatable<Entity<TId>>
	{
		protected Entity() { }

		public virtual TId Id { get; set; }

		public virtual bool Equals(Entity<TId> other)
		{
			if (other == null) return false;
			return Id.Equals(other.Id);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Entity<TId>);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
