using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlingIntegration.Model
{
    public class CSVEmployee
    {
        public CSVEmployee(Contact employee)
        {
            this.name = employee.name;
            this.store_id = ConfigurationManager.AppSettings["CNPJ"];
            this.original_id = employee.name;
            this.street = employee.street;
            this.complement = employee.complement;
            this.city = employee.city;
            this.uf = employee.uf;
            this.zipcode = Regex.Replace(employee.zipcode, @"(\s+|-|\.)", "");
            this.is_active = employee.is_active;
        }

        public string name { get; set; }
        public string street { get; set; }
        public string complement { get; set; }
        public string city { get; set; }
        public string uf { get; set; }
        public string zipcode { get; set; }
        public string store_id { get; set; }
        public string original_id { get; set; }
        public int is_active { get; set; }
    }
}
