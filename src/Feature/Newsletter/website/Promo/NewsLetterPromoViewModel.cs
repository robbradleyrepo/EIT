using System;

namespace LionTrust.Feature.Newsletter.Promo
{
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

        public string BackgroundImageUrl
        {
            get
            {
                if (Data?.BackgroundImage == null)
                {
                    return string.Empty;
                }

                return Data?.BackgroundImage.Src;
            }
        }
    }
}