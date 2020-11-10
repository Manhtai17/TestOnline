using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entitty
{
	public class Question :BaseEntity
	{
		public string QuestionID { get; set; }
		public string QuestionTitle { get; set; }
		public int Type { get; set; }
		public string Answer { get; set; }
	}
}

