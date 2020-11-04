using ApplicationCore.Entity;
using Infrastructure.Repository.Interfaces;
using System.Collections.Generic;

namespace Infrastructure.Repository
{
	public class TermRepository : BaseRepository<Term>, ITermRepository
	{
		public IEnumerable<Term> Paging(string userID, int pageIndex, int pageSize)
		{
			var result = GetEntitites("Proc_GetTermsPaging", new object[] { userID, pageIndex, pageSize }).Result;
			return result;
		}
	}
}
