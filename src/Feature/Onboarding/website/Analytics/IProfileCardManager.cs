using LionTrust.Foundation.Onboarding.Models;
using Sitecore.Analytics.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionTrust.Feature.Onboarding.Analytics
{
    public interface IProfileCardManager
    {
        void AddPointsFromProfileCard(IProfileCard profileCard, Profile profile);        
    }
}
