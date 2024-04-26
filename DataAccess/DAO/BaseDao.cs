using DataAccess.DAO.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DAO;

public class BaseDao(IConfiguration configuration) : IBaseDao
{
    public SqlConnection DbConnection { get; } = new(configuration.GetConnectionString("PcLocalConnection"));
}