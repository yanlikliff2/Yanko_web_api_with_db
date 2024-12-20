using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yanko_web3_v2.Models;
using Yanko_web3_v2.Authorization;
using Yanko_web3_v2.Entities;
namespace Yanko_web3_v2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public PractDbContext Context { get; }

        public UserController(PractDbContext context)
        {
            Context = context;
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<UserTable> users = Context.UserTables.ToList();
            return Ok(users);
        }
        [Authorize(Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            UserTable? userTable = Context.UserTables.Where(x => x.UserId == id).FirstOrDefault();
            if (userTable == null)
            {
                return BadRequest("Not found");
            }
            return Ok(userTable);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Update(int id, string userName, string email, string password, int roleId)
        {
            UserTable? userTable = Context.UserTables.Where(x => x.UserId == id).FirstOrDefault();
            if (userTable == null)
            {
                return BadRequest("Not found");
            }
            
            userTable.Username = userName; 
            userTable.Password = password; 
            userTable.Email = email; 
            //userTable.RoleId = roleId;
            Context.SaveChanges();
            return Ok(userTable);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add(string userName, string email, string password, int roleId)
        {
            UserTable userTable = new UserTable() { Username = userName, Password = password, Email = email, /*RoleId = roleId*/ };
            Context.UserTables.Add(userTable);
            Context.SaveChanges();
            return Ok(userTable);
        }
        [Authorize(Role.Admin)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            UserTable? user = Context.UserTables.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null) return BadRequest("Not found");
            Context.UserTables.Remove(user);
            Context.SaveChanges();
            return Ok(user);
        }
    }
}

