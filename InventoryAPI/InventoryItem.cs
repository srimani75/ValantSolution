using System;

namespace InventoryAPI
{
    public class InventoryItem
    {
        public string InventoryLabel { get; set; }

        public DateTime ExperiationDate { get; set; }

        public string CustomDetails { get; set; }

        public InventoryType ItemType { get; set; }
    }

}
