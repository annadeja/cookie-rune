using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryInfo
{
    public class ConsumableInfo : ItemInfo
    { 
        int statChange;

        public ConsumableInfo() : base()
        { }

        public ConsumableInfo(string nam, string type, string description, string icon, int value, int statChange) : base(nam, type, description, icon, value)
        {
            this.statChange = statChange;
        }

        public ConsumableInfo(ConsumableInfo consumableInfo) : base(consumableInfo)
        { }

        public override void takeEffect(Character target)
        {
            switch(type)
            {
                case "Healing":
                    target.heal(statChange);
                    break;
                case "Restoring":
                    target.restoreMP(statChange);
                    break;
                case "Offensive":
                    target.takeDmg(statChange);
                    break;
            }
        }

        public override ItemInfo copy()
        {
            return new ConsumableInfo(this);
        }
    }
}
