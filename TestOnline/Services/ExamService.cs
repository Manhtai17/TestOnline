using ApplicationCore.Entity;
using Infrastructure.Repository.Interfaces;
using System;
using System.Threading.Tasks;
using TestOnline.Interfaces;

namespace TestOnline.Services
{
	public class ExamService : IExamService
	{

		private readonly IExamRepository _examRepo;
		private readonly IBaseRepository<Contest> _contestRepo;

		public ExamService(IExamRepository examRepo, IBaseRepository<Contest> contestRepo)
		{
			_examRepo = examRepo;
			_contestRepo = contestRepo;
		}
		public async Task<Exam> GetByUserID(string userID, string contestID)
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
				var contest =await _contestRepo.GetEntityByIdAsync(result.ContestId.ToString());
				//đang có người làm 
				if (result.IsDoing == 1 || DateTime.Compare(contest.StartTime, DateTime.Now) > 0 )
				{
					return new Exam();
				}
				//Đã submit không cho làm lại (xem đề)
				else if (result.Status == 1)
				{
					result.Question = null;
					result.Answer = null;

					return result;
				}

				else if (result.ModifiedDate - result.CreatedDate - TimeSpan.FromMinutes(contest.TimeToDo)> TimeSpan.Zero )
				{
					return result;
				}
				else if(DateTime.Compare(contest.FinishTime, DateTime.Now) <= 0)
				{
					if(result.Status == 0)
					{
						result.ModifiedDate = DateTime.Now;
						result.IsDoing = 0;
						result.Status = 1;
						await _examRepo.Update(result);
					}

					result.IsDoing = 0;
				}

				
			}

			return result;
		}
	}
}
