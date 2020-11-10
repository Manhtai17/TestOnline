using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository.Interfaces
{
	public interface ITermRepository
	{
		IEnumerable<Term> Paging(string userID, int pageIndex, int pageSize,string keyword);
	}
}
