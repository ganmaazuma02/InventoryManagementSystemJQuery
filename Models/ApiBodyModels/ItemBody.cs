using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystemJQuery.Models.ApiBodyModels
{
    public class ItemBody
    {
        public string ItemName { get; set; }
        public string ItemDesc { get; set; }
        public int ItemQuantity { get; set; }
    }
}
