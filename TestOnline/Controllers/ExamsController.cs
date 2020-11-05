using ApplicationCore;
using ApplicationCore.Entity;
using Confluent.Kafka;
using ELearning.KafkaCommon;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestOnline.Interfaces;
using static ApplicationCore.Enums.Enumration;

namespace TestOnline.Controllers
{

	public class ExamsController : BaseController<Exam>
	{
		private readonly IExamService _examService;
		private readonly IBaseRepository<Contest> _contestRepo;
		private readonly ProducerConfig _producerConfig;
		public ExamsController(IBaseEntityService<Exam> baseEntityService, IExamService examService, ProducerConfig producerConfig, IBaseRepository<Contest> contestRepo) : base(baseEntityService)
		{
			_examService = examService;
			_producerConfig = producerConfig;
			_contestRepo = contestRepo;
		}
		[HttpGet]
		[Route("{contestID}")]
		public override async Task<ActionServiceResult> GetEntityByID(string contestID)
		{
			StringValues userHeader;
			Request.Headers.TryGetValue("UserID", out userHeader);
			var userID = userHeader.FirstOrDefault().ToString();
			var result = new ActionServiceResult();
			if (userID == null || string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(contestID))
			{
				result.Success = false;
				result.Code = Code.NotFound;
			}
			else
			{
				var response = _examService.GetByUserID(userID, contestID);
				result.Data = response;
			}
			return result;

		}

		[HttpGet]
		[Route("submit")]
		public async Task<ActionServiceResult> SubmitExam(Exam exam)
		{
			StringValues userHeader;
			Request.Headers.TryGetValue("UserID", out userHeader);
			var userID = userHeader.FirstOrDefault().ToString();
			var result = new ActionServiceResult();
			if (userID == null || string.IsNullOrEmpty(userID) || exam == null)
			{
				result.Success = false;
				result.Code = ApplicationCore.Enums.Enumration.Code.NotFound;
			}
			else
			{
				if (exam.Status == 0)
				{
					exam.ModifiedDate = DateTime.Now;
					var message = JsonConvert.SerializeObject(exam);
					using (var producer = new ProducerWrapper<Null, string>(_producerConfig, "autosubmit"))
					{

						await producer.SendMessage(message);
					}
					return new ActionServiceResult()
					{
						Success = true,
						Code = Code.Success,
						Message = Resources.Success,
						Data = exam.ExamId
					};
				}
				else
				{
					var contest = await _contestRepo.GetEntityByIdAsync(exam.ContestId);
					if(DateTime.Compare(DateTime.Now, contest.FinishTime) <= 0)
					{
						//Todo tinh diem 
						exam.Point = 10;
						exam.IsDoing = 0;
						exam.Status = 1;

						result.Data = exam;
						await _baseEntityService.Update(exam);
					}
				}
				

			}
			return result;

		}
	}
}
