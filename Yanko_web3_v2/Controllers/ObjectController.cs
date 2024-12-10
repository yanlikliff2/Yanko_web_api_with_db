using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Yanko_web3_v2.Models;

namespace Yanko_web3_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectController : ControllerBase
    {
        public PractDbContext Context { get; }

        public ObjectController(PractDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ObjectTable> users = Context.ObjectTables.ToList();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ObjectTable? objectTable = Context.ObjectTables.Where(x => x.ObjectId == id).FirstOrDefault();
            if (objectTable == null)
            {
                return BadRequest("Not found");
            }
            return Ok(objectTable);
        }
        [HttpPut]
        public IActionResult Update(int id, string link, string? description, int autorId, int? collectionId)
        {
            ObjectTable? objectTable = Context.ObjectTables.Where(x => x.ObjectId == id).FirstOrDefault();
            if (objectTable == null)
            {
                return BadRequest("Not found");
            }
            objectTable.Link = link;
            objectTable.ObjectDescription = description;
            objectTable.AuthorId = autorId;
            objectTable.CollectionId = collectionId;
            Context.SaveChanges();
            return Ok(objectTable);
        }
        [HttpPost]
        public IActionResult Add(string link, string? description, int autorId, int? collectionId)
        {
            ObjectTable objectTable = new ObjectTable() { Link = link, ObjectDescription = description,
                AuthorId = autorId, CollectionId = collectionId
            };
            Context.ObjectTables.Add(objectTable);
            Context.SaveChanges();
            return Ok(objectTable);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            ObjectTable? objtb = Context.ObjectTables.Where(x => x.ObjectId == id).FirstOrDefault();
            if (objtb == null) return BadRequest("Not found");
            Context.ObjectTables.Remove(objtb);
            Context.SaveChanges();
            return Ok(objtb);
        }
    }
}
