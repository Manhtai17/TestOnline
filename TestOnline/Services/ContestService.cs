using ApplicationCore.Entity;
using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestOnline.Interfaces;

namespace TestOnline.Services
{
	public class ContestService : IContestService
	{
		private readonly IContestRepository _contestRepo;

		public ContestService(IContestRepository contestRepo)
		{
			_contestRepo = contestRepo;
		}

		public IEnumerable<Contest> GetByTermID(string termID)
		{
			var result = _contestRepo.GetByTermID(termID);
			return result;
		}
	}
}
