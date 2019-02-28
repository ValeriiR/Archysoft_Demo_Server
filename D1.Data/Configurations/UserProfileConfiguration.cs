
using D1.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace D1.Data.Configurations
{
    public class UserProfileConfiguration:IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfiles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.BirthDate).IsRequired().HasMaxLength(255);
            builder.Property(x => x.BirthDate).IsRequired();

            builder.HasOne(u => u.User).WithOne(u => u.Profile).HasForeignKey<UserProfile>(u => u.UserId);
        }
    }
}
