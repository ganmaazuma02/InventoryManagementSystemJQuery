using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementSystemJQuery.Models;
using InventoryManagementSystemJQuery.Models.ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystemJQuery.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private InventoryDbContext _context;

        public ItemsController(InventoryDbContext context)
        {
            _context = context;
        }

        [Route("getall")]
        public List<Item> GetAllItems()
        {
            List<Item> Items = _context.Items.ToList();
            return Items;
        }

        [HttpPost("add")]
        public IActionResult AddItem([FromBody]ItemApiModel item)
        {
            if(item != null)
            {
                Item newItem = new Item
                {
                    ItemName = item.ItemName,
                    ItemDesc = item.ItemDesc,
                    Quantity = item.Quantity,
                    DateTimeAdded = DateTime.Now
                };
                _context.Items.Add(newItem);
                _context.SaveChanges();

                return Ok(newItem);
            }

            return BadRequest("No item data was sent");
        }

        [HttpPut("edit")]
        public IActionResult EditItem([FromBody]ItemApiModel item, [FromQuery] int itemId)
        {
            if (item != null && itemId != 0)
            {
                Item itemInDb = _context.Items.Single(i => i.ItemId == itemId);
                if(itemInDb == null)
                {
                    return NotFound();
                }
                itemInDb.ItemName = item.ItemName;
                itemInDb.ItemDesc = item.ItemDesc;
                itemInDb.Quantity = item.Quantity;
  
                _context.SaveChanges();

                return Ok(itemInDb);
            }

            return NotFound();
        }

        [HttpDelete("delete")]
        public IActionResult DeleteItem([FromQuery]int itemId)
        {
            if(itemId != 0)
            {
                Item itemInDb = _context.Items.Single(i => i.ItemId == itemId);
                if (itemInDb == null)
                {
                    return NotFound();
                }
                _context.Items.Remove(itemInDb);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
