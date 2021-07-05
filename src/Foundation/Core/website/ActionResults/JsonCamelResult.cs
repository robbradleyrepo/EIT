namespace LionTrust.Foundation.Core.ActionResults
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Web;
    using System.Web.Mvc;

    public class JsonCamelCaseResult : JsonResult
    {
        private const string HttpMethod = "GET";
        private const string DefaultContentType = "application/json";

        public JsonCamelCaseResult(object data, JsonRequestBehavior jsonRequestBehavior) : base()
        {
            Data = data;
            JsonRequestBehavior = jsonRequestBehavior;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                 String.Equals(context.HttpContext.Request.HttpMethod,
                               HttpMethod, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("HttpMethod 'GET' is not allowed");
            }

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? DefaultContentType : ContentType;
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None
            };

            response.Write(JsonConvert.SerializeObject(Data, jsonSerializerSettings));
        }
    }
}