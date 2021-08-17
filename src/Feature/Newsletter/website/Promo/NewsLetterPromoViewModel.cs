namespace LionTrust.Feature.Newsletter.Promo
{
    using Foundation.ORM.Extensions;
    using System;

    public class NewsletterPromoViewModel
    {
        public NewsletterPromoViewModel(INewsletterPromoModel data)
        {
            Data = data;
        }

        public INewsletterPromoModel Data { get; }

        public Guid GoalId
        {
            get
            {                   
                if (string.IsNullOrEmpty(Data?.Goal) || !Guid.TryParse(Data?.Goal, out var result))
                {
                    return Guid.Empty;
                }

                return result;
            }
        }

        public string BackgroundImageStyle
        {
            get
            {
                return Data?.BackgroundImage?.GetSafeBackgroundImageStyle();
            }
        }
    }
}