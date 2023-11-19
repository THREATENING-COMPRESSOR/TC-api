using Microsoft.AspNetCore.Mvc;
using TC.Domain.Catalog;
using TC.Data;

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
            return Created("/catalog/42", item);
        }
        
        // post review by item id
        [HttpPost("{id:int}/ratings")]
        public IActionResult PostRating(int id, [FromBody] Rating rating)
        {
            var item = new Item("Shirt", "Ohio State shirt.", "Nike", 29.99m);
            item.Id = id;
            item.AddRating(rating);
            
            return Ok(item);
        }

        // Put an update to an Item based on its ID
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Item item)
        {
            return NoContent();
        }

        // delete an item from collection
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}