using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryInfo
{
    public class ArmorInfo : ItemInfo
    {
        private int physicalDefense;
        public int PhysicalDefense
        { get; set; }
        private int magicalDefense;
        public int MagicalDefense
        { get; set; }

        public ArmorInfo() : base()
        { }

        public ArmorInfo(string nam, int physicalDefense, int magicalDefense, string type, string description, string icon, int value) : base(nam, type, description, icon, value)
        {
            this.physicalDefense = physicalDefense;
            this.magicalDefense = magicalDefense;
        }

        public ArmorInfo(ArmorInfo armorInfo) : base(armorInfo)
        {
            this.physicalDefense = armorInfo.PhysicalDefense;
            this.magicalDefense = armorInfo.MagicalDefense;
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

        public ArmorInfo copy()
        {
            return new ArmorInfo(this);
        }
    }
}
