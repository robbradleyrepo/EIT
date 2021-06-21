using LionTrust.Foundation.Legacy.Models;
using System.Collections.Generic;

namespace LionTrust.Feature.Fund.Models
{
    public class LiteratureViewModel
    {
        public LiteratureViewModel(ILiterature literature)
        {
            Literature = literature;
            Documents = new Dictionary<string, List<IDocument>>();
        }

        public ILiterature Literature { get; }

        public string FundName
        {
            get
            {
                return Literature?.Fund?.Name;
            }
        }

        public Dictionary<string, List<IDocument>> Documents { get; set; }

        public string CtaText
        {
            get
            {
                return Literature?.Cta?.Text;
            }
        }      
    }
}