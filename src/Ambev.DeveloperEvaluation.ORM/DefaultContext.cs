using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    //TODO: Implement entities
    // public DbSet<ProductEntity> Products { get; set; }
    // public DbSet<SaleEntity> Sales { get; set; }
    // public DbSet<SaleItemEntity> SaleItems { get; set; }
    // public DbSet<CartEntity> Carts { get; set; }


    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}