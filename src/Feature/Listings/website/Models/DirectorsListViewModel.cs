using System.Collections.Generic;

namespace LionTrust.Feature.Listings.Models
{
    public class DirectorsListViewModel
    {
        public IDirectorsList Data { get; set; }

        public string EmailLabel { get; set; }

        public string DirectLineLabel { get; set; }

        public string MobileLabel { get; set; }

        public IEnumerable<DirectorViewModel> Children { get; set; }
    }
}