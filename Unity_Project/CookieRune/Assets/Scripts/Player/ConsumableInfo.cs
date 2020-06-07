using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryInfo
{
    public class ConsumableInfo : ItemInfo
    {
        public ConsumableInfo() : base()
        { }

        public ConsumableInfo(string nam, string type, string description, string icon, int value) : base(nam, type, description, icon, value)
        { }

        public ConsumableInfo(ConsumableInfo consumableInfo) : base(consumableInfo)
        { }

        public virtual void takeEffect(Character target) //Tu będzie przeciążana funkcja efektu przedmiotu.
        { }

        public ConsumableInfo copy()
        {
            return new ConsumableInfo(this);
        }
    }
}
