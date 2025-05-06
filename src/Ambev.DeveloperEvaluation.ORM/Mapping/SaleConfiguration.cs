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
            .HasForeignKey(s => s.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}