namespace LionTrust.Feature.Text.Models
{
    using Glass.Mapper.Sc.Configuration.Attributes;

    public interface IScriptModule : ITextGlassBase
    {
        [SitecoreField(Constants.ScriptModule.Title_FieldId)]
        string Title { get; set; }

        [SitecoreField(Constants.ScriptModule.Src_FieldId)]
        string Src { get; set; }

        [SitecoreField(Constants.ScriptModule.Id_FieldId)]
        string ScriptId { get; set; }       
    }
}