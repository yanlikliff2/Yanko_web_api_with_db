using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yanko_web3_v2.Models;

namespace Yanko_web3_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public PractDbContext Context { get; }

        public UserController(PractDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<UserTable> users = Context.UserTables.ToList();
            return Ok(users);
        }
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
        [HttpPost]
        public IActionResult Add(string userName, string email, string password, int roleId)
        {
            UserTable userTable = new UserTable() { Username = userName, Password = password, Email = email, /*RoleId = roleId*/ };
            Context.UserTables.Add(userTable);
            Context.SaveChanges();
            return Ok(userTable);
        }
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

