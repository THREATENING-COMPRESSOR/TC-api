using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TC.Domain.Catalog;
using TC.Data;
using TC.Api.Security;

namespace TC.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly StoreContext _db;

        public CatalogController(StoreContext db){
            _db = db;
        }

        // returns list items "all items" from db
        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok(_db.Items);
        }

        // returns a singular item based on ID from db
        [HttpGet("{id:int}")]
        public IActionResult GetItem(int id)
        {
            var item = _db.Items.Find(id);
            if (item == null){
                // returns error 404
                return NotFound();
            }
            return Ok();
        }
        
        // creates new resource
        [HttpPost]
        public IActionResult Post(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
            return Created($"/catalog/{item.Id}", item);
        }
        
        // post review by item id
        [HttpPost("{id:int}/ratings")]
        public IActionResult PostRating(int id, [FromBody] Rating rating)
        {
            var item = _db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            
            item.AddRating(rating);
            _db.SaveChanges();

            return Ok(item);
        }

        // Put an update to an Item based on its ID
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Item item)
        {
            if (id != item.Id){
                return BadRequest();
            }

            if (_db.Items.Find(id) == null){
                return NotFound();
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return NoContent();
        }


        // delete an item from collection
        [HttpDelete("{id:int}")]
        [Authorize("delete:catalog")]
        public IActionResult DeleteItem(int id)
        {
            var item = _db.Items.Find(id);

            if (item == null){
                return NotFound();
            }

            _db.Items.Remove(item);
            _db.SaveChanges();

            return Ok();
        }
    }
}