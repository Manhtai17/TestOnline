using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository.Interfaces
{
	public interface IExamRepository : IBaseRepository<Exam>
	{
		public Exam GetExamByUserID(string userID, string contestID);
	}
}
