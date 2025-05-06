using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItemEntity>
{
    public void Configure(EntityTypeBuilder<SaleItemEntity> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(c => c.Id);

        builder.Property(si => si.CustomerId)
            .IsRequired();

        builder.Property(si => si.Quantity)
            .IsRequired();
        builder.Property(si => si.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");
        builder.Property(si => si.Discount)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");
        builder.Property(si => si.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");

        builder.HasOne(si => si.Product)
            .WithMany()
            .HasForeignKey(si => si.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Customer)
            .WithMany()
            .HasForeignKey(s => s.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}