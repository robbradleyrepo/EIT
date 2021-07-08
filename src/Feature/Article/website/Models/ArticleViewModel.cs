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

                if (ArticleData.Authors.Any() && ArticleData.Authors.Count() > 1)
                {
                    return ArticleData.MultipleAuthorsLabel;
                }
                
                return ArticleData.Authors?.FirstOrDefault()?.FullName;
            }
        }

        public string BackgroundImageUrl
        {
            get
            {
                return ComponentData?.BackgroundImage?.Src;
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
                if (ArticleData.Authors.Any() && ArticleData.Authors.Count() > 1)
                {
                    return ArticleData.MultipleAuthorsIcon?.Src;
                }

                return this.ArticleData?.Authors?.FirstOrDefault()?.Image?.Src;
            }
        }
    }
}
