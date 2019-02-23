using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Data;
using Api.DAL;
using Api.Models;
using Microsoft.AspNetCore.Cors;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
	    private readonly StudentUnitOfWork _unitOfWork;
		public StudentController(ApiContext context)
	    {
			_unitOfWork = new StudentUnitOfWork(context);
		}
        // GET api/Student
        [HttpGet]
        [EnableCors("AllowAllOrigins")]
		public IEnumerable<Student> GetStudents()
        {
	        return _unitOfWork.Students.GetAll();
        }

        // GET api/Student/5
        [HttpGet("{id}")]
        [EnableCors("AllowAllOrigins")]
		public Student GetStudent(string id)
        {
	        return _unitOfWork.Students.Get(new Guid(id));
		}

        // POST api/Student
        [HttpPost]
        [EnableCors("AllowAllOrigins")]
		public void AddStudent([FromBody] Student student)
        {
	        _unitOfWork.Students.Add(student);
	        _unitOfWork.Complete();
		}

        // PUT api/Student/5
        [HttpPut("{id}")]
        [EnableCors("AllowAllOrigins")]
		public void UpdateStudent([FromBody] Student student)
        {
	        _unitOfWork.Students.Update(student);
			_unitOfWork.Complete();
        }

        // DELETE api/Student/5
        [HttpDelete("{id}")]
        [EnableCors("AllowAllOrigins")]
		public void DeleteStudent(string id)
        {
	        var account = _unitOfWork.Students.Get(new Guid(id));
			_unitOfWork.Students.Remove(account);
	        _unitOfWork.Complete();
		}
    }
}