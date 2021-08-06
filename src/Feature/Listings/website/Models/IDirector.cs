namespace LionTrust.Feature.Listings.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;

    public interface IDirector : IListingsGlassBase
    {
        [SitecoreField(Constants.Director.FullName_FieldId)]
        string FullName { get; set; }

        [SitecoreField(Constants.Director.Role_FieldId)]
        string Role { get; set; }

        [SitecoreField(Constants.Director.ShortBio_FieldId)]
        string ShortBio { get; set; }

        [SitecoreField(Constants.Director.Image_FieldId)]
        Image Image { get; set; }
    }
}