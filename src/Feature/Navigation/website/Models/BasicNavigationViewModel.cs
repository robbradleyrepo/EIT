﻿using LionTrust.Foundation.Navigation.Models;
using LionTrust.Foundation.Schema.Models;
using System.Collections.Generic;

namespace LionTrust.Feature.Navigation.Models
{
    public class BasicNavigationViewModel
    {
        public IHomeBase HomeItem { get; set; }
        public OrganizationSchema Organization { get; set; }
        public IEnumerable<INavigablePage> MenuItems { get; set; }
    }
}