using ApplicationCore.Entity;
using Infrastructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
	public class ContestRepository : BaseRepository<Contest>,IContestRepository
	{
		public IEnumerable<Contest> GetByTermID(string termID, int indexPage, int sizePage,string keyword)
		{
			var result = (GetEntitites("Proc_GetContestsByTermID", new object[] {termID,indexPage,sizePage,keyword })).Result;
			return result;
		}
	}
}
