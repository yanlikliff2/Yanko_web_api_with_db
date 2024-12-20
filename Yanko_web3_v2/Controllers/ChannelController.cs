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
    public class ChannelController : ControllerBase
    {
        public PractDbContext Context { get; }

        public ChannelController(PractDbContext context)
        {
            Context = context;
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ChannelTable> users = Context.ChannelTables.ToList();
            return Ok(users);
        }
        [Authorize(Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ChannelTable? objectTable = Context.ChannelTables.Where(x => x.ChannelId == id).FirstOrDefault();
            if (objectTable == null)
            {
                return BadRequest("Not found");
            }
            return Ok(objectTable);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Update(int id, string channelName) // Не вижу смысла менять AutorId
        {
            ChannelTable? channelTable = Context.ChannelTables.Where(x => x.ChannelId == id).FirstOrDefault();
            if (channelTable == null)
            {
                return BadRequest("Not found");
            }
            channelTable.ChannelName = channelName;
            Context.SaveChanges();
            return Ok(channelTable);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add(string channelName, int autorId)
        {
            ChannelTable channelTable = new ChannelTable()
            {
                ChannelName = channelName,
                AutorId = autorId
            };
            Context.ChannelTables.Add(channelTable);
            Context.SaveChanges();
            return Ok(channelTable);
        }
        [Authorize(Role.Admin)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            ChannelTable? channelTable = Context.ChannelTables.Where(x => x.ChannelId == id).FirstOrDefault();
            if (channelTable == null) return BadRequest("Not found");
            Context.ChannelTables.Remove(channelTable);
            Context.SaveChanges();
            return Ok(channelTable);
        }
    }
}
