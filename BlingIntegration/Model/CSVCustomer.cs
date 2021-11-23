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
    public class CSVCustomer
    {
        public CSVCustomer(Contact customer)
        {
            this.name = customer.name;
            this.nickname = customer.name.Split(' ')[0];
            this.cpf = DefaultFormat.CPF(customer.cpf);
            this.email = customer.email;
            this.phone = DefaultFormat.Phone(customer.phone);
            this.mobile = DefaultFormat.Phone(customer.mobile);
            this.gender = customer.gender;
            this.birthday = customer.birthday;
            this.street = customer.street;
            this.number = customer.number;
            this.complement = customer.complement;
            this.neighborhood = customer.neighborhood;
            this.city = customer.city;
            this.uf = customer.uf;
            this.country = customer.country;
            this.zipcode = string.IsNullOrEmpty(customer.zipcode) ? string.Empty : Regex.Replace(customer.zipcode, @"(\s+|-|\.)", "");
            this.employee_id = customer.employee_id;
            this.store_id = ConfigurationManager.AppSettings["CNPJ"];
            this.registered_at = DefaultFormat.Date(customer.registered_at);
            this.original_id = customer.original_id;
            this.identifier = DefaultFormat.CPF(customer.cpf);
        }

        public string name { get; set; }
        public string nickname { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string complement { get; set; }
        public string neighborhood { get; set; }
        public string city { get; set; }
        public string uf { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public string employee_id { get; set; }
        public string store_id { get; set; }
        public string registered_at { get; set; }
        public string original_id { get; set; }
        public string identifier { get; set; }
}
}
