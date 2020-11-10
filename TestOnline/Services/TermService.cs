using ApplicationCore.Entity;
using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestOnline.Interfaces;

namespace TestOnline.Services
{
	public class TermService : ITermService
	{
		private readonly ITermRepository _termRepo;
		private readonly IBaseRepository<Term> _baseRepository;

		public TermService(ITermRepository termRepo, IBaseRepository<Term> baseRepository)
		{
			_termRepo = termRepo;
			_baseRepository = baseRepository;
		}

		public Task<IEnumerable<Term>> Paging(string userID, int pageIndex, int pageSize,string keyword)
		{
			var result = _termRepo.Paging(userID, pageIndex, pageSize,keyword);
			return Task.FromResult(result);
		}

		public Task<int> GetTotalRecords(string userID, string keyword)
		{
			var result = _baseRepository.GetTotalRecords("Proc_GetTotalTermRecords", new object[] { userID,keyword });
			return Task.FromResult(result);
		}
	}
}
