using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystemJQuery.Models.Dtos
{
    public class GetLocationDto
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationDesc { get; set; }
        public List<GetItemDto> Items { get; set; }
    }
}
