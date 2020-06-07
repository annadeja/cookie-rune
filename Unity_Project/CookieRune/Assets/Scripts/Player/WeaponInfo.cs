using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryInfo
{
    public class WeaponInfo : ItemInfo
    {
        private int physicalDamage;
        public int PhysicalDamage
        { get; set; }
        private int magicalDamage;
        public int MagicalDamage
        { get; set; }

        public WeaponInfo() : base()
        { }

        public WeaponInfo(string nam, int physicalDamage, int magicalDamage, string type, string description, string icon, int value) : base(nam, type, description, icon, value)
        {
            this.physicalDamage = physicalDamage;
            this.magicalDamage = magicalDamage;
        }

        public WeaponInfo(WeaponInfo weaponInfo) : base(weaponInfo)
        {
            this.physicalDamage = weaponInfo.PhysicalDamage;
            this.magicalDamage = weaponInfo.MagicalDamage;
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

        public WeaponInfo copy()
        {
            return new WeaponInfo(this);
        }
    }
}
