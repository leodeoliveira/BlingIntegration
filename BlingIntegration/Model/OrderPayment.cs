using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using BlingIntegration.Model;

namespace BlingIntegration
{
    public class OrderPayment
    {
        [JsonProperty("parcela")]
        public OrderPaymentJSON Payment { get; set; }
    }

    public class OrderPaymentJSON
    {
        [JsonProperty("valor")]
        public decimal totalPayment { get; set; }

        [JsonProperty("dataVencimento")]
        public DateTime dueDate { get; set; }

        [JsonProperty("forma_pagamento")]
        public PaymentType PaymentType { get; set; }

    }

    public class PaymentType
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("descricao")]
        public string description { get; set; }

        [JsonProperty("codigoFiscal")]
        public int code { get; set; }
    }
}
