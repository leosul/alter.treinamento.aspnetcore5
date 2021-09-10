using alter.treinamento.business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace alter.treinamento.data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(s => s.Description)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(s => s.IsActive)
                .IsRequired()
                .HasColumnType("bit");

            //1 : N => Categories : Products
            builder.HasMany(s => s.Products)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId);

            builder.HasIndex(s => new { s.Description }).IsUnique(false);

            builder.ToTable("Categories");
        }
    }
}
