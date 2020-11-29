using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementSystemJQuery.Models.ApiBodyModels;
using InventoryManagementSystemJQuery.Models.ApiQueryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystemJQuery.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private InventoryDbContext _context;

        public WarehouseController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public string GetWarehouse([FromQuery] WarehouseQuery query)
        {
            return query.Name + " " + query.Description;
        }

        [HttpPost]
        public string GetWarehouse([FromBody] WarehouseBody body, [FromQuery] WarehouseQuery query)
        {
            return query.Name + " " + query.Description + ", body " + body.Name + " " + body.Description;
        }
    }
}
