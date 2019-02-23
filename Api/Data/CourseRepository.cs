using Api.DAL;
using Api.Models;


namespace Api.Data
{
    public class CourseRepository : Repository<Course>, ICourseRepository
	{
		public CourseRepository(ApiContext context) : base(context)
		{
		}

		public ApiContext ApiContext => Context as ApiContext;

	}
}
