using Microsoft.EntityFrameworkCore;

namespace Utility;

public class UtilityContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ColorTable> ColorTables => Set<ColorTable>();
}


