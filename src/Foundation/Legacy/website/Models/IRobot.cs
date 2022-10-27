using Glass.Mapper.Sc.Configuration.Attributes;
using LionTrust.Foundation.ORM.Models;

namespace LionTrust.Foundation.Legacy.Models
{
    public interface IRobot : ILegacyGlassBase
    {
        [SitecoreField(Constants.Robot.Content_FieldId)]
        string Content { get; set; }
    }
}