using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yanko_web3_v2.Models;

namespace Yanko_web3_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public PractDbContext Context { get; }

        public RoleController(PractDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Role> users = Context.Roles.ToList();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Role? roleTable = Context.Roles.Where(x => x.RoleId == id).FirstOrDefault();
            if (roleTable == null)
            {
                return BadRequest("Not found");
            }
            return Ok(roleTable);
        }
        [HttpPut]
        public IActionResult Update(int id, string newRole)
        {
            Role? roleTable = Context.Roles.Where(x => x.RoleId == id).FirstOrDefault();
            if (roleTable == null)
            {
                return BadRequest("Not found");
            }
            roleTable.Role1 = newRole;
            Context.SaveChanges();
            return Ok(roleTable);
        }
        [HttpPost]
        public IActionResult Add(string role)
        {
            Role role1 = new Role() { Role1 = role };
            Context.Roles.Add(role1);
            Context.SaveChanges();
            return Ok(role1);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Role? role = Context.Roles.Where(x => x.RoleId == id).FirstOrDefault();
            if (role == null) return BadRequest("Not found");
            Context.Roles.Remove(role);
            Context.SaveChanges();
            return Ok(role);
        }
    }
}

