using MySql.Data.MySqlClient;

namespace Infrastructure.DatabaseContext
{
	public interface IDatabaseContext
	{
		MySqlConnection Connection { get; }

	}
}
