using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudnetsController : ControllerBase
     {
         private static List<Student> _students = new List<Student>();
         private readonly IDbService _dbService;

         public StudnetsController(IDbService dbService)
         {
             _dbService = dbService;
         }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbService.GetStudents());
        }

        [HttpPost]
        //create new
        public IActionResult Post(Student newStudent)
        {
            
            newStudent.IndexNumber = $"s{new Random().Next(1,20000)}";
            newStudent.IdStudent = _students.Count;
            _students.Add(newStudent);
            return Ok(newStudent);
        }

        [HttpDelete]
        //delete istniejącego
        public IActionResult Delete([FromQuery]int id)
        {
            if (_students.Any(e => e.IdStudent == id))
            {
                _students.RemoveAll(p => p.IdStudent == id);
                return Ok($"Removed student {id}");
            }

            return NotFound($"Student {id} not found");
        }

        [HttpPut]
        //update istniejącego
        public IActionResult Put([FromQuery] int id, Student student)
        {
            
            if (_students.Any(e => e.IdStudent == id))
            {
                //put == delete -> insert
                _students.RemoveAll(p => p.IdStudent == id);

                student.IdStudent = id;
               
                _students.Add(student);
                
                return Ok($"Updated student {id}");
            }

            return NotFound($"Student {id} does not exist");

        }

        
        
     }
}
