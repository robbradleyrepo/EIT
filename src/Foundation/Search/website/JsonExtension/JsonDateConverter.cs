namespace LionTrust.Foundation.Search.JsonExtension
{
    using Newtonsoft.Json.Converters;

    public class JsonDateConverter : IsoDateTimeConverter
    {
        public JsonDateConverter()
        {
            DateTimeFormat = "dd.MM.yyyy";
        }
    }
}