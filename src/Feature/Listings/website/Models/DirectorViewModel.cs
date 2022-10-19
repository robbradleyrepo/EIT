using Glass.Mapper.Sc.Fields;
using System;

namespace LionTrust.Feature.Listings.Models
{
    public class DirectorViewModel
    {
        public Guid Id { get; set; }

        public IDirector Data { get; set; }

        public string Header { get; set; }

        public Image ImageOverlay { get; set; }

        public string ViewMoreLabel { get; set; }

        public string EmailLabel { get; set; }

        public string DirectLineLabel { get; set; }

        public string LinkedInLabel { get; set; }

        public Image LinkedInImage { get; set; }

        public DirectorViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}