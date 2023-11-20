using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.DAL
{
    public class TodoDataAccessLayer
    {
        EmployeeDBContext db = new EmployeeDBContext();

        //To Get all todos 
        public List<Todo> GetAllTodos()
        {
            try
            {
                return db.TodoRecord.Find(_ => true).ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new todo record
        public void AddTodo(Todo todo)
        {
            try
            {
                db.TodoRecord.InsertOne(todo);
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular todo
        public Todo GetTodoData(string id)
        {
            try
            {
                FilterDefinition<Todo> filterEmployeeData = Builders<Todo>.Filter.Eq("Id", id);

                return db.TodoRecord.Find(filterEmployeeData).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particular todo
        public void UpdateTodo(Todo todo)
        {
            try
            {
                db.TodoRecord.ReplaceOne(filter: g => g.Id == todo.Id, replacement: todo);
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular todo
        public void DeleteTodo(string id)
        {
            try
            {
                FilterDefinition<Todo> employeeData = Builders<Todo>.Filter.Eq("Id", id); db.TodoRecord.DeleteOne(employeeData);
            }
            catch
            {
                throw;
            }
        }

    }
}


