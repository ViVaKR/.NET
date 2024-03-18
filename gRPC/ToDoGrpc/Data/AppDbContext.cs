using Microsoft.EntityFrameworkCore;

namespace ToDoGrpc;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ToDoItem> ToDoItems { get; set; }

}
