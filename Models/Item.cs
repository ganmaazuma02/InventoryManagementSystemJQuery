using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystemJQuery.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Item Name")]
        [Required(ErrorMessage = "This Field is required")]
        public string ItemName { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [DisplayName("Item Description")]
        [Required(ErrorMessage = "This Field is required")]
        public string ItemDesc { get; set; }

        [DisplayName("Item Quantity")]
        [Required(ErrorMessage = "This Field is required")]
        public int Quantity { get; set; }

        [DisplayName("Date & Time Added")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateTimeAdded { get; set; }
    }
}
