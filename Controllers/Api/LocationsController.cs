using InventoryManagementSystemJQuery.Models;
using InventoryManagementSystemJQuery.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystemJQuery.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private InventoryDbContext _context;

        public LocationsController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetLocations()
        {
            List<GetLocationDto> locationsDto = new List<GetLocationDto>();
            var locationsInDb = _context.Locations.Include(l => l.Items).ToList();
            
            foreach(Location locationInDb in locationsInDb)
            {
                locationsDto.Add(new GetLocationDto
                {
                    LocationId = locationInDb.LocationId,
                    LocationDesc = locationInDb.LocationDesc,
                    LocationName = locationInDb.LocationName,
                    Items = locationInDb.Items.Select(i =>
                        new GetItemDto
                        {
                            ItemId = i.ItemId,
                            ItemDesc = i.ItemDesc,
                            DateTimeAdded = i.DateTimeAdded,
                            ItemName = i.ItemName,
                            Quantity = i.Quantity
                        }).ToList()
                });
            }

            return Ok(locationsDto);
        }

        [HttpGet("assignitem")]
        public IActionResult AssignItemToLocation([FromQuery] int itemId, [FromQuery] int locationId)
        {
            if(itemId != 0 || locationId != 0)
            {
                var item = _context.Items.SingleOrDefault(i => i.ItemId == itemId);
                var location = _context.Locations.Include(l => l.Items).SingleOrDefault(l => l.LocationId == locationId);
                if(item == null || location == null)
                {
                    return NotFound();
                }

                location.Items.Add(item);
            }

            return BadRequest("No item or location id has been sent");
        }
    }
}
