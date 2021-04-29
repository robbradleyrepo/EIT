namespace LionTrust.Feature.Article.Models
{
    using LionTrust.Foundation.Legacy.Models;

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

                return ArticleData.Author.FullName;
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
                return this.ArticleData?.Author?.Image.Src;
            }
        }
    }
}
