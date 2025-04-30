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
        
        builder.Property(si => si.UserId)
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

        builder.HasOne(si => si.Sale)
            .WithMany(s => s.SaleItems)
            .HasForeignKey(si => si.SaleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(si => si.Product)
            .WithMany()
            .HasForeignKey(si => si.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(si => si.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}