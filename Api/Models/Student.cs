using System;
using System.Globalization;

namespace Api.Models
{
    public class Student
	{
		public Student()
		{
			CreationTime = DateTime.Now.ToString(new CultureInfo("se-SE"));
			Present = true;
		}
		public Guid Id { get; set; }
		public Guid CourseID { get; set; }
		public string CreationTime { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool Present { get; set; }
		public bool Disabled { get; set; } //Used to block changes of Present/Absent status
	}
}
