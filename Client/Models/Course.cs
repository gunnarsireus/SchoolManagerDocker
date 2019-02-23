using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Client.Models
{
	public class Course
	{
		public Course()
		{
			CreationTime = DateTime.Now.ToString(new CultureInfo("en-US"));
		}
		public Guid Id { get; set; }

		[Display(Name = "Created date")]
		public string CreationTime { get; set; }
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Classroom")]
		public string Classroom { get; set; }

		public ICollection<Student> Students { get; set; }
	}
}
