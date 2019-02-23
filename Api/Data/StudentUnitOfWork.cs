using Api.DAL;

namespace Api.Data
{
    public class StudentUnitOfWork:IStudentUnitOfWork
    {
	    private readonly ApiContext _context;

	    public StudentUnitOfWork(ApiContext context)
	    {
		    _context = context;
		    Students = new StudentRepository(_context);
		    Courses = new CourseRepository(_context);
		}

	    public void Dispose()
	    {
		   _context.Dispose();
	    }

	    public IStudentRepository Students { get; private set; }
	    public ICourseRepository Courses { get; private set; }
		public int Complete()
		{
			return _context.SaveChanges();
		}
    }
}
