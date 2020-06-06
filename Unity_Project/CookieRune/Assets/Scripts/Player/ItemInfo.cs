using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryInfo{
    public class ItemInfo
    {
        string name;
        public string Name { get => name; }
        protected string type  //Zamienić na enum?
        { get; set; }
        protected string description
        { get; set; }
        protected string icon //Tu będzie grafika, zobaczymy jak to implementować później
        { get; set; }

        public ItemInfo()
        { }

        public ItemInfo(string nam, string type, string description, string icon)
        {
            this.name = nam;
            this.type = type;
            this.description = description;
            this.icon = icon;
        }

        public virtual ArrayList getStats()
        {
            ArrayList stats = new ArrayList();
            stats.Add(type);
            stats.Add(description);

            return stats;
        }
    }
}
