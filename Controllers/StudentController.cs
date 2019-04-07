using RegistrationForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace RegistrationForm.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index(string searchString)
        {
            List<StudentModel> studentList = new List<StudentModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50322/api/");
                var responseTask = client.GetAsync("StudentApi");
                responseTask.Wait();
                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<StudentModel>>();
                    readTask.Wait();
                    studentList = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                if (searchString != null)
                {
                    return View(studentList.Where(s => s.Name.ToLower().Contains(searchString.ToLower())));
                }
                return View(studentList);
            }
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            List<CountryModel> countryList = new List<CountryModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50322/api/");
                var responseTask = client.GetAsync("CountryApi");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<CountryModel>>();
                    readTask.Wait();
                    countryList = readTask.Result;
                    ViewBag.CountryList = countryList;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
                return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(StudentModel student)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50322/api/StudentApi");
                    var postTask = client.PostAsJsonAsync<StudentModel>("StudentApi", student);
                    postTask.Wait();
                    var result = postTask.Result;                    
                    if(result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return View(student);
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            StudentModel student = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50322/api/");
                var responseTask = client.GetAsync("StudentApi?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<StudentModel>();
                    readTask.Wait();
                    student = readTask.Result;
                }
            }
                return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(StudentModel student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50322/api/StudentApi");
                var putTask = client.PutAsJsonAsync<StudentModel>("StudentApi", student);
                putTask.Wait();
                var result = putTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50322/api/");
                var deleteTask = client.DeleteAsync("StudentApi/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");                
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }        
    }
}
