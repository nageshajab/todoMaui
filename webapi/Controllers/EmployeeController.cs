using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.DAL;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        EmployeeDataAccessLayer objemployee = new EmployeeDataAccessLayer();

        [HttpGet]
        [Route("Employee/Index")]
        public IEnumerable<Employee> Index()
        {
            return objemployee.GetAllEmployees();
        }

        [HttpPost]
        [Route("Employee/Create")]
        public void Create([FromBody] Employee employee)
        {
            objemployee.AddEmployee(employee);
        }

        [HttpGet]
        [Route("Employee/Details/{id}")] 
        public Employee Details(string id) 
        {
            return objemployee.GetEmployeeData(id); 
        }

        [HttpPut]
        [Route("Employee/Edit")] 
        public void Edit([FromBody] Employee employee) 
        { 
            objemployee.UpdateEmployee(employee); 
        }

        [HttpDelete]
        [Route("Employee/Delete/{id}")] 
        public void Delete(string id) 
        { 
            objemployee.DeleteEmployee(id); 
        }

        [HttpGet]
        [Route("Employee/GetCities")] 
        public List<Cities> GetCities() 
        {
            return objemployee.GetCityData(); 
        }
    }
}
