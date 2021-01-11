using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class InventoryListViewModel
    {
        public List<InventoryInfoViewModel> Inventories { get; set; }
        public StoreInfoViewModel StoreLocation { get; set; }
    }
}
