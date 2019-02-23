using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.Models.StudentViewModel
{
    public class StudentListViewModel : Student
    {
        public List<SelectListItem> CourseSelectList { get; set; }
        public List<Student> Students { get; set; }
    }
}