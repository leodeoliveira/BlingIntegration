using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using BlingIntegration.Model;

namespace BlingIntegration
{
    public class OrderItem
    {
        [JsonProperty("item")]
        public OrderItemJSON Item { get; set; }
    }

    public class OrderItemJSON
    {
        [JsonProperty("codigo")]
        public string sku { get; set; }

        [JsonProperty("descricao")]
        public string name { get; set; }

        [JsonProperty("quantidade")]
        public decimal quantity { get; set; }

        [JsonProperty("valorUnidade")]
        public decimal price { get; set; }

        [JsonProperty("descontoItem")]
        public decimal discount { get; set; }

        [JsonProperty("unidade")]
        public string unit { get; set; }

        [JsonProperty("pesoBruto")]
        public string weight { get; set; }

        [JsonProperty("largura")]
        public string width { get; set; }

        [JsonProperty("altura")]
        public string height { get; set; }

        [JsonProperty("profundidade")]
        public string deepth { get; set; }

        [JsonProperty("unidadeMedida")]
        public string unitOfMeasurement { get; set; }

        public int ToInt()
        {
            string value = String.Format("{0:C}", this);
            return int.Parse(value.Replace(",", ""));
        }
    }
}
