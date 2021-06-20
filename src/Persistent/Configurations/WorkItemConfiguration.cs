using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Model;

namespace Persistent.Configurations
{
	public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
	{
		public void Configure(EntityTypeBuilder<WorkItem> template)
		{
			template.ToTable("WorkItem");
			template.HasIndex(e => e.Id).IsUnique();
			template.Property(e => e.Id).HasColumnName("WorkItemID");
			
			template.Property(e => e.Status).HasConversion(s => (byte)s, s => (Status)s);
			template.Property(e => e.WorkItemType).HasConversion(s => (byte)s, s => (WorkItemType)s);
			template.Property(c => c.ParentId).IsRequired(false);
			template.Property(e => e.Activity).HasConversion(s => (byte)s, s => (Activity)s);


			template.HasMany(c => c.Children).WithOne(c => c.Parent)
				.IsRequired(false)
				.HasForeignKey(c => c.ParentId);

			



		}
	}
}
