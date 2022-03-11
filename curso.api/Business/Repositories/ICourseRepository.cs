using curso.api.Business.Entities;
using System.Collections.Generic;

namespace curso.api.Business.Repositories
{
    public interface ICourseRepository
    {
        void Add(Course course);

        void Commit();

        IList<Course> GetByUser(int UserId);
    }
}
