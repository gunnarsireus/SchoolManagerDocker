using System;
using System.Linq;
using Api.Models;

namespace Api.DAL
{
	public static class ApiExtensions
	{
		public static void EnsureSeedData(this ApiContext context)
		{
			if (!context.Students.Any() || !context.Courses.Any())
			{
				var courseId = Guid.NewGuid();
				context.Courses.Add(new Course() { Id = courseId, Name = "Advanced Mathematics", Classroom = "Room 1" });
				context.Students.Add(new Student
				{
					Id = Guid.NewGuid(),
					CourseID = courseId,
					FirstName = "Anders",
					LastName = "Karlsson"
				});
				context.Students.Add(new Student
				{
					Id = Guid.NewGuid(),
					CourseID = courseId,
					FirstName = "Anna",
					LastName = "Johansson"
				});
				context.Students.Add(new Student
				{
					Id = Guid.NewGuid(),
					CourseID = courseId,
					FirstName = "Bertil",
					LastName = "Karlsson"
				});

				courseId = Guid.NewGuid();
				context.Courses.Add(new Course() { Id = courseId, Name = "English", Classroom = "Room 2" });
				context.Students.Add(new Student
				{
					Id = Guid.NewGuid(),
					CourseID = courseId,
					FirstName = "Allan",
					LastName = "Lie"
				});
				context.Students.Add(new Student
				{
					Id = Guid.NewGuid(),
					CourseID = courseId,
					FirstName = "Jan",
					LastName = "Rasmussen"
				});

				courseId = Guid.NewGuid();
				context.Courses.Add(new Course() { Id = courseId, Name = "Geography", Classroom = "Room 3" });
				context.Students.Add(new Student
				{
					Id = Guid.NewGuid(),
					CourseID = courseId,
					FirstName = "Sören",
					LastName = "Larssen"
				});
				context.Students.Add(new Student
				{
					Id = Guid.NewGuid(),
					CourseID = courseId,
					FirstName = "Karl",
					LastName = "Nilsson"
				});
			}
            else
            {
                foreach (var car in context.Students)
                {
                    car.Disabled = false;
                }
            }
            context.SaveChanges();
        }
	}
}
