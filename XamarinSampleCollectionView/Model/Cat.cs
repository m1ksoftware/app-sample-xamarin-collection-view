using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xamarin.Forms;

namespace XamarinSampleCollectionView.Model
{
	public partial class Cat
	{
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        public ImageSource ImageSource { get; set; } = null;
    }

    public partial class Cat
    {
        public static Cat[] FromJson(string json) => JsonConvert.DeserializeObject<Cat[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Cat[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

