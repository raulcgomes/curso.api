using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace curso.api.Infrastructure.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseDbContext _context;

        public CourseRepository(CourseDbContext context)
        {
            _context = context;
        }

        public void Add(Course course)
        {
            _context.Course.Add(course);
        }

        public void Commit()
        {
            _context.SaveChanges(); 
        }

        public IList<Course> GetByUser(int UserId)
        {
            return _context.Course.Include(i => i.User).Where(w => w.CourseId == UserId).ToList();   
        }
    }
}
