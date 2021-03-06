﻿using ApplicationCore;
using ApplicationCore.Entity;
using AutoMapper;
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
using TestOnline.Utility;
using static ApplicationCore.Enums.Enumration;

namespace TestOnline.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExamsController : ControllerBase
	{
		private readonly IExamService _examService;
		private readonly IBaseRepository<Contest> _contestRepo;
		private readonly ProducerConfig _producerConfig;
		private readonly IMapper _mapper;
		private readonly IBaseEntityService<Exam> _baseEntityService;
		public ExamsController(IBaseEntityService<Exam> baseEntityService, IExamService examService, ProducerConfig producerConfig, IBaseRepository<Contest> contestRepo, IMapper mapper)
		{
			_examService = examService;
			_producerConfig = producerConfig;
			_contestRepo = contestRepo;
			_mapper = mapper;
			_baseEntityService = baseEntityService;
		}
		[HttpGet]
		public async Task<ActionServiceResult> GetEntityByID([FromQuery] string contestID)
		{
			StringValues userHeader;
			Request.Headers.TryGetValue("UserID", out userHeader);
			var userID = userHeader.FirstOrDefault().ToString();
			var token = Request.Headers["Authorization"].ToString();
			var roleName = Utils.GetClaimFromToken(token, "rolename") == "" ? "student" : Utils.GetClaimFromToken(token, "rolename");

			var result = new ActionServiceResult();
			if (userID == null || string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(contestID))
			{
				result.Success = false;
				result.Code = Code.NotFound;
			}
			else
			{
				var response = await _examService.GetByUserID(contestID);
				switch (roleName)
				{
					case "lecture":
						result.Data = response;
						return result;
					case "student":
						var exam = response.Where(item => item.UserId.ToString() == userID).FirstOrDefault();
						result.Data = exam;

						if (exam == null)
						{
							//Handle goi api tao de thi tu nhom 10
							var res = JsonConvert.DeserializeObject("[{'Question':'Day la cau hoi','type':1,'Answer':'|Dap an 1|Dap an 2 |Dap an 3'},{ 'Question':'Day la cau hoi','type':1,'Answer':'|Dap an 1|Dap an 2 |Dap an 3'},{ 'Question':'Day la cau hoi','type':1,'Answer':'|Dap an 1|Dap an 2 |Dap an 3'}]");
							//
							var examRes = new Exam();
							examRes.ContestId = Guid.Parse(contestID);
							examRes.CreatedDate = DateTime.Now;
							examRes.ModifiedDate = DateTime.Now;
							examRes.ExamId = Guid.NewGuid();
							examRes.UserId = Guid.Parse(userID);
							examRes.Question = response;
							//exam.Answer = response.Answer;
							examRes.IsDoing = 1;
							examRes.Status = 0;

							await _baseEntityService.Insert(examRes);
							return new ActionServiceResult(true,Resources.Success, Code.Success,examRes,0);
						}
						else
						{
							exam.ModifiedDate = DateTime.Now;
							result.Data = exam;
							await _baseEntityService.Update(exam);
							return result;
						}
						
					default:
						break;
				}
			}
			return result;
		}

		/// <summary>
		/// Cập nhật
		/// </summary>
		/// <param name="entity">Đối tượng sửa</param>
		/// <returns></returns>
		[HttpPut]
		public async Task<ActionServiceResult> Put([FromBody] Exam entity)
		{
			StringValues userHeader;
			Request.Headers.TryGetValue("UserID", out userHeader);
			var userID = userHeader.FirstOrDefault().ToString();
			var result = new ActionServiceResult();
			if (userID == null || string.IsNullOrEmpty(userID) || entity == null)
			{
				result.Success = false;
				result.Code = ApplicationCore.Enums.Enumration.Code.NotFound;
			}
			else
			{
				var response = new ActionServiceResult();
				if (entity == null)
				{
					response.Success = false;
					response.Code = Code.NotFound;
					response.Message = Resources.NotFound;
				}
				else
				{
					response = await _baseEntityService.Update(entity);
					return response;
				}
				return response;
			}
			return new ActionServiceResult(); 
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
					if (DateTime.Compare(DateTime.Now, contest.FinishTime) <= 0)
					{
						//Todo tinh diem
						exam.Point = 10;
						exam.IsDoing = 0;
						exam.Status = 1;
						await _baseEntityService.Update(exam);
						result.Data = exam.ExamId;
					}
					else
					{
						return new ActionServiceResult
						{
							Code = Code.NotFound,
							Data = null,
							Message = Resources.NotFound,
							Success = false

						};
					}
				}


			}
			return result;

		}
	}
}
