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
    public class AdvertisementController : ControllerBase
    {
        public PractDbContext Context { get; }

        public AdvertisementController(PractDbContext context)
        {
            Context = context;
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<AdvertisementTable> advertisements = Context.AdvertisementTables.ToList();
            return Ok(advertisements);
        }
        [Authorize(Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AdvertisementTable? advertisements = Context.AdvertisementTables.Where(x => x.AdvertisementId == id).FirstOrDefault();
            if (advertisements == null)
            {
                return BadRequest("Not found");
            }
            return Ok(advertisements);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Update(int id, int userId, int objectId, double? price, int? sale, int tegId, int ChannelId)
        {
            AdvertisementTable? advertisements = Context.AdvertisementTables.Where(x => x.UserId == id).FirstOrDefault();
            if (advertisements == null)
            {
                return BadRequest("Not found");
            }
            advertisements.UserId = userId;
            advertisements.ObjectId = objectId;
            advertisements.Price = price;
            advertisements.Sale = sale;
            advertisements.TegId = tegId;
            advertisements.ChannelId = ChannelId;
            Context.SaveChanges();
            return Ok(advertisements);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add(int userId, int objectId, double? price, int? sale, int tegId, int ChannelId)
        {
            AdvertisementTable advertisements = new AdvertisementTable() { UserId = userId, ObjectId = objectId,
                Price = price, Sale = sale, TegId = tegId, ChannelId = ChannelId };
            Context.AdvertisementTables.Add(advertisements);
            Context.SaveChanges();
            return Ok(advertisements);
        }
        [Authorize(Role.Admin)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            AdvertisementTable? advertisements = Context.AdvertisementTables.Where(x => x.AdvertisementId == id).FirstOrDefault();
            if (advertisements == null) return BadRequest("Not found");
            Context.AdvertisementTables.Remove(advertisements);
            Context.SaveChanges();
            return Ok(advertisements);
        }
    }
}
