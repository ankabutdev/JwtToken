using Jwt_Token_Learn.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jwt_Token_Learn.DataAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public virtual DbSet<User> Users { get; set; }

}
