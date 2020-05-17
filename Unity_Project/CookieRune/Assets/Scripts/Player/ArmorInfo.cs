using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryInfo
{
    public class ArmorInfo : ItemInfo
    {
        private int physicalDefense
        { get; set; }
        private int magicalDefense
        { get; set; }

        public ArmorInfo() : base()
        { }

        public ArmorInfo(int physicalDefense, int magicalDefense, string type, string description, string icon) : base(type, description, icon)
        {
            this.physicalDefense = physicalDefense;
            this.magicalDefense = magicalDefense;
        }

        public override ArrayList getStats()
        {
            ArrayList stats = new ArrayList();
            stats.Add(physicalDefense);
            stats.Add(magicalDefense);
            stats.Add(type);
            stats.Add(description);

            return stats;
        }
    }
}
