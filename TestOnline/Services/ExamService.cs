using ApplicationCore.Entitty;
using ApplicationCore.Entity;
using AutoMapper;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestOnline.Interfaces;

namespace TestOnline.Services
{
	public class ExamService : IExamService
	{

		private readonly IExamRepository _examRepo;
		private readonly IBaseRepository<Contest> _contestRepo;
		private readonly IMapper _mapper;

		public ExamService(IExamRepository examRepo, IBaseRepository<Contest> contestRepo, IMapper mapper)
		{
			_examRepo = examRepo;
			_contestRepo = contestRepo;
			_mapper = mapper;
		}

		public Task<IEnumerable<Exam>> GetByUserID(string contestID)
		{
			return Task.FromResult(_examRepo.GetExamByContestID(contestID));
		}


		//public async Task<object> GetByUserID(string userID, string contestID, string roleName)
		//{
		//	var result = _examRepo.GetExamByContestID(contestID);
		//	switch (roleName)
		//	{
		//		case "lecture":
		//			return result;
		//		case "student":
		//			var exam = result.Where(item => item.UserId.ToString() == userID).FirstOrDefault();
		//			if (exam == null)
		//			{
		//				//Handle goi api tao de thi tu nhom 10
		//				var response = JsonConvert.SerializeObject("[{ 'Question':'Day la cau hoi','type':1,'Answer':'|Dap an 1|Dap an 2 |Dap an 3'},{ 'Question':'Day la cau hoi','type':1,'Answer':'|Dap an 1|Dap an 2 |Dap an 3'},{ 'Question':'Day la cau hoi','type':1,'Answer':'|Dap an 1|Dap an 2 |Dap an 3'}]");
		//				//
		//				var examRes = new Exam();
		//				examRes.ContestId = Guid.Parse(contestID);
		//				examRes.CreatedDate = DateTime.Now;
		//				examRes.ModifiedDate = exam.CreatedDate;
		//				examRes.ExamId = Guid.NewGuid();
		//				examRes.UserId = Guid.Parse(userID);
		//				examRes.Question = response;
		//				//exam.Answer = response.Answer;
		//				exam.IsDoing = 1;
		//				exam.Status = 0;

		//				await _examRepo.UpdateAsync(exam);
		//				return examRes;
		//			}
		//			else
		//			{
		//				return exam;
		//			}


		//		default:
		//			break;
		//	}

		//	return result;
		//}


		//public async Task<Exam> GetExamForStudent(string userID,string contestID,IEnumerable<Exam> exams)
		//{
		//	var exam = exams.Where(item => item.UserId.ToString() == userID).FirstOrDefault();
		//	if (exam == null)
		//	{
		//		//Handle goi api tao de thi tu nhom 10
		//		var response = new List<Question>{
		//					new Question{
		//						QuestionID ="123",
		//						QuestionTitle="Day la cau hoi",
		//						Type=1,
		//						Answer="|Dap an 1|Dap an 2 |Dap an 3"
		//					}
		//				};
		//		//
		//		var examRes = new Exam();
		//		examRes.ContestId = Guid.Parse(contestID);
		//		examRes.CreatedDate = DateTime.Now;
		//		examRes.ModifiedDate = exam.CreatedDate;
		//		examRes.ExamId = Guid.NewGuid();
		//		examRes.UserId = Guid.Parse(userID);
		//		examRes.Question = response;
		//		//exam.Answer = response.Answer;
		//		exam.IsDoing = 1;
		//		exam.Status = 0;

		//		await _examRepo.UpdateAsync(exam);
		//		return examRes;
		//	}
		//	else
		//	{
		//		var contest = await _contestRepo.GetEntityByIdAsync(contestID);
		//		if (exam.Status == 1 || DateTime.Compare(contest.FinishTime, DateTime.Now) <= 0)
		//		{
		//			exam.Question = null;
		//			exam.Answer = null;

		//			return exam;
		//		}
		//		else
		//		{

		//			//đang có người làm 
		//			if (exam.IsDoing == 1 || DateTime.Compare(contest.StartTime, DateTime.Now) > 0)
		//			{
		//				return new Exam();
		//			}
		//			var lastExam = exams.OrderByDescending(item => item.ModifiedDate).Take(1);
		//			if ((exam.ModifiedDate - exam.CreatedDate - TimeSpan.FromMinutes(contest.TimeToDo) > TimeSpan.Zero && DateTime.Now - exam.ModifiedDate > TimeSpan.FromSeconds(30))
		//				)
		//			{
		//				return exam;
		//			}
		//			if (DateTime.Compare(contest.FinishTime, DateTime.Now) <= 0)
		//			{

		//				exam.IsDoing = 0;
		//				exam.Status = 1;
		//				await _examRepo.Update(exam);

		//				exam.Question = null;
		//				exam.Answer = null;
		//				return exam;
		//			}

		//		}
		//		//var contest = await _contestRepo.GetEntityByIdAsync(contestID);
		//		//đang có người làm 
		//		if (exam.IsDoing == 1 || DateTime.Compare(contest.StartTime, DateTime.Now) > 0)
		//		{
		//			return new Exam() ;
		//		}
		//		//Đã submit không cho làm lại (xem đề)
		//		if (exam.Status == 1)
		//		{
		//			exam.Question = null;
		//			exam.Answer = null;

		//			return exam ;
		//		}
		//		//var lastExam = exams.OrderByDescending(item=>item.ModifiedDate).Take(1) ;
		//		if (( exam.ModifiedDate - exam.CreatedDate - TimeSpan.FromMinutes(contest.TimeToDo) > TimeSpan.Zero &&  DateTime.Now-exam.ModifiedDate > TimeSpan.FromSeconds(30))
		//			|| 
		//			)
		//		{
		//			return exam;
		//		}
		//		if (DateTime.Compare(contest.FinishTime, DateTime.Now) <= 0)
		//		{
		//			if(exam.Status == 0)
		//			{
		//				exam.IsDoing = 0;
		//				exam.Status = 1;
		//				await _examRepo.Update(exam);
		//			}
		//			else
		//			{
		//				exam.IsDoing = 0;
		//			}
		//			exam.Question = null;
		//			exam.Answer = null;
		//			return exam;
		//		}
		//	}
		//	return new Exam();
		//}
	}
}
