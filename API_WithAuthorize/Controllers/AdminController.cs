using API_WithAuthorize.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WithAuthorize.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase

    {
        private readonly ApplicationDBContext _dbContext;
        
        public AdminController(ApplicationDBContext dBContext) {
        
            _dbContext = dBContext;
        }
       [Authorize]
        [HttpGet]     
        public IActionResult StudentList()
        {
            var student = _dbContext.Students.ToList();
            return Ok(student);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddStudent([FromForm] StudentDTO obj)
        {
            Student st = new Student
            {
              
                Name = obj.Name,
                Age = obj.Age,
                Grade = obj.Grade,
            };
            _dbContext.Students.Add(st);

            int i = _dbContext.SaveChanges();
            if (i > 0)
            {
                return Ok(new { Message = "Add New Successfully!!" });
            }
            else
            {
                return NotFound();
            }

        }
        [Authorize]
        [HttpDelete]
        public IActionResult AddDelete(Guid Id)
        {
            var data = _dbContext.Students.Where(x => x.Id == Id).FirstOrDefault();
            if (data == null)
            {
                NotFound(new { Message = "Student Deleted !!" });
            }
            _dbContext.Students.Remove(data);
            int i = _dbContext.SaveChanges();
           
                return Ok(new { Message = "Deleted Successfully!!" });



        }
        [Authorize]
        [HttpPut]
        public IActionResult EditStudent([FromForm] Student obj)
        {
            _dbContext.Students.Update(obj);
            _dbContext.SaveChanges();

            return Ok(new { Message = "Update Successfully!!" });
        }
       
    }
}
