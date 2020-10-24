using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystemJQuery.Models.ApiModels
{
    public class ItemApiModel
    {
        [Required(ErrorMessage = "This Field is required")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public string ItemDesc { get; set; }

        [Required(ErrorMessage = "This Field is required")]
        public int Quantity { get; set; }
    }
}
