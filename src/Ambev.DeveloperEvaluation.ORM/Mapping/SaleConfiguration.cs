using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<SaleEntity>
{
    public void Configure(EntityTypeBuilder<SaleEntity> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.SaleNumber)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(s => s.SaleDate)
            .IsRequired();
        builder.Property(s => s.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");
        builder.Property(s => s.Branch)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(s => s.IsCancelled)
            .IsRequired();

        builder.HasOne(s => s.Customer)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class CartItemConfiguration : IEntityTypeConfiguration<CartItemEntity>
{
    public void Configure(EntityTypeBuilder<CartItemEntity> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(c => c.Id);
        builder.Property(si => si.UserId)
            .IsRequired();

        builder.HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ci => ci.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(ci => ci.Quantity)
            .IsRequired();

        builder.Property(ci => ci.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");

        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(si => si.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}