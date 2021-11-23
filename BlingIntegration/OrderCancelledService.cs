using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;

namespace BlingIntegration
{
    public class OrderCancelledService
    {
        private const int RETURN_CODE = 5;

        public static void PrepareOrderFile(string apiKey)
        {
            bool lastSevenDays = bool.Parse(ConfigurationManager.AppSettings["LastSevenDays"]);
            List<string> jsonStringList = ExecuteGetOrder(apiKey, lastSevenDays);
            List<CSVOrder> orderList = new List<CSVOrder>();
            foreach (string jsonString in jsonStringList)
            {
                var parsedObject = JObject.Parse(jsonString);
                foreach (var obj in parsedObject["retorno"]["pedidos"])
                {
                    Order order = JsonConvert.DeserializeObject<Order>(obj["pedido"].ToString());
                    order.product_return = GetPaymentReturn(order.OrderPayments);
                    order.payment_type = GetPaymentType(order.OrderPayments);

                    if (order.OrderItems != null)
                        foreach (var orderItem in order.OrderItems)
                            orderList.Add(new CSVOrder(order, orderItem));
                }
            }

            string fileName = DefaultFormat.GetFileName("ordercanceled");
            ExportData.ExportCsv(orderList, fileName);
        }

        private static string GetPaymentType(List<OrderPayment> orderPayments)
        {
            return orderPayments != null ? orderPayments[0].Payment.PaymentType.description : string.Empty;
        }

        private static decimal GetPaymentReturn(List<OrderPayment> orderPayments)
        {
            OrderPayment paymentReturn =  orderPayments?.FirstOrDefault(x => x.Payment.PaymentType.code == RETURN_CODE);
            return paymentReturn?.Payment.totalPayment ?? 0;

        }

        public static List<string> ExecuteGetOrder(string apiKey, bool lastSevenDays)
        {
            int counter = 1;
            bool hasData = true;
            List<string> jsonStringList = new List<string>();

            while (hasData)
            {
                string url = string.Format("https://bling.com.br/Api/v2/pedidos/page={0}/json&apikey={1}&filters=idSituacao[12]", counter,
                    apiKey);

                if (lastSevenDays)
                {
                    url += string.Format("; dataEmissao[{0} TO {1}]",
                        DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                    url = url.Replace("-", "/");
                }

                var request = WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Method = "GET";
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        Console.Out.WriteLine("Error. Server returned status code: {0}", response.StatusCode);
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(content) || JObject.Parse(content)["retorno"]["erros"] != null)
                            hasData = false;
                        else
                            jsonStringList.Add(content);
                    }
                }
                counter++;
            }

            return jsonStringList;
        }
    }
}
