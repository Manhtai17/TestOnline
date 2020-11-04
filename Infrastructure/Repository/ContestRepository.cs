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
		public IEnumerable<Contest> GetByTermID(string termID)
		{
			var result = (GetEntitites("Proc_GetContestsByTermID", new object[] {termID })).Result;
			return result;
		}
	}
}
