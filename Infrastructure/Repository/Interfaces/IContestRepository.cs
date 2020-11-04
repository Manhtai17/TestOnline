using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository.Interfaces
{
	public interface IContestRepository
	{
		IEnumerable<Contest> GetByTermID(string userID);
	}
}
