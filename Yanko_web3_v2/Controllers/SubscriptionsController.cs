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
    public class SubscriptionsController : ControllerBase
    {
        public PractDbContext Context { get; }

        public SubscriptionsController(PractDbContext context)
        {
            Context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<SubscriptionsTable> subscribers = Context.SubscriptionsTables.ToList();
            return Ok(subscribers);
        }
        [Authorize(Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            SubscriptionsTable? subscribers = Context.SubscriptionsTables.Where(x => x.SubscriptionsId == id).FirstOrDefault();
            if (subscribers == null)
            {
                return BadRequest("Not found");
            }
            return Ok(subscribers);
        }
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Update(int id, int userId, int channelId, int level)
        {
            SubscriptionsTable? subscribers = Context.SubscriptionsTables.Where(x => x.SubscriptionsId == id).FirstOrDefault();
            if (subscribers == null)
            {
                return BadRequest("Not found");
            }
            subscribers.UserId = userId;
            subscribers.ChannelId = channelId;
            subscribers.SubscriptionsLevel = level;
            Context.SaveChanges();
            return Ok(subscribers);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add(int userId, int channelId, int level)
        {
            SubscriptionsTable subscribers = new SubscriptionsTable() { UserId = userId, ChannelId = channelId, SubscriptionsLevel = level };
            Context.SubscriptionsTables.Add(subscribers);
            Context.SaveChanges();
            return Ok(subscribers);
           
        }
        [Authorize(Role.Admin)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            SubscriptionsTable? subscribers = Context.SubscriptionsTables.Where(x => x.SubscriptionsId == id).FirstOrDefault();
            if (subscribers == null) return BadRequest("Not found");
            Context.SubscriptionsTables.Remove(subscribers);
            Context.SaveChanges();
            return Ok(subscribers);
        }
    }
}
