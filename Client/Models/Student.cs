using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Models
{
	public class Student
	{
		public Student()
		{
			CreationTime = DateTime.Now.ToString(new CultureInfo("en-US"));
			Present = true;
		}
		public Guid Id { get; set; }
		public Guid CourseId { get; set; }

		[Display(Name = "Created date")]
		public string CreationTime { get; set; }

		[Display(Name = "FirstName ")]
		[Remote("FirstNameAvailable", "Student", ErrorMessage = "FirstName taken")]
		public string FirstName { get; set; }

		[Display(Name = "Lastname")]
		[Remote("LastNameAvailable","Student", ErrorMessage = "LastName taken")]
		public string LastName { get; set; }

		[Display(Name = "Status")]
		public bool Present { get; set; }

		[Display(Name = "Present (X) or Absent ()?")]
		public string PresentOrAbsent => (this.Present)?"Present":"Absent";

		public bool Disabled { get; set; } //Used to block changes of Present/Absent status
	}
}
