using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DatabaseContext
{
	public interface IDatabaseContextFactory
	{
		IDatabaseContext Context();
	}
}
