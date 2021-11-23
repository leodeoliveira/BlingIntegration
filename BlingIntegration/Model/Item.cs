using Newtonsoft.Json;
using System;
using BlingIntegration.Model;
using System.Collections.Generic;

namespace BlingIntegration
{
    public class Item
    {
        [JsonProperty("codigo")]
        public string sku { get; set; }

        [JsonProperty("descricao")]
        public string name { get; set; }

        [JsonProperty("nomeFornecedor")]
        public string supplierreference { get; set; }

        [JsonProperty("marca")]
        public string brandname { get; set; }

        public string seasonname { get; set; }

        public string category1 => Category?.Name;

        [JsonProperty("grupoProduto")]
        public string category2 { get; set; }

        [JsonProperty("eighty_min_score")]
        public string category3 { get; set; }

        public string size { get; set; }

        public string color { get; set; }

        public string identifier { get; set; }

        [JsonProperty("unidade")]
        public string unit { get; set; }

        [JsonProperty("preco")]
        public decimal price { get; set; }

        [JsonProperty("dataInclusao")]
        public DateTime created_at { get; set; }

        [JsonProperty("dataAlteracao")]
        public DateTime updated_at { get; set; }

        [JsonProperty("categoria")]
        public Category Category { get; set; }

        [JsonProperty("variacoes")]
        public List<ItemVariation> ItemVariatons { get; set; }

        public int ToInt()
        {
            string value = String.Format("{0:C}", this);
            return int.Parse(value.Replace(",", ""));
        }
    }
}
