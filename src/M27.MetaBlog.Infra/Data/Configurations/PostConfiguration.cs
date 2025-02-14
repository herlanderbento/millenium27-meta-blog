using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace M27.MetaBlog.Infra.Data.Configurations;

internal class PostConfiguration : IEntityTypeConfiguration<PostModel>
{
    public void Configure(EntityTypeBuilder<PostModel> builder)
    {
        builder.HasKey(post => post.Id);

        builder.Property(post => post.Title)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(post => post.Slug)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(post => post.Description)
            .HasMaxLength(10_000)
            .IsRequired();
        
        builder.Property(post => post.Slug)
            .HasMaxLength(255)
            .IsRequired();


        builder.Property(post => post.ImagePath)
            .HasColumnName("Image")
            .HasMaxLength(500);

        
        builder.HasOne(post => post.Author)
            .WithMany()
            .HasForeignKey(post => post.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(post => post.Category)
            .WithMany()
            .HasForeignKey(post => post.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}