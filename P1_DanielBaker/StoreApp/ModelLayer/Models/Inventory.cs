using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Models
{
    public class Inventory
    {
        [Key]
        public Guid InventoryId { get; set; } = Guid.NewGuid();

        [Required]
        public Product Product { get; set; }

        [Required]
        public StoreLocation StoreLocation { get; set; }

        [Required]
        [Range(0, 1000)]
        public int ProductQuantity { get; set; } = 0;
        
        public Inventory(){}

        public Inventory( Product product, StoreLocation store, int quantity )
        {
            this.Product = product;
            this.StoreLocation = store;
            this.ProductQuantity = quantity;
        }
    }
}