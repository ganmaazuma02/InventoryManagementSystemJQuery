using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystemJQuery.Models.Dtos
{
    public class GetItemDto
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; }
        public string ItemDesc { get; set; }

        public int Quantity { get; set; }

        public DateTime DateTimeAdded { get; set; }
    }
}
