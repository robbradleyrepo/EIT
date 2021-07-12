namespace LionTrust.Feature.Search.Models.API.Response
{
    using LionTrust.Foundation.Search.Models.API;
    using Newtonsoft.Json;

    public class ArticleFacetsResponse : FacetsResponse
    {
        public Dates Dates = new Dates();
    }
}