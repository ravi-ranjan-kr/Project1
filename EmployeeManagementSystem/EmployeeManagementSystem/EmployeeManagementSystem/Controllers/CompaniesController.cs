using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Infrastructure; 
using System.Security.Claims;



namespace EmployeeManagementSystem.Controllers
{
     public class CompaniesController : Controller

    {
       string userName;
        int userId;
        ICRUDRepository<Company, int> _repository; 
        public CompaniesController( ICRUDRepository<Company, int> repository )  => _repository = repository;
        public ActionResult<IEnumerable<Company>> Get()
        {try{
            var items = _repository.GetAll(); 
            return items.ToList();
        }
        catch(Exception ex)
            {
                throw;
            }
    
        }
        //Add the EFCore.SQLServer package 
        //dotnet add package Microsoft.EntityFrameworkCore.SqlServer


         //URL: /api/employees/1
         //try with id parameter values between 1 and 9

         [HttpGet("{id}")]
         public ActionResult<Company> GetDetails(int id)
        {
            try{
            var item = _repository.GetDetails(id);
            if(item==null)
            return NotFound();
            return item;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
         [HttpPost("addnew")]
        public ActionResult<Company> Create(Company emp)
       {
           try{
           if(emp==null)
             return BadRequest();
             _repository.Create(emp);
              return emp;
           }
           catch(Exception ex)
            {
                throw;
            }
    
  
            
       }
        [Microsoft.AspNetCore.Authorization.Authorize()]

       [HttpPut("update/{id}")]
       public ActionResult<Company> update(int id, Company emp)
       {
            userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            userId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if(role!="admin") 
            {
                return Unauthorized();
            }
            if(userId==0) return BadRequest();
           if(emp==null)
             return BadRequest();
             if(id==0)  return BadRequest();
            _repository.update(emp);
            return emp;
       }
       [HttpDelete("remove/{id}")]
       public ActionResult Delete(int id)
       {
           try{
           _repository.Delete(id);

            return Ok();
           }
           catch(Exception ex)
            {
                throw;
            }
    
       }


    }

}