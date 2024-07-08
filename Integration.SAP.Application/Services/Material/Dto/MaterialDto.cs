using Newtonsoft.Json;

namespace Integration.SAP.Application.Services.Material.Dto
{
    public class MaterialDto
    {
        [JsonProperty("MANDT")]
        public string Material { get; set; }

        [JsonProperty("MATNR")]
        public string NumeroMaterial { get; set; }

        //[JsonPropertyName("ERSDA")]
        //public string Data{ get; set; }

        //[JsonPropertyName("CREATED_AT_TIME")]
        //public string Criado { get; set; }

    }
}
