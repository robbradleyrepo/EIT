using Glass.Mapper.Sc.Configuration.Attributes;
using LionTrust.Foundation.ORM.Models;

namespace LionTrust.Foundation.Legacy.Models
{
    public interface IEmail : IGlassBase
    {
        [SitecoreField(Constants.EmailTemplate.FromAddress_FieldId)]
        string FromAddress { get; set; }

        [SitecoreField(Constants.EmailTemplate.FromDisplayName_FieldId)]
        string FromDisplayName { get; set; }

        [SitecoreField(Constants.EmailTemplate.ToAddresses_FieldId)]
        string ToAddresses { get; set; }

        [SitecoreField(Constants.EmailTemplate.Subject_FieldId)]
        string Subject { get; set; }
    }
}