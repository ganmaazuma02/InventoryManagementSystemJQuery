using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementSystemJQuery.Models;
using InventoryManagementSystemJQuery.Models.ApiBodyModels;
using InventoryManagementSystemJQuery.Models.ApiModels;
using InventoryManagementSystemJQuery.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult AddItem([FromBody] ItemBody body, [FromQuery] int locationId)
        {
            if(body != null)
            {
                var location = _context.Locations.Include(l => l.Items).SingleOrDefault(l => l.LocationId == locationId);

                Item newItem = new Item
                {
                    ItemName = body.ItemName,
                    ItemDesc = body.ItemDesc,
                    Quantity = body.ItemQuantity,
                    DateTimeAdded = DateTime.Now
                };

                location.Items.Add(newItem);
                _context.SaveChanges();

                NewItemResultDto itemDto = new NewItemResultDto
                {
                    ItemId = newItem.ItemId,
                    ItemDesc = newItem.ItemDesc,
                    ItemName = newItem.ItemName,
                    DateTimeAdded = newItem.DateTimeAdded,
                    Quantity = newItem.Quantity,
                    LocationId = location.LocationId,
                    LocationName = location.LocationName
                };

                return Ok(itemDto);
            }

            return BadRequest("No item data was sent");
        }

        [HttpPut("update")]
        public IActionResult UpdateItem([FromBody] ItemBody body, [FromQuery] int itemId)
        {
            if (body != null && itemId != 0)
            {
                Item itemInDb = _context.Items.SingleOrDefault(i => i.ItemId == itemId);
                if(itemInDb == null)
                {
                    return NotFound();
                }
                itemInDb.ItemName = body.ItemName;
                itemInDb.ItemDesc = body.ItemDesc;
                itemInDb.Quantity = body.ItemQuantity;

                _context.SaveChanges();

                return Ok(itemInDb);
            }

            return BadRequest("No item data was sent");
        }

        [HttpDelete("del")]
        public IActionResult DeleteItem([FromQuery] int itemId)
        {
            if(itemId != 0)
            {
                Item itemInDb = _context.Items.SingleOrDefault(i => i.ItemId == itemId);
                if(itemInDb == null)
                {
                    return NotFound();
                }
                _context.Items.Remove(itemInDb);
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest("No item ID was sent");
        }

    }
}
