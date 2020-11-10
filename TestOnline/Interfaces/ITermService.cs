using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestOnline.Interfaces
{
	public interface ITermService
	{
		 Task<IEnumerable<Term>> Paging(string userID, int pageIndex, int pageSize,string keyword);
		Task<int> GetTotalRecords(string userID, string keyword);
	}
}
