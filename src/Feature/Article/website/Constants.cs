namespace LionTrust.Feature.Article
{
    using System;

    public static class Constants
    {
        public static class ArticlePage
        {
            public static Guid ArticlePageTemplateId = new Guid("{51BDA851-7256-4BDE-B294-AE2A0CE45682}");
        }

        public static class Image
        {
            public const string ImageFieldId = "{48CE5243-5DC9-4B3C-B27F-EA89478F21A5}";
            public const string OpacityFieldId = "{B59F2B9F-73B9-4BED-9E7A-900D0CDB125D}";
            public const string MarginsContainerFieldId = "{ED739C9B-001A-4E72-BC67-4141BDEF057E}";
        }

        public static class ArticleRichText
        {
            public const string CopyFieldId = "{6876726D-D5D2-40DF-94A7-A9A6FAE2F683}";
        }

        public static class ArticleText
        {
            public const string CopyFieldId = "{8D298C2B-5646-4EEB-8F1B-2A81BCE70E01}";
        }

        public static class TextBase
        {
            public const string TextColorFieldId = "{D5A1D36A-6823-4646-81CF-958CEB0128B0}";
        }

        public static class ArticleFilter
        {
            public const string TemplateId = "{B0C943DC-16B9-44A0-B4ED-92D7492B3813}";
            public const string FundTeamsFieldId = "{0E5031B3-9232-42BE-82D4-05579D7827D9}";
            public const string FundsFieldId = "{D65BC812-CEB9-4C78-A2FB-EBC7788CCDE1}";
            public const string ContentTypesFieldId = "{A03D4F9E-4BDA-401D-9639-DD0247F50FAB}";
            public const string FundManagersFieldId = "{71469DCB-2347-4FA2-883B-50F4DF0E5244}";
            public const string TopicsFieldId = "{550B2192-38E6-4CEE-85F2-0BFCA295BCF8}";
        }

        public static class Link
        {
            public const string LinkFieldId = "{7DB38AE3-3B88-459D-8C62-DD656D7DEC8E}";
        }

        public static class FeaturedArticles
        {
            public const string HeadingFieldId = "{B2950181-27E6-4EEF-A1FA-329AA73FCE55}";
            public const string ArticlesFieldId = "{88BDA130-09AE-4E0E-8FD0-253A7924143E}";
        }        

        public static class ArticleHeader
        {            
            public const string BackgroundImageFieldId = "{30451F6B-437D-4473-AE3E-79FE7A1AF36A}";
        }

        public static class Article
        {
            public const string TopicsFieldId = "{4F9B280F-A22D-4E70-886A-45327E8AA507}";
            public const string MultipleAuthorsSetting_FieldId = "{B6833539-8781-4788-92F4-D173F0727FEB}";
            public const string ImageOpacity_FieldId = "{FE72ED1F-986F-475E-A40F-B95DA77091B8}";
            public const string Date_FieldId = "{79CC76CC-7503-449B-9809-9808CF6C2D26}";
            public const string PdfDocument_FieldId = "{AE17B17D-0024-4700-9075-E95D8E7B15EE}";
            public const string ArticleVideoUrl = "{8EB78475-C445-4960-84B0-E946D262F81C}";
            public const string PercentageScrollFieldId = "{C522ADF8-B03B-41DF-9D55-7C1DE30E6C3D}";
            public const string PercentageGoalFieldId = "{729A677E-787C-4B48-966F-D545AFAE6620}";
        }

        public static class ArticleScroller
        {
            public const string Heading_FieldId = "{93854C18-0919-445E-BC88-2D353C8491E8}";
            public const string CTA_FieldId = "{BAD318C3-B9B5-457F-A42D-F51516FC3EC2}";
            public const string SelectedArticles_FieldId = "{725DC6C8-8878-4947-86B3-163E51C5911D}";
            public const string SelectedTags_FieldId = "{23CB5FCD-BCFA-4A06-AE30-8991CC839A4C}";            
        }        

        public static class RelatedArticles
        {
            public const string HeadingFieldId = "{9B7BD172-7140-40A2-B464-782C99CF7E73}";
            public const string ArticlesFieldId = "{8146922E-1AF4-48E9-91CD-CA5F37CAB4E5}";
        }

        public static class ArticleLinks
        {
            public const string ArticleSharing_FieldId = "{1D03C959-0BBF-4C3A-A5A5-9050E9F4B38F}";
            public const string DownloadLabel_FieldId = "{338376F5-AB65-4249-878A-E891F1FD465A}";
            public const string DownloadGoal_FieldId = "{BB708901-B592-4F8C-9119-7F619B26D5B1}";
        }

        public static class FundManagerInsights
        {
            public const string SelectedArticles_FieldId = "{C83820F8-FE31-440B-BDB9-61698D79C066}";
        }

        public static class FundManagerInsightsBase
        {
            public const string CTA_FieldId = "{6E1628B0-EBD1-45C5-AD2F-3E0AA6FC19A4}";
            public const string Heading_FieldId = "{1492465C-0E3C-4FDB-97B8-0FC76924E400}";
        }

        public static class FeaturedArticleHero
        {
            public const string Article_FieldId = "{AACB640C-6457-4A38-81E2-7AE97DB0DB3A}";
            public const string BackgroundImage_FieldId = "{74A7E935-2A62-4192-B046-958C1ADDB98B}";
            public const string ArticleLinkText_FieldId = "{447AB9EA-F782-41A7-8269-E2AA6EF9862B}";
        }

        public static class ArticleShareLink
        {
            public const string Label_FieldId = "{D83E057F-DDBD-4603-9C36-56932E3626D7}";
            public const string Tooltip_FieldId = "{191B3D6F-C419-407E-8B6D-C69F364FED92}";
            public const string Goal_FieldId = "{C4EA3176-6C3D-4EE4-8924-034F5664117F}";
        }

        public static class PromoType
        {
            public const string ArticleType_FieldId = "{0B122919-9295-4AF9-A77D-4EFC952ED25B}";
        }

        public static class MultipleAuthorsSetting
        {
            public const string Label_FieldId = "{89BA5B06-2D92-4329-BCBA-9DE16C450CA9}";
            public const string Icon_FieldId = "{F7431439-3BF3-4D7D-A4FB-B1B6745A233D}";
        }

        public static class PageTypes
        {
            public const string HomeTemplateId = "{51D3A578-6E10-4EC6-BB5B-5C1307A6D661}";
        }        
    }
}