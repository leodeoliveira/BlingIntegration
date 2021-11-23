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
    public class ContactService
    {
        private static List<string> ExecuteGetContacts(string apiKey, bool lastSevenDays)
        {
            int counter = 1;
            bool hasData = true;
            List<string> jsonStringList = new List<string>();

            while (hasData)
            {
                string url = string.Format("https://bling.com.br/Api/v2/contatos/page={0}/json&apikey={1}&filters=tipoPessoa[F]", counter, apiKey);
                if (lastSevenDays)
                {
                    url += string.Format(";dataAlteracao[{0} TO {1}]", DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy"),
                        DateTime.Now.ToString("dd/MM/yyyy"));
                    url = url.Replace("-", "/");
                }

                var request = HttpWebRequest.Create(url);
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

        public static void PrepareCustomerFile(string apiKey)
        {
            List<CSVCustomer> customerList = new List<CSVCustomer>();
            bool lastSevenDays = bool.Parse(ConfigurationManager.AppSettings["LastSevenDays"]);
            List<string> jsonStringList = ExecuteGetContacts(apiKey, lastSevenDays);
            foreach (string jsonString in jsonStringList)
            {
                var parsedObject = JObject.Parse(jsonString);
                foreach (var obj in parsedObject["retorno"]["contatos"])
                {
                    Contact customer = JsonConvert.DeserializeObject<Contact>(obj["contato"].ToString());
                    customerList.Add(new CSVCustomer(customer));
                }
            }

            string fileName = DefaultFormat.GetFileName("customer");
            ExportData.ExportCsv(customerList, fileName);
        }

        public static void PrepareEmployeeFile(string apiKey)
        {
            bool lastSevenDays = bool.Parse(ConfigurationManager.AppSettings["LastSevenDays"]);
            List<string> jsonStringList = ExecuteGetContacts(apiKey, lastSevenDays);
            List<CSVEmployee> employeeList = new List<CSVEmployee>();
            foreach (string jsonString in jsonStringList)
            {
                var parsedObject = JObject.Parse(jsonString);
                foreach (var obj in parsedObject["retorno"]["contatos"])
                {
                    Contact employee = JsonConvert.DeserializeObject<Contact>(obj["contato"].ToString());
                    if (employee.ContactTypes != null && employee.ContactTypes[0].ContactType.description == "Vendedor")
                        employeeList.Add(new CSVEmployee(employee));
                }
            }

            string fileName = DefaultFormat.GetFileName("employee");
            ExportData.ExportCsv(employeeList, fileName);
        }
    }
}
