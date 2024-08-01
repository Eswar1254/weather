using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace weather
{
    public class WeatherData
    {
        [JsonPropertyName("coord")]
        public Coordinates coord { get; set; }

        [JsonPropertyName("weather")]
        public List<WeatherDescription> weather { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("main")]
        public MainInfo main { get; set; }

        [JsonPropertyName("visibility")]
        public int visibility { get; set; }

        [JsonPropertyName("wind")]
        public WindInfo wind { get; set; }

        [JsonPropertyName("clouds")]
        public CloudsInfo clouds { get; set; }

        [JsonPropertyName("dt")]
        public long dt { get; set; }

        [JsonPropertyName("sys")]
        public SystemInfo sys { get; set; }

        [JsonPropertyName("timezone")]
        public int timezone { get; set; }

        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("cod")]
        public int cod { get; set; }
    }

    public class Coordinates
    {
        [JsonPropertyName("lon")]
        public float lon { get; set; }

        [JsonPropertyName("lat")]
        public float lat { get; set; }
    }

    public class WeatherDescription
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("main")]
        public string main { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("icon")]
        public string icon { get; set; }
    }

    public class MainInfo
    {
        [JsonPropertyName("temp")]
        public float temp { get; set; }

        [JsonPropertyName("feels_like")]
        public float feels_like { get; set; }

        [JsonPropertyName("temp_min")]
        public float temp_min { get; set; }

        [JsonPropertyName("temp_max")]
        public float temp_max { get; set; }

        [JsonPropertyName("pressure")]
        public int pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int humidity { get; set; }

        [JsonPropertyName("sea_level")]
        public int sea_level { get; set; }

        [JsonPropertyName("grnd_level")]
        public int grnd_level { get; set; }
    }

    public class WindInfo
    {
        [JsonPropertyName("speed")]
        public float speed { get; set; }

        [JsonPropertyName("deg")]
        public int deg { get; set; }
    }

    public class CloudsInfo
    {
        [JsonPropertyName("all")]
        public int all { get; set; }
    }

    public class SystemInfo
    {
        [JsonPropertyName("type")]
        public int type { get; set; }

        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("country")]
        public string country { get; set; }

        [JsonPropertyName("sunrise")]
        public long sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public long sunset { get; set; }
    }
}
