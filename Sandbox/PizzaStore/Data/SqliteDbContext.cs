using Microsoft.EntityFrameworkCore;
using PizzaStore.Models;

namespace PizzaStore;

public class SqliteDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Pizza> Pizzas => Set<Pizza>();
}
