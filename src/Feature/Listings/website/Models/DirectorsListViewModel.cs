using Glass.Mapper.Sc.Fields;
using System.Collections.Generic;

namespace LionTrust.Feature.Listings.Models
{
    public class DirectorsListViewModel
    {
        public IDirectorsList Data { get; set; }

        public IEnumerable<DirectorViewModel> Children { get; set; }

        public Image LinkedInImage { get; set; }
    }
}