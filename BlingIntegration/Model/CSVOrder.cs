using BlingIntegration.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace BlingIntegration
{
    public class CSVOrder
    {
        private readonly Order _order;

        public CSVOrder(Order order, OrderItem orderItem)
        {
            this.order_id = order.order_id;
            this.identifier = string.Concat(order.order_id, "-1-", order.order_date.ToString("yyyyMMdd"));
            this.store_id = ConfigurationManager.AppSettings["CNPJ"];
            this.customer_id = DefaultFormat.CPF(order.Customer?.cpf);
            this.order_date = order.order_date.ToString("yyyy-MM-dd hh:mm:ss");
            this.total_amount = DefaultFormat.Number(order.total_amount);
            this.product_return = DefaultFormat.Number(order.product_return);
            this.sku = orderItem.Item.sku;
            this.amount = DefaultFormat.Number(orderItem.Item.price * orderItem.Item.quantity);
            this.quantity = orderItem.Item.quantity.ToString("F");
            this.employee_id = order.employee_id;
            this.payment_type = order.payment_type;
            this.total_exclusion = DefaultFormat.Number(order.total_exclusion);
            this.total_debit = DefaultFormat.Number(order.total_debit);
        }

        public string order_id { get; set; }
        public string identifier { get; set; }
        public string store_id { get; set; }
        public string customer_id { get; set; }
        public string order_date { get; set; }
        public string total_amount { get; set; }
        public string product_return { get; set; }
        public string sku { get; set; }
        public string amount{ get; set; }
        public string quantity { get; set; }
        public string employee_id { get; set; }
        public string payment_type { get; set; }
        public string total_exclusion { get; set; }
        public string total_debit { get; set; }
    }
}
