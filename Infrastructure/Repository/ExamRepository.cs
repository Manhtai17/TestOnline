using ApplicationCore.Entity;
using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
	public class ExamRepository : BaseRepository<Exam>, IExamRepository
	{
		public IEnumerable<Exam> GetExamByContestID(string contestID)
		{
			var result = GetEntities("Proc_GetExamByUserID", new object[] { contestID });
			return result;
		}
	}
}
