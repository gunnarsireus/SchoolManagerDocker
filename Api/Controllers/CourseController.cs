using System;
using System.Collections.Generic;
using Api.Data;
using Api.DAL;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
	    private readonly StudentUnitOfWork _unitOfWork;
		public CourseController(ApiContext context)
	    {
			_unitOfWork = new StudentUnitOfWork(context);
		}
        // GET api/Course
        [HttpGet]
        public IEnumerable<Course> GetCourses()
        {
	        return _unitOfWork.Courses.GetAll();
        }

        // GET api/Course/5
        [HttpGet("{id}")]
        public Course GetCourse(string id)
        {
	        return _unitOfWork.Courses.Get(new Guid(id));
		}

        // POST api/Course
        [HttpPost]
        public void AddCourse([FromBody] Course company)
        {
	        _unitOfWork.Courses.Add(company);
	        _unitOfWork.Complete();
		}

        // PUT api/Course/5
        [HttpPut("{id}")]
        public void UpdateCourse([FromBody] Course company)
        {
	        _unitOfWork.Courses.Update(company);
			_unitOfWork.Complete();
        }

        // DELETE api/Course/5
        [HttpDelete("{id}")]
        public void DeleteCourse(string id)
        {
	        var account = _unitOfWork.Courses.Get(new Guid(id));
			_unitOfWork.Courses.Remove(account);
	        _unitOfWork.Complete();
		}
    }
}