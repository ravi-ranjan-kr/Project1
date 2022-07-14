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
    public class ProjectsController : Controller
    {
      ICRUDRepository<Project, int> _repository; 
      public ProjectsController(ICRUDRepository<Project, int> repository) 
      => _repository = repository;
                public ActionResult<IEnumerable<Project>> Get()
          {
               var items = _repository.GetAll();
               return items.ToList();
          }
     
//  [HttpGet("project")]
//         public Models.Projects GetProject()
//         {
//             Models.Projects obj = new Models.Projects{
//                 ProjectId=1,
//                 ProjectName="R",
//                 ProjectDescription="SSSS",
//             };
//            return obj;  

           [HttpGet("{id}")]
          public ActionResult<Project> GetDetails(int id)
          {
            var item = _repository.GetDetails(id);
            if(item==null)
              return NotFound();
 
              return item;
          }

          [HttpPost("addnew")]

          public ActionResult<Project> Create(Project emp)
          {
            if(emp==null)
              return BadRequest();
              _repository.Create(emp);
              return emp;
          }

          [HttpPut("update/{id}")]

          public ActionResult<Project> update(int id, Project emp)
          {
            if(emp == null)
            return BadRequest();
            if(id==0)   return BadRequest();
            _repository.update(emp);
            return emp;
          }

          [HttpDelete("remove/{id}")]

          public ActionResult Delete(int id)
          {
            _repository.Delete(id);
            return Ok();
          }
        


      //  }
       
    }
}