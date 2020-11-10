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
		private readonly IBaseRepository<Contest> _baseRepository;

		public ContestService(IContestRepository contestRepo, IBaseRepository<Contest> baseRepository)
		{
			_contestRepo = contestRepo;
			_baseRepository = baseRepository;
		}

		public Task<IEnumerable<Contest>> GetByTermID(string termID, int indexPage, int sizePage,string keyword)
		{
			var result = _contestRepo.GetByTermID(termID,indexPage,sizePage,keyword);
			return Task.FromResult(result);
		}

		public Task<int> GetTotalRecords(string termID,string keyword)
		{
			var result = _baseRepository.GetTotalRecords("Proc_GetTotalContestRecords", new object[] { termID ,keyword});
			return Task.FromResult(result);
		}
	}
}
