using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using TestOnline.Interfaces;

namespace TestOnline.Controllers
{
	
	public class ExamsController : BaseController<Exam>
	{
		private readonly IExamService _examService;
		public ExamsController(IBaseEntityService<Exam> baseEntityService, IExamService examService) : base(baseEntityService)
		{
			_examService = examService;
		}
		[HttpGet]
		[Route("contest={contestID}")]
		public ActionServiceResult GetExamByID(string contestID)
		{
			StringValues userHeader;
			Request.Headers.TryGetValue("UserID", out userHeader);
			var userID = userHeader.FirstOrDefault().ToString();
			var result = new ActionServiceResult();
			if (userID == null || string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(contestID))
			{
				result.Success = false;
				result.Code = ApplicationCore.Enums.Enumration.Code.NotFound;
			}
			else
			{
				var response = _examService.GetByUserID(userID,contestID);
				result.Data = response;
			}
			return result;

		}
	}
}
