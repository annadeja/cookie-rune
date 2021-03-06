﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryInfo
{
    public class ArmorInfo : ItemInfo
    {
        private int physicalDefense;
        public int PhysicalDefense
        { get => physicalDefense; set => physicalDefense = value; }
        private int magicalDefense;
        public int MagicalDefense
        { get => magicalDefense; set => magicalDefense = value; }

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

        public override void takeEffect(Character target)
        {
            target.curDef = target.def + physicalDefense;
            target.curMdef = target.mdef + magicalDefense;
            target.armor = this;
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

        public override ItemInfo copy()
        {
            return new ArmorInfo(this);
        }

        public override string getDescription()
        {
            return description + " Grants " + physicalDefense + " def and " + magicalDefense + "mdef.";
        }

        public override string getShortDesc()
        {
            return "Grants " + physicalDefense + " def and " + magicalDefense + "mdef.";
        }
    }
}
