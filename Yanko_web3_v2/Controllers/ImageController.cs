using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yanko_web3_v2.Models;

namespace Yanko_web3_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public PractDbContext Context { get; }

        public ImageController(PractDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ImageTable> users = Context.ImageTables.ToList();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ImageTable? image = Context.ImageTables.Where(x => x.ImageId == id).FirstOrDefault();
            if (image == null)
            {
                return BadRequest("Not found");
            }
            return Ok(image);
        }
        [HttpPut]
        public IActionResult Update(int id, byte[] newImage) // Не вижу смысла менять ObjectId
        {
            ImageTable? image = Context.ImageTables.Where(x => x.ImageId == id).FirstOrDefault();
            if (image == null)
            {
                return BadRequest("Not found");
            }
            image.Image = newImage;
            Context.SaveChanges();
            return Ok(image);
        }
        [HttpPost]
        public IActionResult Add(byte[] newImage, int objectId)
        {
            ImageTable image = new ImageTable() { Image = newImage, ObjectId = objectId };
            Context.ImageTables.Add(image);
            Context.SaveChanges();
            return Ok(image);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        { 
            ImageTable? image = Context.ImageTables.Where(x => x.ImageId == id).FirstOrDefault();
            if (image == null) return BadRequest("Not found");
            Context.ImageTables.Remove(image);
            Context.SaveChanges();
            return Ok(image);
        }
    }
}
