using System;
using System.Collections.Generic;
using System.Globalization;

namespace Api.Models
{
	public class Course
	{
		public Course()
		{
			CreationTime = DateTime.Now.ToString(new CultureInfo("se-SE"));
		}
		public Guid Id { get; set; }

		public string CreationTime { get; set; }
		public string Name { get; set; }

		public string Classroom { get; set; }

		public ICollection<Student> Students { get; set; }
	}
}
