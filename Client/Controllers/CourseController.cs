using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Client.Models.CourseViewModel;

namespace Client.Controllers
{
	public class CourseController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		public CourseController(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}


		// GET: Course

		public async Task<IActionResult> Index()
		{
			if (!_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Home");
			var courses = await Utils.Get<List<Course>>("api/Course");

			foreach (var course in courses)
			{
				var students = await Utils.Get<List<Student>>("api/Student");
				students = students.Where(c => c.CourseId == course.Id).ToList();
				course.Students = students;
			}

			var courseViewModel = new CourseViewModel { Courses = courses };

			return View(courseViewModel);
		}

		// GET: Course/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			var course = await Utils.Get<Course>("api/Course/" + id);

			return View(course);
		}

		// GET: Course/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Course/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Classroom,CreationTime")] Course course)
		{
			if (!ModelState.IsValid) return View(course);
			course.Id = Guid.NewGuid();
			await Utils.Post<Course>("api/Course/", course);

			return RedirectToAction(nameof(Index));
		}

		// GET: Course/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			var course = await Utils.Get<Course>("api/Course/" + id);
			return View(course);
		}

		// POST: Course/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,CreationTime, Name, Classroom")] Course course)
		{
			if (!ModelState.IsValid) return View(course);
			var oldCourse = await Utils.Get<Course>("api/Course/" + id);

			oldCourse.Name = course.Name;
			oldCourse.Classroom = course.Classroom;
			await Utils.Put<Course>("api/Course/" + oldCourse.Id, oldCourse);

			return RedirectToAction(nameof(Index));
		}

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
		{
			var course = await Utils.Get<Course>("api/Course/" + id);
			return View(course);
		}

        // POST: Course/Delete/5
        [HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			await Utils.Delete<Course>("api/Course/" + id);
			return RedirectToAction(nameof(Index));
		}

		private async Task<bool> CourseExists(Guid id)
		{
			var courses = await Utils.Get<List<Course>>("api/Course");
			return courses.Any(e => e.Id == id);
		}
	}
}