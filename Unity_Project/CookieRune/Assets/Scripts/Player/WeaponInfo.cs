using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryInfo
{
    public class WeaponInfo : ItemInfo
    {
        private int physicalDamage
        { get; set; }
        private int magicalDamage
        { get; set; }

        public WeaponInfo() : base()
        { }

        public WeaponInfo(int physicalDamage, int magicalDamage, string type, string description, string icon) : base(type, description, icon)
        {
            this.physicalDamage = physicalDamage;
            this.magicalDamage = magicalDamage;
        }

        public override ArrayList getStats()
        {
            ArrayList stats = new ArrayList();
            stats.Add(physicalDamage);
            stats.Add(magicalDamage);
            stats.Add(type);
            stats.Add(description);

            return stats;
        }
    }
}
