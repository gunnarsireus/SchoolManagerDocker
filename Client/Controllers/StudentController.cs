using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Client.Models;
using Client.Models.StudentViewModel;

namespace Client.Controllers
{
	public class StudentController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		public StudentController(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}

		// GET: Student
		public async Task<IActionResult> Index(string id)
		{
			if (!_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Home");
			var courses = await Utils.Get<List<Course>>("api/Course");

			if (courses.Any() && id == null)
				id = courses[0].Id.ToString();
			var selectList = new List<SelectListItem>
			{
				new SelectListItem
				{
					Text = "Choose course",
					Value = ""
				}
			};
			selectList.AddRange(courses.Select(course => new SelectListItem
			{
				Text = course.Name,
				Value = course.Id.ToString(),
				Selected = course.Id.ToString() == id
			}));
			var students = new List<Student>();

			if (id != null)
			{
				students = await Utils.Get<List<Student>>("api/Student");
				var courseId = new Guid(id);
				students = students.Where(o => o.CourseId == courseId).ToList();
			}

			var studentListViewModel = new StudentListViewModel()
			{
				CourseSelectList = selectList,
				Students = students
			};

			ViewBag.CourseId = id;
			return View(studentListViewModel);
		}

		// GET: Student/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			var student = await Utils.Get<Student>("api/Student/" + id);
			var course = await Utils.Get<Course>("api/Course/" + student.CourseId);
			ViewBag.CourseName = course.Name;
			return View(student);
		}

		// GET: Student/Create
		public async Task<IActionResult> Create(string id)
		{
			var courseId = new Guid(id);
			var student = new Student
			{
				CourseId = courseId,
			};
			var course = await Utils.Get<Course>("api/Course/" + courseId);
			ViewBag.CourseName = course.Name;
			return View(student);
		}

		// POST: Student/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("CourseId,FirstName,LastName,Present")] Student student)
		{
			if (!ModelState.IsValid) return View(student);
			student.Id = Guid.NewGuid();
			await Utils.Post<Student>("api/Student/", student);

			return RedirectToAction("Index", new { id = student.CourseId });
		}

		// GET: Student/Edit/5
		public async Task<IActionResult> Edit(Guid id)
		{
			var student = await Utils.Get<Student>("api/Student/" + id);
			student.Disabled = true; //Prevent updates of Present/Absent while editing
			await Utils.Put<Student>("api/student/" + id, student);
			var course = await Utils.Get<Course>("api/Course/" + student.CourseId);
			ViewBag.CourseName = course.Name;
			return View(student);
		}

		// POST: Student/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id, Present")] Student student)
		{
			if (!ModelState.IsValid) return View(student);
			var oldStudent = await Utils.Get<Student>("api/Student/" + student.Id);
			oldStudent.Present = student.Present;
			oldStudent.Disabled = false; //Enable updates of Present/Absent when editing done
			await Utils.Put<Student>("api/Student/" + oldStudent.Id, oldStudent);

			return RedirectToAction("Index", new { id = oldStudent.CourseId });
		}

		// GET: Student/Delete/5
		public async Task<IActionResult> Delete(Guid id)
		{
			var student = await Utils.Get<Student>("api/Student/" + id);
			return View(student);
		}

		// POST: Student/Delete/5
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var student = await Utils.Get<Student>("api/Student/" + id);
			await Utils.Delete<Student>("api/Student/" + id);
			return RedirectToAction("Index", new { id = student.CourseId });
		}

		public async Task<bool> LastNameAvailable(string lastName)
		{
			var students = await Utils.Get<List<Student>>("api/Student");
			return students.All(c => c.LastName != lastName);
		}

		public async Task<bool> FirstNameAvailable(string firstName)
		{
			var students = await Utils.Get<List<Student>>("api/Student");
			return students.All(c => c.FirstName != firstName);
		}
	}
}