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
    public class UsersController : Controller
    {
        string userName;
        int userId;
        ICRUDRepository<User, int> _repository;
        public UsersController(ICRUDRepository<User, int> repository) => _repository = repository;
        public ActionResult<IEnumerable<User>> Get()
        {
            var items = _repository.GetAll();
            return items.ToList();
        }

        // [HttpGet("Users")]
        // public Models.User GetUser()
        // {
        //     Models.User obj = new Models.User{
        //         Id=1,
        //         UserName="Jyo",
        //         UserAddress="Banglore",
        //         UserType="No",
        //         Password="AH6SKJ",
        //     };
        //    return obj;  
        // } 

             [HttpGet("{id}")]
           public ActionResult<User> GetDetails(int id)
           {
               var item =_repository.GetDetails(id);
               if(item==null)
               return NotFound();

               return item;

           }
            [Microsoft.AspNetCore.Authorization.Authorize()]
           [HttpPost("register")]
           public ActionResult<User> Create(User usr)
           {
            userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            userId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if(role!="newemployee") 
            {
                return Unauthorized();
            }
            if(userId==0) return BadRequest();
               if(usr==null)
               return BadRequest();

               _repository.Create(usr);
               return usr;
    
               
           } 

           [HttpPut("update/{id}")]
           public ActionResult<User> update(int id,User usr)
           {
               if(usr==null)
               return BadRequest();
                if(id==0)  return BadRequest();
               _repository.update(usr);
               return usr;

           }
            [HttpDelete("remove/{id}")]
           public ActionResult Delete(int id)
           {
            userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            userId = Convert.ToInt32("0" + HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = Convert.ToString(HttpContext.User.FindFirst(ClaimTypes.Role).Value);
            if(role!="admin") 
            {
                return Unauthorized();
            }
            if(userId==0) return BadRequest();
               _repository.Delete(id);
               return Ok();
           }

        
    }
}