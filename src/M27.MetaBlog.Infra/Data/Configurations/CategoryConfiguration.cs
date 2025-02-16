using M27.MetaBlog.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace M27.MetaBlog.Infra.Data.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.Id);
        
        builder.Property(category => category.Name)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(category => category.Description)
            .HasMaxLength(4_000)
            .IsRequired();
    }
}