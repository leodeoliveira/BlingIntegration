using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlingIntegration
{
    public class Employee
    {
        [JsonProperty("nome")]
        public string name { get; set; }
        public string store_id { get; set; }

        [JsonProperty("id")]
        public string original_id { get; set; }

        [JsonProperty("endereco")]
        public string street { get; set; }

        [JsonProperty("complemento")]
        public string complement { get; set; }

        [JsonProperty("cidade")]
        public string city { get; set; }

        [JsonProperty("uf")]
        public string uf { get; set; }

        [JsonProperty("cep")]
        public string zipcode { get; set; }
        public int is_active => 1;

    }
}
