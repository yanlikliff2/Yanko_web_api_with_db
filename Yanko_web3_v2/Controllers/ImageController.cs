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
    public class ImageController : ControllerBase
    {
        public PractDbContext Context { get; }

        public ImageController(PractDbContext context)
        {
            Context = context;
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ImageTable> users = Context.ImageTables.ToList();
            return Ok(users);
        }
        [Authorize(Role.Admin)]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add(byte[] newImage, int objectId)
        {
            ImageTable image = new ImageTable() { Image = newImage, ObjectId = objectId };
            Context.ImageTables.Add(image);
            Context.SaveChanges();
            return Ok(image);
        }
        [Authorize(Role.Admin)]
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
