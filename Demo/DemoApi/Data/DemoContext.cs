using DemoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.Data;

public class DemoContext(DbContextOptions<DemoContext> options)
: DbContext(options)
{
    public DbSet<DemoItem> DemoItems { get; set; } = null!;
}
