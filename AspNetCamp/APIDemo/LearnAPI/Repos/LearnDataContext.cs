using System;
using System.Collections.Generic;
using LearnAPI.Repos.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Repos;

public partial class LearnDataContext : DbContext
{
    public LearnDataContext()
    {
    }

    public LearnDataContext(DbContextOptions<LearnDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<TodoItem> TodoItems { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=192.168.0.8,59273;Database=ViVaKR;User ID=ViVaKR;Password=B9037!m8947#;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
