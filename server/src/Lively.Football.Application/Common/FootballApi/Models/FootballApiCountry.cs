using Newtonsoft.Json;

namespace Lively.Football.Application.Common.FootballApi.Models
{
    internal class FootballApiCountry
    {
        [JsonProperty("country_id")]
        public string Id { get; set; }

        [JsonProperty("country_name")]
        public string Name { get; set; }
    }
}
