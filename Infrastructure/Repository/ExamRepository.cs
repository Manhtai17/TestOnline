using ApplicationCore.Entity;
using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
	public class ExamRepository : BaseRepository<Exam>, IExamRepository
	{
		public Exam GetExamByUserID(string userID, string contestID)
		{
			var result = GetEntity("Proc_GetExamByUserID", new object[] { userID, contestID }).Result;
			return result;
		}
	}
}
