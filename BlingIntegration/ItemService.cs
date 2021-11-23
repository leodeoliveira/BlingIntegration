using BlingIntegration.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;

namespace BlingIntegration
{
    public class ItemService
    {
        public static void PrepareItemFile(string apiKey)
        {
            List<CSVItem> itemList = new List<CSVItem>();
            bool lastSevenDays = bool.Parse(ConfigurationManager.AppSettings["LastSevenDays"]);
            List<string> jsonStringList = ExecuteGetProducts(apiKey, lastSevenDays);
            foreach (string jsonString in jsonStringList)
            {
                var parsedObject = JObject.Parse(jsonString);
                foreach (var obj in parsedObject["retorno"]["produtos"])
                {
                    Item item = JsonConvert.DeserializeObject<Item>(obj["produto"].ToString());
                    CSVItem csvItem = new CSVItem(item);
                    itemList.Add(csvItem);
                }
            }

            string fileName = DefaultFormat.GetFileName("item");
            ExportData.ExportCsv(itemList, fileName);
        }

        private static List<string> ExecuteGetProducts(string apiKey, bool lastSevenDays)
        {
            int counter = 1;
            bool hasData = true;
            List<string> jsonStringList = new List<string>();

            while (hasData)
            {
                string url = url = string.Format("https://bling.com.br/Api/v2/produtos/page={0}/json&apikey={1}", counter, apiKey);

                if (lastSevenDays)
                {
                    url += string.Format("&filters=dataAlteracao[{0} TO {1}]",
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
