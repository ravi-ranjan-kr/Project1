using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Infrastructure;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentsController : Controller
    {
        ICRUDRepository<Department, int> _repository; 
        public DepartmentsController( ICRUDRepository<Department, int> repository )  => _repository = repository;
        public ActionResult<IEnumerable<Department>> Get()
        {
            var items = _repository.GetAll(); 
            return items.ToList();
        }

        //Add the EFCore.SQLServer package 
        //dotnet add package Microsoft.EntityFrameworkCore.SqlServer


         //URL: /api/employees/1
         //try with id parameter values between 1 and 9

          [HttpGet("{id}")]
         public ActionResult<Department> GetDetails(int id)
        {
            var item = _repository.GetDetails(id);
            if(item==null)
            return NotFound();

            return item; 
        }
 
         [HttpPost("addnew")]
        public ActionResult<Department> Create(Department dept)
       {
           if(dept==null)
           {
             return BadRequest();

           }
            _repository.Create(dept);
            return dept;
  
            
       }

       [HttpPut("update/{id}")]
       public ActionResult<Department> update(int id, Department dept)
       {
           if(dept==null)
             return BadRequest();
         if(id==0) return BadRequest();
        _repository.update(dept);
            return dept;
       }

       [HttpDelete("remove/{id}")]
       public ActionResult<Department> Delete(int id)
       {
           _repository.Delete(id);

            return Ok();
       }

    }
}