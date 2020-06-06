using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryInfo
{
    public class ConsumableInfo : ItemInfo
    {
        public ConsumableInfo() : base()
        { }

        public ConsumableInfo(string nam, string type, string description, string icon) : base(nam, type, description, icon)
        { }

        public virtual void takeEffect(Character target) //Tu będzie przeciążana funkcja efektu przedmiotu.
        { }
    }
}
