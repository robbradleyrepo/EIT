namespace LionTrust.Feature.Article.Models
{
    using System;
    using System.Linq;
    using LionTrust.Foundation.ORM.Extensions;
    using LionTrust.Foundation.Legacy;
    using LionTrust.Foundation.Schema.Models;

    public class ArticleViewModel
    {
        public IArticleHeader ComponentData { get; set; }

        public IArticle ArticleData { get; set; }

        public string BackgroundImageStyle
        {
            get
            {
                if (ComponentData != null && ComponentData.BackgroundImage != null && !string.IsNullOrEmpty(ComponentData.BackgroundImage.Src))
                {
                    return ComponentData.BackgroundImage.GetSafeBackgroundImageStyle();
                }
                else
                {
                    return ArticleData?.HeaderImage?.GetSafeBackgroundImageStyle();
                }
            }
        }

        public bool IsFundUpdate
        {
            get
            {
                if (ArticleData != null && ArticleData.PromoType != null && ArticleData.PromoType.Id.ToString("D").ToUpper().Equals(Constants.Article.FundUpdateContentTypeId))
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsLearning
        {
            get
            {
                if (ArticleData != null && ArticleData.PromoType != null && ArticleData.PromoType.Id.ToString("D").ToUpper().Equals(Constants.Article.LearningContentTypeId))
                {
                    return true;
                }

                return false;
            }
        }

        public DateTime ArticleDate
        {
            get
            {
                if (ArticleData == null || ArticleData.Date == null || ArticleData.Date == DateTime.MinValue)
                {
                    return DateTime.Now;
                }

                return ArticleData.Date;
            }
        }

        public ArticleSchema ArticleSchema { get; set; }

        public bool ShowAuthors { get; set; }
    }
}
