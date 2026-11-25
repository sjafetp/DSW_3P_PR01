using Microsoft.EntityFrameworkCore;
using DSW_3P_PR01.Models;
using System.Data.Common;

namespace DSW_3P_PR01.Data;

public class PostgreSqlDbContext : DbContext
{
    public PostgreSqlDbContext(
        DbContextOptions<PostgreSqlDbContext> options
    ) : base(options)
    {
    }

    public DbSet<Mision> Misiones => Set<Mision>();
}