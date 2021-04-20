namespace LionTrust.Foundation.Design
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface ILookupValue
    {
        [SitecoreField(Constants.LookupValues.ValueFieldId)]
        string Value { get; set; }
    }
}
