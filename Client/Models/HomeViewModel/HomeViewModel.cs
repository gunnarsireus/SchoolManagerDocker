using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.Models.HomeViewModel
{
    public class HomeViewModel : Student
    {
	    public List<Course> Courses { get; set; }
	}
}