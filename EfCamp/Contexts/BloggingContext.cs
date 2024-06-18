using Microsoft.EntityFrameworkCore;

namespace EfCamp;

public class BloggingContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
}

// dotnet new class -n BloggingContext -o Contexts
