﻿using System;

namespace ApplicationCore.Entity
{
	public partial class Userterm : BaseEntity
	{
		public Guid UserTermId { get; set; }
		public Guid? UserId { get; set; }
		public Guid? TermId { get; set; }
	}
}
