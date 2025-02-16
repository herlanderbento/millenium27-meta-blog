using M27.MetaBlog.Domain.Entity;
using M27.MetaBlog.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace M27.MetaBlog.Infra.Data.Configurations;

internal class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
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
        
        var slugConverter = new ValueConverter<Slug, string>(
            slug => slug.Value, 
            value => Slug.Create(value)
        );

        builder.Property(post => post.Slug)
            .HasMaxLength(255)
            .IsRequired()
            .HasConversion(slugConverter);
        
        var imageConverter = new ValueConverter<Image, string>(
            image => image.Path,
            path => new Image(path)
        );

        builder.Property(post => post.Image)
            .HasColumnName("Image")
            .IsRequired(false)
            .HasConversion(imageConverter);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(post => post.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(post => post.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}