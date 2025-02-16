using M27.MetaBlog.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace M27.MetaBlog.Infra.Data.Configurations;

internal class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Name)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(user => user.Email)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(user => user.Password)
            .HasMaxLength(255)
            .IsRequired();
    }
}