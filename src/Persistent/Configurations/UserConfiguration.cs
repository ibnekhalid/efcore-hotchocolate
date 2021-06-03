using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Model;

namespace Persistent.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> template)
        {
            template.ToTable("User");
            template.HasIndex(e => e.Id).IsUnique();
            template.Property(e => e.Id).HasColumnName("UserID");
            template.Property(e => e.CompanyId).HasColumnName("CompanyID");
            template.Property(e => e.Status).HasConversion(s => (byte)s, s => (Status)s);
            template.Property(e => e.Email).HasMaxLength(20);

            //template
            //   .HasMany(x => x.UserProjects)
            //   .WithMany(x => x.Project)
            //   .UsingEntity<UserProject>(
            //       ba => ba
            //           .HasOne(e => e.Project)
            //           .WithMany()
            //           .HasForeignKey(e => e.ProjectId),
            //       ba => ba
            //           .HasOne(e => e.User)
            //           .WithMany()
            //           .HasForeignKey(e => e.UserId));
        }
    }
}
