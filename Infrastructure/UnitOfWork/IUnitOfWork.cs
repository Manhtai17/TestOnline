using MySql.Data.MySqlClient;
using Infrastructure.DatabaseContext;
using System;

namespace Infrastructure.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IDatabaseContext DataContext { get; }
		MySqlTransaction BeginTransaction();
		void Commit();
		void RollBack();
	}
}
