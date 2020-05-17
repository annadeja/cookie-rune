using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryInfo
{
    public class ConsumableInfo : ItemInfo
    {
        public ConsumableInfo() : base()
        { }

        public ConsumableInfo(string type, string description, string icon) : base(type, description, icon)
        { }

        public virtual void takeEffect() //Tu będzie przeciążana funkcja efektu przedmiotu.
        { }
    }
}
