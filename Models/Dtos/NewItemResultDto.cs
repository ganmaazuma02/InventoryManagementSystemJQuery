using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystemJQuery.Models.Dtos
{
    public class NewItemResultDto
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemDesc { get; set; }

        public int Quantity { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }
}
