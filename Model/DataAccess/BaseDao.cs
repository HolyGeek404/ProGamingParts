using Microsoft.EntityFrameworkCore;
using Model.Contexts;

namespace Model.DataAccess;

public class BaseDao(DbContextOptions<PGPContext> options)
{
    protected PGPContext PgpContext { get; } = new(options);
}