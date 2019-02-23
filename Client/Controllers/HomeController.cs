using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Client.Models.HomeViewModel;

namespace Client.Controllers
{
	public class HomeController : Controller
	{
		public async Task<IActionResult> Index()
		{
			List<Course> courses;
			try
			{
				courses = await Utils.Get<List<Course>>("api/Course");
			}
			catch (Exception e)
			{
				TempData["CustomError"] = "No contact with server! Api must be started before Client can run!";
				return View(new HomeViewModel { Courses = new List<Course>()});
			}

			var allStudents = await Utils.Get<List<Student>>("api/Student");
            foreach (var student in allStudents)
            {
                student.Disabled = false; //Enable updates of Present/Absent
                await Utils.Put<Student>("api/Student/" + student.Id, student);
            }

            foreach (var course in courses)
			{
				var courseStudents = allStudents.Where(o => o.CourseId == course.Id).ToList();
				course.Students = courseStudents;
			}
			var homeViewModel = new HomeViewModel { Courses = courses };
			return View(homeViewModel);
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}