using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAdventure
{
    internal class Item
    {
        public string itemName { get; set; }
        public Item(string itemName)
        {
            this.itemName = itemName;
        }

    }
}