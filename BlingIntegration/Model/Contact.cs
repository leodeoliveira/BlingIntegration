using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlingIntegration
{
    public class Contact
    {
        [JsonProperty("nome")]
        public string name { get; set; }

        [JsonProperty("fantasia")]
        public string nickname { get; set; }

        [JsonProperty("cnpj")]
        public string cpf { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("fone")]
        public string phone { get; set; }

        [JsonProperty("celular")]
        public string mobile { get; set; }

        [JsonProperty("sexo")]
        public string gender { get; set; }

        [JsonProperty("dataNascimento")]
        public string birthday { get; set; }

        [JsonProperty("endereco")]
        public string street { get; set; }

        [JsonProperty("numero")]
        public string number { get; set; }

        [JsonProperty("complemento")]
        public string complement { get; set; }

        [JsonProperty("bairro")]
        public string neighborhood { get; set; }

        [JsonProperty("cidade")]
        public string city { get; set; }

        [JsonProperty("uf")]
        public string uf { get; set; }

        public string country  => "Brasil";

        [JsonProperty("cep")]
        public string zipcode { get; set; }

        public string employee_id { get; set; }

        public int is_active => 1;

        [JsonProperty("dataInclusao")]
        public DateTime registered_at { get; set; }

        [JsonProperty("id")]
        public string original_id { get; set; }

        [JsonProperty("tiposContato")]
        public List<ContactTypes> ContactTypes { get; set; }
    }

    public class ContactTypes
    {
        [JsonProperty("tipoContato")]
        public ContactType ContactType { get; set; }
    }

    public class ContactType
    {
        [JsonProperty("descricao")]
        public string description { get; set; }
    }
}
