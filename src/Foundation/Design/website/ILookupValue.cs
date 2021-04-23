namespace LionTrust.Foundation.Design
{
    using Glass.Mapper.Sc.Configuration.Attributes;
    using LionTrust.Foundation.ORM.Models;

    public interface ILookupValue: IGlassBase
    {
        [SitecoreField(Constants.LookupValues.ValueFieldId)]
        string Value { get; set; }
    }
}
