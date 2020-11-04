using ApplicationCore.Entity;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using TestOnline.Interfaces;

namespace TestOnline.Controllers
{
	public class ContestsController : BaseController<Contest>
	{
		private readonly IContestService _contestService;
		public ContestsController(IBaseEntityService<Contest> baseEntityService, IContestService contestService) : base(baseEntityService)
		{
			_contestService = contestService;
		}


		//public IEnumerable<Contest> GetByUserID(string userID)
		//{
		//	var result = _contestRepo.GetByUserID(userID);
		//	return result;
		//}


	}
}
