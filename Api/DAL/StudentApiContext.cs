using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.DAL
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions options)
            : base(options)
        {
        }

	    public DbSet<Student> Students { get; set; }

	    public DbSet<Course> Courses { get; set; }
	}
}