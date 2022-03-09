using curso.api.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace curso.api.Infrastructure.Data.Mappings
{
    public class CourseMapping : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("TB_COURSE");
            builder.HasKey(c => c.CourseId);
            builder.Property(c => c.CourseId).ValueGeneratedOnAdd();
            builder.Property(c => c.Name);
            builder.Property(c => c.Description);
            builder.HasOne(c => c.User).WithMany().HasForeignKey(fk => fk.UserId);
        }
    }
}
