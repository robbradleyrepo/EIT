namespace LionTrust.Feature.EXM.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IUserNameIntro : IExmGlassBase
    {
        [SitecoreField(Constants.UserNameIntro.Text_FieldID)]
        string Text { get; set; }       
    }
}