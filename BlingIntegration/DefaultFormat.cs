using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlingIntegration
{
    public static class DefaultFormat
    {
        public static string Number(decimal value)
        {
            string decimalFormatted = value.ToString("F");
            return decimalFormatted.Replace(",", "");
        }

        public static string Date(DateTime date)
        {
            if (date == new DateTime())
                return string.Empty;
            return date.ToString("yyyy-MM-dd");
        }

        public static string CPF(string cpf)
        {
            return string.IsNullOrEmpty(cpf) ? string.Empty : Regex.Replace(cpf, @"(\s+|-|\.)", "");
        }

        public static string Phone(string phone)
        {
            return string.IsNullOrEmpty(phone) ? string.Empty : Regex.Replace(phone, @"(\s+|-|\(|\))", "");
        }

        public static string GetFileName(string type)
        {
            return string.Format("{0}-{1}-{2}-{3}.csv", ConfigurationManager.AppSettings["ClientID"], ConfigurationManager.AppSettings["CNPJ"], type, DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}
