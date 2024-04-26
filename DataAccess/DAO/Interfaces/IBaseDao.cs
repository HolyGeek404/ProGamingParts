using Microsoft.Data.SqlClient;

namespace DataAccess.DAO.Interfaces;

public interface IBaseDao
{
    SqlConnection DbConnection { get; }
}