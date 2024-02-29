using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CourseRepository : Repo<CourseEntity>
{
    public CourseRepository(DataContext context) : base(context)
    {
    }
}
