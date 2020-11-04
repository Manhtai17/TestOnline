using ApplicationCore.Entity;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;
using TestOnline.Interfaces;

namespace TestOnline.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TermsController : BaseController<Term>
	{
		private readonly ITermService _termService;
		private readonly IContestService _contestService;
		public TermsController(IBaseEntityService<Term> baseEntityService, ITermService termService, IContestService contestService) : base(baseEntityService)
		{
			_termService = termService;
			_contestService = contestService;
		}
		[HttpGet]
		[Route("paging/{index}/{size}")]
		public ActionServiceResult Paging(int index, int size)
		{
			StringValues userHeader;
			Request.Headers.TryGetValue("UserID", out userHeader);
			var userID = userHeader.FirstOrDefault().ToString();
			var result = new ActionServiceResult();
			if (userID == null || string.IsNullOrEmpty(userID))
			{
				result.Success = false;
				result.Code = ApplicationCore.Enums.Enumration.Code.NotFound;
			}
			else
			{
				var response = _termService.Paging(userID, index, size);
				result.Data = response;
			}
			return result;

		}

		[HttpGet]
		[Route("{termID}/Contests/")]
		public ActionServiceResult GetContestsByTermID(string termID)
		{
			StringValues userHeader;
			Request.Headers.TryGetValue("UserID", out userHeader);
			var userID = userHeader.FirstOrDefault().ToString();
			var result = new ActionServiceResult();
			if (userID == null || string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(termID))
			{
				result.Success = false;
				result.Code = ApplicationCore.Enums.Enumration.Code.NotFound;
			}
			else
			{
				var response = _contestService.GetByTermID(termID);
				result.Data = response;
			}
			return result;

		}
	}
}
