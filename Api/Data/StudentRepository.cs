using Api.DAL;
using Api.Models;

namespace Api.Data
{
    public class StudentRepository : Repository<Student>, IStudentRepository
	{
		public StudentRepository(ApiContext context) : base(context)
		{
		}

		public ApiContext ApiContext => Context as ApiContext;

	}
}
