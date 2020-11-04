using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestOnline.Interfaces
{
	public interface ITermService
	{
		public IEnumerable<Term> Paging(string userID, int pageIndex, int pageSize);
	}
}
