using curso.api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace curso.api.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CourseDbContext>
    {
        public CourseDbContext CreateDbContext(string[] args)
        {
            var optionsBilder = new DbContextOptionsBuilder<CourseDbContext>();
            optionsBilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=DOTNETCOURSE;Trusted_Connection=True;");
            CourseDbContext context = new CourseDbContext(optionsBilder.Options);

            return context;
        }
    }
}
