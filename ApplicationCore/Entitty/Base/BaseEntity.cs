using static ApplicationCore.Enums.Enumration;

namespace ApplicationCore.Entity
{
	public abstract class BaseEntity : IAggregateRoot
	{
		public EntityState EntityState = EntityState.GET;
	}
}
