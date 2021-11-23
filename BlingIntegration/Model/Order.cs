using BlingIntegration.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BlingIntegration
{
    public class Order
    {
        [JsonProperty("numero")]
        public string order_id { get; set; }

        [JsonProperty("data")]
        public DateTime order_date { get; set; }

        [JsonProperty("totalvenda")]
        public decimal total_amount { get; set; }
        public decimal product_return { get; set; }
        [JsonProperty("vendedor")]
        public string employee_id { get; set; }
        public string payment_type { get; set; }
        [JsonProperty("valorfrete")]
        public decimal total_exclusion { get; set; }
        public decimal total_debit { get; set; }

        [JsonProperty("nota")]
        public NF NF { get; set; }

        [JsonProperty("itens")]
        public List<OrderItem> OrderItems { get; set; }

        [JsonProperty("parcelas")]
        public List<OrderPayment> OrderPayments { get; set; }

        [JsonProperty("cliente")]
        public Contact Customer { get; set; }
    }
}
