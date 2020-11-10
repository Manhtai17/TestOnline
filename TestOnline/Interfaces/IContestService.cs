using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestOnline.Interfaces
{
	public interface IContestService
	{
		Task<IEnumerable<Contest>> GetByTermID(string termID, int indexPage, int sizePage,string keyword);
		Task<int> GetTotalRecords(string userID,string keyword);
	}
}
