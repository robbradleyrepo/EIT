using System;

namespace LionTrust.Feature.Hero
{
    /// <summary>
    /// https://staticreadonly.com/2019/02/06/structures-vs-static-classes-for-sitecore-template-references/
    /// </summary>
    public static class Constants
    {
        public static class Hero
        {
            public const string HeroImagesFieldId = "{522DFB98-05DE-44B8-821D-2E392CFFD875}";
            public const string HeroTemplateId = "{462BB765-F578-4D46-A47B-20D16A1BFD94}";
            public const string HeroTitleFieldId = "{6968B632-46DF-4DE2-A129-D9637CCA094F}";
            
            public static readonly Guid TemplateId = new Guid(HeroTemplateId);
        }

        public static class FundManagerHero
        {
            public const string FundManagerHeroTemplateId = "{864CA206-2FC7-4D45-83E1-B7B7A8690770}";
            public const string FundManagerFirstNameFieldId = "{F420C908-8C33-4D99-A0C4-8AB9C1CD54F4}";
            public const string FundManagerSecondNameFieldId = "{1AE2288A-187B-4B53-B78C-FEBEBD844D64}";
            public const string FundManagerBackgroundImageFieldId = "{6C3A7C12-47EA-43E8-9FCB-E453D5F331CE}";

            public static readonly Guid TemplateId = new Guid(FundManagerHeroTemplateId);
        }

        public static class Views
        {
            public const string HeroView = "~/Views/Navigation/Hero.cshtml";
            public const string FundManagerHeroView = "~/Views/Navigation/FundManagerHero.cshtml";
        }
    }

    public static class Logging
    {
        public static class Error
        {
            public const string DataSourceError = "The Hero datasource was empty";
        }
    }

    public static class MediatorCodes
    {
        public static class HeroResponse
        {
            public const string DataSourceError = "HeroMediator.CreateHeroViewModel.DataSourceError";
            public const string ViewModelError = "HeroMediator.CreateHeroViewModel.ViewModelError";
            public const string Ok = "HeroMediator.CreateHeroViewModel.Ok";
        }
    }
}
