namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface ISignature : IExmGlassBase
    {
        [SitecoreField(Constants.Signature.Name_FieldID)]
        string FullName { get; set; }

        [SitecoreField(Constants.Signature.JobPosition_FieldID)]
        string JobPosition { get; set; }

        [SitecoreField(Constants.Signature.Email_FieldID)]
        string Email { get; set; }

        [SitecoreField(Constants.Signature.Phone_FieldID)]
        string Phone { get; set; }
    }
}