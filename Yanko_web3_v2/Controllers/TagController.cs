using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yanko_web3_v2.Models;

namespace Yanko_web3_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        public PractDbContext Context { get; }

        public TagController(PractDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<TagTable> tag = Context.TagTables.ToList();
            return Ok(tag);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            TagTable? tag = Context.TagTables.Where(x => x.TegId == id).FirstOrDefault();
            if (tag == null)
            {
                return BadRequest("Not found");
            }
            return Ok(tag);
        }
        [HttpPut]
        public IActionResult Update(int id, string tagName)
        {
            TagTable? tag = Context.TagTables.Where(x => x.TegId == id).FirstOrDefault();
            if (tag == null)
            {
                return BadRequest("Not found");
            }
            tag.TegName = tagName;
            Context.SaveChanges();
            return Ok(tag);
        }
        [HttpPost]
        public IActionResult Add(string tagName)
        {
            TagTable tag = new TagTable() { TegName = tagName };
            Context.TagTables.Add(tag);
            Context.SaveChanges();
            return Ok(tag);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            TagTable? tag = Context.TagTables.Where(x => x.TegId == id).FirstOrDefault();
            if (tag == null) return BadRequest("Not found");
            Context.TagTables.Remove(tag);
            Context.SaveChanges();

            return Ok(tag);
        }
    }
}
