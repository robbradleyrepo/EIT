﻿namespace LionTrust.Foundation.Contact.Models
{
    using System.Collections.Generic;

    public class SFCountryListViewModel
    {
        public SFCountryListViewModel()
        {
            CountryList = new List<SFCountryListItem>();
        }

        public List<SFCountryListItem> CountryList { get; set; }
    }
}