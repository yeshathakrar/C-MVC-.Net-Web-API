using RegistrationForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RegistrationForm.Controllers
{
    public class StudentApiController : ApiController
    {
        // GET: api/StudentApi
        public List<StudentModel> Get()
        {            
            StudentDto sd = new StudentDto();
            return sd.GetStudent();
            //return new string[] { "value1", "value2" };
        }

        // GET: api/StudentApi/5
        public StudentModel Get(int id)
        {
            StudentDto sd = new StudentDto();
            return sd.GetStudent().Find(student => student.Id == id);
            //return "value";
        }

        // POST: api/StudentApi
        public IHttpActionResult Post(StudentModel student)
        {
            StudentDto sd = new StudentDto();
            student.Country = student.Country.Replace("1", "India").Replace("2", "USA").Replace("3", "Netherlands");
            //bool response = sd.AddStudent(student);
            if(sd.AddStudent(student))
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT: api/StudentApi/5
        public IHttpActionResult Put(StudentModel student)
        {
            StudentDto sd = new StudentDto();
            if (sd.GetStudent().Find(stud => stud.Id == student.Id) == null)
            {
                return BadRequest("Not found");
            }
            else
            {
                if (sd.UpdateStudent(student))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        // DELETE: api/StudentApi/5
        public IHttpActionResult Delete(int id)
        {
            StudentDto sd = new StudentDto();
            if (sd.GetStudent().Find(student => student.Id == id) == null)
            {
                return BadRequest("Not found");
            }
            else
            {
                if (sd.DeleteStudent(id))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
