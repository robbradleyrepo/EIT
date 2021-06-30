namespace LionTrust.Feature.Search.Controllers
{
    using Sitecore.Analytics;
    using Sitecore.Analytics.Data;
    using Sitecore.Data;
    using System;
    using System.Web.Http;

    public class SearchAnalyticsController: ApiController
    {
        public class SaveRequest
        {
            public Guid PageId { get; set; }

            public string Query { get; set; }
        }

        public IHttpActionResult Save(SaveRequest request)
        {
            if (request == null || request.PageId == null || string.IsNullOrEmpty(request.Query))
            {
                return BadRequest();
            }

            if (Tracker.IsActive)
            {
                var pageEventItem = Sitecore.Context.Database.GetItem(new ID(request.PageId));
                if (pageEventItem == null)
                {
                    return NotFound();
                }

                var pageEventData = new PageEventData("Search", Search.Constants.SearchAnalytics.SearchPageEvent)
                {
                    ItemId = pageEventItem.ID.ToGuid(),
                    Data = request.Query,
                    DataKey = request.Query,
                    Text = request.Query
                };

                var interaction = Tracker.Current.Session.Interaction;
                if (interaction != null)
                {
                    interaction.CurrentPage.Register(pageEventData);
                }
            }

            return Ok(); 
        }
    }
}