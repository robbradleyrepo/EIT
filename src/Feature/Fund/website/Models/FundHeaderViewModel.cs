namespace LionTrust.Feature.Fund.Models
{
    using LionTrust.Foundation.Legacy.Models;
    using System.Collections.Generic;

    public class FundHeaderViewModel
    {
        public FundHeaderViewModel(IFundHeader header)
        {
            this.Data = header;
        }

        public IFundHeader Data { get; private set; }

        public IEnumerable<IAuthor> Managers 
        { 
            get        
            {
                if (Data == null)
                {
                    return new IAuthor[0];
                }

                if (Data.FundManager != null)
                {
                    return new IAuthor[1] { Data.FundManager };
                }

                if (Data.Fund == null)
                {
                    return new IAuthor[0];
                }

                return Data.Fund.FundManagers;
            }
        }
    }
}