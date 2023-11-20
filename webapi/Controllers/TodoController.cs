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
    public class TodoController : Controller
    {
        TodoDataAccessLayer objemployee = new TodoDataAccessLayer();

        [HttpGet]
        [Route("todo/Index")]
        public IEnumerable<Todo> Index()
        {
            return objemployee.GetAllTodos();
        }

        [HttpPost]
        [Route("todo/Create")]
        public void Create([FromBody] Todo employee)
        {
            objemployee.AddTodo(employee);
        }

        [HttpGet]
        [Route("todo/Details/{id}")] 
        public Todo Details(string id) 
        {
            return objemployee.GetTodoData(id); 
        }

        [HttpPut]
        [Route("todo/Edit")] 
        public void Edit([FromBody] Todo employee) 
        { 
            objemployee.UpdateTodo(employee); 
        }

        [HttpDelete]
        [Route("todo/Delete/{id}")] 
        public void Delete(string id) 
        { 
            objemployee.DeleteTodo(id); 
        }

    }
}
