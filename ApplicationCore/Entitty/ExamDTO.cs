﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entitty
{
	public class ExamDTO
	{
		public Guid ExamId { get; set; }
		public Guid? ContestId { get; set; }
		public Guid? UserId { get; set; }
		public int? Point { get; set; }
		public ulong? Status { get; set; }
		public ulong? IsDoing { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string Question { get; set; }
		public string Result { get; set; }
	}
}
