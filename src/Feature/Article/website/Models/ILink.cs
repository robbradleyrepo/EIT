namespace LionTrust.Feature.Article.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using LionTrust.Foundation.ORM.Models;

    public interface ILink: IGlassBase
    {
        [SitecoreField(Constants.Link.LinkFieldId)]
        Link Link { get; set; }
    }
}
