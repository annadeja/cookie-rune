using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryInfo{
    public class ItemInfo
    {
        private string name;
        public string Name
        { get => name; }
        protected string type;  //Zamienić na enum?
        public string Type
        { get => type; }
        protected string description;
        public string Description
        { get => description; }
        protected string icon; //Tu będzie grafika, zobaczymy jak to implementować później
        public string Icon
        { get { return icon; } set { this.icon = value; } }
        protected int value;
        public int Value
        { get => value; }

        public ItemInfo()
        { }

        public ItemInfo(string nam, string type, string description, string icon, int value)
        {
            this.name = nam;
            this.type = type;
            this.description = description;
            this.icon = icon;
            this.value = value;
        }

        public ItemInfo(ItemInfo itemInfo)
        {
            this.name = itemInfo.Name;
            this.type = itemInfo.Type;
            this.description = itemInfo.Description;
            this.icon = itemInfo.Icon;
            this.value = itemInfo.Value;
        }

        public virtual void takeEffect(Character target) //Tu będzie przeciążana funkcja efektu przedmiotu.
        { }

        public virtual ArrayList getStats()
        {
            ArrayList stats = new ArrayList();
            stats.Add(type);
            stats.Add(description);

            return stats;
        }

        public virtual ItemInfo copy()
        {
            return new ItemInfo(this);
        }
    }
}
