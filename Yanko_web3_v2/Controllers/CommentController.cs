using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yanko_web3_v2.Authorization;
using Yanko_web3_v2.Entities;
using Yanko_web3_v2.Models;

namespace Yanko_web3_v2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public PractDbContext Context { get; }

        public CommentController(PractDbContext context)
        {
            Context = context;
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<CommentTable> users = Context.CommentTables.ToList();
            return Ok(users);
        }
        [Authorize(Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            CommentTable? comment = Context.CommentTables.Where(x => x.CommentId == id).FirstOrDefault();
            if (comment == null)
            {
                return BadRequest("Not found");
            }
            return Ok(comment);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Update(int id, string newText) // Не вижу смысла менять UserId
        {
            CommentTable? comment = Context.CommentTables.Where(x => x.CommentId == id).FirstOrDefault();
            if (comment == null)
            {
                return BadRequest("Not found");
            }
            comment.CommentText = newText;
            Context.SaveChanges();
            return Ok(comment);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add(string text, int userId, int objectId)
        {
            CommentTable comment = new CommentTable() { CommentText = text, ObjectId = objectId, UserId = userId };
            Context.CommentTables.Add(comment);
            Context.SaveChanges();
            return Ok(comment);
        }
        [Authorize(Role.Admin)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            CommentTable? comment = Context.CommentTables.Where(x => x.CommentId == id).FirstOrDefault();
            if (comment == null) return BadRequest("Not found");
            Context.CommentTables.Remove(comment);
            Context.SaveChanges();
            return Ok(comment);
        }
    }
}
