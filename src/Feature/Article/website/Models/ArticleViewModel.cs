namespace LionTrust.Feature.Article.Models
{
    using System;
    using System.Linq;

    public class ArticleViewModel
    {
        public IArticleHeader ComponentData { get; set; }

        public IArticle ArticleData { get; set; }

        public string AuthorName
        {
            get
            {
                if (ArticleData == null || ArticleData.Authors == null)
                {
                    return string.Empty;
                }
                // TO DO multiple authors option
                return ArticleData.Authors?.FirstOrDefault()?.FullName;
            }
        }

        public string BackgroundImageUrl
        {
            get
            {
                if (ComponentData != null && ComponentData.BackgroundImage != null && !string.IsNullOrEmpty(ComponentData.BackgroundImage.Src))
                {
                    return ComponentData?.BackgroundImage?.Src;
                }
                else
                {
                    return ArticleData?.HeaderImage?.Src;
                }
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


        public string AuthorImageUrl
        {
            get
            {
                // TO DO multiple authors option
                return this.ArticleData?.Authors?.FirstOrDefault()?.Image?.Src;
            }
        }
    }
}
