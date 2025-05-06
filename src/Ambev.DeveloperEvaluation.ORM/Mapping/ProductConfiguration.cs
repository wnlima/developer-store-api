using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(p => p.Description)
            .HasMaxLength(1000);
        builder.Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");
        builder.Property(p => p.QuantityInStock)
            .IsRequired();
    }
}