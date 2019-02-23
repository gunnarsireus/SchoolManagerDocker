using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.DAL
{
    public class StudentDataAccess
    {
        private readonly DbContextOptionsBuilder<ApiContext> _optionsBuilder =
            new DbContextOptionsBuilder<ApiContext>();

        public StudentDataAccess()
        {
            _optionsBuilder.UseSqlite("DataSource=App_Data/School.db");
        }

	    public ICollection<Student> GetStudents()
	    {
		    using (var context = new ApiContext(_optionsBuilder.Options))
		    {
			    return context.Students.ToList();
		    }
	    }

	    public Student GetStudent(Guid id)
	    {
		    using (var context = new ApiContext(_optionsBuilder.Options))
		    {
			    return context.Students.SingleOrDefault(o => o.Id == id);
		    }
	    }

	    public void AddStudent(Student student)
	    {
		    using (var context = new ApiContext(_optionsBuilder.Options))
		    {
			    context.Students.Add(student);
			    context.SaveChanges();
		    }
	    }

	    public void DeleteStudent(Guid id)
	    {
		    using (var context = new ApiContext(_optionsBuilder.Options))
		    {
			    var Student = GetStudent(id);
			    context.Students.Remove(Student);
			    context.SaveChanges();
		    }
	    }

	    public void UpdateStudent(Student student)
	    {
		    using (var context = new ApiContext(_optionsBuilder.Options))
		    {
			    context.Students.Update(student);
			    context.SaveChanges();
		    }
	    }

		public ICollection<Course> GetCourses()
        {
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                return context.Courses.ToList();
            }
        }

        public Course GetCourse(Guid id)
        {
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                return context.Courses.SingleOrDefault(o => o.Id == id);
            }
        }

        public void AddCourse(Course course)
        {
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                context.Courses.Add(course);
                context.SaveChanges();
            }
        }

        public void DeleteCourse(Guid id)
        {
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                var students = GetStudents().Where(c => c.CourseID == id);
                foreach (var student in students)
                {
                    context.Students.Remove(student);
                }

                var course = GetCourse(id);
                context.Courses.Remove(course);
                context.SaveChanges();
            }
        }

        public void UpdateCourse(Course course)
        {
            using (var context = new ApiContext(_optionsBuilder.Options))
            {
                context.Courses.Update(course);
                context.SaveChanges();
            }
        }
    }
}