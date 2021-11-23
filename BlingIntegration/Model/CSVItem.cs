using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlingIntegration.Model
{
    public class CSVItem
    {
        public CSVItem(Item item)
        {
            this.sku = item.sku;
            this.name = item.name;
            this.description = item.name;
            this.supplierreference = item.supplierreference;
            this.brandname = item.brandname;
            this.seasonname = item.seasonname;
            this.category1 = item.Category?.Name;
            this.category2 = item.category2;
            this.category3 = item.category3;
            this.size = item.size;
            this.color = item.color;
            this.identifier = item.identifier;
            this.unit = item.unit;
            this.price = DefaultFormat.Number(item.price);
            this.created_at = DefaultFormat.Date(item.created_at.Value);
            this.updated_at = DefaultFormat.Date(item.updated_at.Value);
        }

        public string sku { get; set; }
        public string name { get; set; }
        public string description { get; set; } 
        public string supplierreference { get; set; }
        public string brandname { get; set; }
        public string seasonname { get; set; }
        public string category1 { get; set; }
        public string category2 { get; set; }
        public string category3 { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public string identifier { get; set; }
        public string unit { get; set; }
        public string price { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
