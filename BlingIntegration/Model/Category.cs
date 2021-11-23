using Newtonsoft.Json;

namespace BlingIntegration
{
    public class Category
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("descricao")]
        public string Name { get; set; }
    }
}