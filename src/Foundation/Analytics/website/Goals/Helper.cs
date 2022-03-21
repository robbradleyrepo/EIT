using Sitecore.Analytics;
using Sitecore.Data;

namespace LionTrust.Foundation.Analytics.Goals
{
    public static class Helper
    {
        public static void TriggerGoal(ID goalId)
        {
            //Check if tracker is active or not
            if (!Tracker.IsActive)
            {
                Tracker.StartTracking();
            }

            if (!Tracker.IsActive || Tracker.Current.CurrentPage == null)
            {
                return;
            }

            var goalItem = Sitecore.Context.Database.GetItem(goalId);
            if (goalItem == null)
            {
                return;
            }

            var goalTrigger = Tracker.MarketingDefinitions.Goals[goalItem.ID.ToGuid()];
            if (goalTrigger == null)
            {
                return;
            }

            var goalEventData = Tracker.Current.CurrentPage.RegisterGoal(goalTrigger);
            goalEventData.Data = goalItem["Name"];
            goalEventData.ItemId = goalItem.ID.ToGuid();
            goalEventData.DataKey = goalItem.Paths.Path;
            goalEventData.Text = goalItem["Description"];
            Tracker.Current.Interaction.AcceptModifications();
        }
    }
}
