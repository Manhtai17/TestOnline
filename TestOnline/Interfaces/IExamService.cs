using ApplicationCore.Entity;
using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestOnline.Interfaces
{
	public interface IExamService
	{
		public Task<IEnumerable<Exam>> GetByUserID(string contestID);
		//public Task<ExamDTO> GetByUserID(string userID, string contestID);
	}
}
