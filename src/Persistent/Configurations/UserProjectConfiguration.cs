using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Model;

namespace Persistent.Configurations
{
    public class UserProjectConfiguration : IEntityTypeConfiguration<UserProject>
    {
        public void Configure(EntityTypeBuilder<UserProject> template)
        {
            template.ToTable("UserProject");
            template.HasIndex(e => e.Id).IsUnique();
            template.Property(e => e.Id).HasColumnName("UserProjectID");
            template.Property(e => e.UserId).HasColumnName("UserID");
            template.Property(e => e.ProjectId).HasColumnName("ProjectID");
            template.HasOne(x => x.User).WithMany(x=>x.UserProjects).OnDelete(DeleteBehavior.NoAction);
            template.HasOne(x => x.Project).WithMany(x=>x.UserProjects).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
