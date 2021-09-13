using alter.treinamento.business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace alter.treinamento.data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Description)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("varchar(300)");

            builder.Property(s => s.Code)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(s => s.Reference)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(s => s.StockBalance)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(s => s.Price)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

            builder.Property(s => s.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            builder.OwnsOne(s => s.Dimension, sx =>
            {
                sx.Property(s => s.Height)
                    .HasColumnName("Height")
                    .HasColumnType("decimal(18, 2)");

                sx.Property(s => s.Width)
                    .HasColumnName("Width")
                    .HasColumnType("decimal(18, 2)");

                sx.Property(s => s.Depth)
                    .HasColumnName("Depth")
                    .HasColumnType("decimal(18, 2)");
            });

            builder.HasIndex(s => new { s.Description }).IsUnique(false);
            builder.HasIndex(s => new { s.Code }).IsUnique(false);
            builder.HasIndex(s => new { s.Code, s.Reference });

            builder.ToTable("Products");
        }
    }
}
