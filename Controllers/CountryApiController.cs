using RegistrationForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RegistrationForm.Controllers
{
    public class CountryApiController : ApiController
    {
        // GET: api/CountryApi
        public List<CountryModel> Get()
        {
            StudentDto sd = new StudentDto();
            ModelState.Clear();
            return sd.GetCountry();            
        }        
    }
}
