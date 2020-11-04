using ApplicationCore.Entity;
using AutoMapper;
using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestOnline.Interfaces;

namespace TestOnline.Services
{
	public class ExamService: IExamService
	{

		private readonly IExamRepository _examRepo;

		public ExamService(IExamRepository examRepo)
		{
			_examRepo = examRepo;
		}
		public Exam GetByUserID(string userID, string contestID)
		{
			var result = _examRepo.GetExamByUserID(userID, contestID);
			if (result == null)
			{
				var resultUpdate = 0;
				do
				{
					//Handle goi api tao de thi tu nhom 10
					var response = new
					{
						Questions = "This is de thi ",
						Answer = "This is dap an",
					};
					//
					var exam = new Exam();
					exam.ContestId = Guid.Parse(contestID);
					exam.CreatedDate = DateTime.Now;
					exam.ModifiedDate = exam.CreatedDate;
					exam.ExamId = Guid.NewGuid();
					exam.UserId = Guid.Parse(userID);
					exam.Question = response.Questions;
					exam.Answer = response.Answer;
					exam.IsDoing = 1;
					exam.Status = 0;

					resultUpdate = _examRepo.Update(exam).Result;
					result = exam;
				}
				while (resultUpdate > 0);
			}
			else
			{
				//đang có người làm 
				if(result.IsDoing==1)
				{
					return new Exam();
				}
				//Đã submit không cho làm lại (xem đề)
				if (result.Status == 1 )
				{
					result.Question = null;
					result.Answer = null;

					return result;
				}
				
				result.ModifiedDate = DateTime.Now;
				result.IsDoing = 1;
				result.Status = 0;
				_examRepo.Update(result);
			}

			return result;
		}
	}
}
