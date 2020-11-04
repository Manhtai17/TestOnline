using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestOnline.Interfaces
{
	public interface IContestService
	{
		IEnumerable<Contest> GetByTermID(string userID);
	}
}
