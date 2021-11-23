using Newtonsoft.Json;

namespace BlingIntegration
{
    public class ItemVariation
    {
        [JsonProperty("variacao")]
        public Variation Variaton { get; set; }
    }

    public class Variation
    {
        [JsonProperty("codigo")]
        public string sku { get; set; }
    }
}