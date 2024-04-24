// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

using System.Text.Json.Serialization;

    public class Image
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("thumbnail_medium")]
        public string ThumbnailMedium { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("internal_4rent_id")]
        public string Internal4rentId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

      [JsonPropertyName("min_price")]
        public string MinPrice { get; set; }

        [JsonPropertyName("max_price")]
        public string MaxPrice { get; set; }

        [JsonPropertyName("image")]
        public Image Image { get; set; }

        [JsonPropertyName("property_type")]
        public string PropertyType { get; set; }

    }

    public class Root
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }
    }

