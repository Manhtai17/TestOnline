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

		public TermService(ITermRepository termRepo)
		{
			_termRepo = termRepo;
		}

		public IEnumerable<Term> Paging(string userID, int pageIndex, int pageSize)
		{
			var result = _termRepo.Paging(userID, pageIndex, pageSize);
			return result;
		}
	}
}
