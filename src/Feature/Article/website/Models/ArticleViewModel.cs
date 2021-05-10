namespace LionTrust.Feature.Article.Models
{
    using LionTrust.Foundation.Article.Models;
    using System.Linq;

    public class ArticleViewModel
    {
        public IArticleHeader ComponentData { get; set; }

        public IArticle ArticleData { get; set; }

        public string AuthorName
        {
            get
            {
                if (ArticleData == null || ArticleData.Author == null)
                {
                    return string.Empty;
                }
                // TO DO multiple authors option
                return ArticleData.Author?.FirstOrDefault().FullName;
            }
        }

        public string BackgroundImageUrl
        {
            get
            {
                return ComponentData?.BackgroundImage?.Src;
            }
        }

        public string AuthorImageUrl
        {
            get
            {
                // TO DO multiple authors option
                return this.ArticleData?.Author?.FirstOrDefault()?.Image.Src;
            }
        }
    }
}
