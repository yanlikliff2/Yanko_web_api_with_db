﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yanko_web3_v2.Models;

namespace Yanko_web3_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        public PractDbContext Context { get; }

        public CollectionController(PractDbContext context)
        {
            Context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<CollectionTable> collections = Context.CollectionTables.ToList();
            return Ok(collections);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            CollectionTable? collection = Context.CollectionTables.Where(x => x.CollectionId == id).FirstOrDefault();
            if (collection == null)
            {
                return BadRequest("Not found");
            }
            return Ok(collection);
        }
        [HttpPut]
        public IActionResult Update(int id, string collectionName) // Не вижу смысла менять UserId
        {
            CollectionTable? collection = Context.CollectionTables.Where(x => x.CollectionId == id).FirstOrDefault();
            if (collection == null)
            {
                return BadRequest("Not found");
            }
            collection.CollectionName = collectionName;
            Context.SaveChanges();
            return Ok(collection);
        }
        [HttpPost]
        public IActionResult Add(int userId, string collectionName)
        {
            CollectionTable collection = new CollectionTable() { UserId = userId, CollectionName = collectionName };
            Context.CollectionTables.Add(collection);
            Context.SaveChanges();
            return Ok(collection);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            CollectionTable? collection = Context.CollectionTables.Where(x => x.CollectionId == id).FirstOrDefault();
            if (collection == null) return BadRequest("Not found");
            Context.CollectionTables.Remove(collection);
            Context.SaveChanges();
            return Ok(collection);
        }
    }
}
