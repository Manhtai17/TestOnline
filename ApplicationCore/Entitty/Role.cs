using ApplicationCore.Entity;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Entity
{
	public partial class Role : BaseEntity
	{
		public Guid RoleId { get; set; }
		public string RoleName { get; set; }
	}
}
