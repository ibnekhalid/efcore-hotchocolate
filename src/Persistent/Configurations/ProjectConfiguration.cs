using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Model;

namespace Persistent.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> template)
        {
            template.ToTable("Project");
            template.HasIndex(e => e.Id).IsUnique();
            template.Property(e => e.Id).HasColumnName("ProjectID");
            template.Property(e => e.CompanyId).HasColumnName("CompanyId");
            template.Property(e => e.Status).HasConversion(s => (byte)s, s => (Status)s);
            template.Property(e => e.Title).HasMaxLength(20);

            template.HasMany(e => e.WorkItems).WithOne(x=>x.Project).HasForeignKey(x=>x.ProjectId).OnDelete(DeleteBehavior.Cascade);
            // template.Navigation(x => x.Users);

            //template
            //     .HasMany(x => x.Users)
            //     .WithMany(x => x.UserProjects)
            //     .UsingEntity<UserProject>(
            //         ba => ba
            //             .HasOne(e => e.User)
            //             .WithMany()
            //             .HasForeignKey(e => e.UserId),
            //         ba => ba
            //             .HasOne(e => e.Project)
            //             .WithMany()
            //             .HasForeignKey(e => e.ProjectId));
        }
    }
}
