namespace LionTrust.Feature.Fund
{
    using System;

    public static class Constants
    {
        public static class PerformanceTable
        {
            public const string TableHeadingFieldId = "{24B821F2-896B-4BBE-AB3A-6C943A3D8B8F}";
            public const string QuartileRowLabelFieldId = "{21554471-1768-4C04-9A5D-A5FAB1B4D8F4}";
            public const string HeadingFieldId = "{450FEC5E-B30E-48AF-ADC4-051FD7B27289}";
            public const string ColumnMonthPrefixFieldId = "{8FF8ECB3-3CA7-47D7-89DF-76E1DF36BA79}";
        }

        public static class Literature
        {
            public const string HeadingFieldId = "{6B74C5E5-7F94-405C-9E38-2B38FCC30079}";
            public const string FundFieldId = "{1000613E-6F3E-4DB4-91EF-C1B5D447DEE2}";
            public const string CtaFieldId = "{EEED7C2E-DC3C-4E29-9855-03BD4F9139AD}";
        }

        public static class CreditRatingRow
        {
            public const string NameFieldId = "{5D4C8234-C349-44B9-AA47-652484734A53}";
            public const string ValueFieldId = "{17793BA8-E9B1-406B-B73D-E10C37D4D2C2}";
        }

        public static class CapitalisationChartEntry
        {
            public const string NameFieldId = "{7FF12937-2E4B-4FB0-BBED-3B27E399339E}";
            public const string ValueFieldId = "{0F549840-F03B-426E-A71F-71B009E78690}";
            public const string BackgroundColorFieldId = "{A5716B6A-3E75-48C6-8344-3F2BE2D0F6C6}";
        }

        public static class FundPerformanceGraph
        {
            public const string HeadingFieldId = "{46C68BF8-8D80-4959-81D6-0EECAE187697}";
            public const string FactsheetTextFieldId = "{09768300-13A0-46E6-BD44-474DB7AAA852}";
            public const string DetailUrlFieldId = "{7BA56456-C580-4919-8B04-9156BB341B4C}";
        }

        public static class CreditRating
        {
            public const string HeadingFieldId = "{26BD09DF-992E-40F9-A625-77F17A237AC7}";
            public const string MaxValueFieldId = "{EA8D41F6-9599-4413-A778-8B37F5582E17}";
        }

        public static class CapitalisationChart
        {
            public const string HeadingFieldId = "{0C319F29-5F95-4061-849B-E27634734863}";
        }

        public static class GraphWithHeading
        {
            public const string HeadingFieldId = "{AA344985-91C2-4371-AEE9-85A678700BC1}";
        }

        public static class FundCard
        {
            public const string HeadingFieldId = "{6387CA9A-7A5A-4D33-B706-201681C0B74C}";
            public const string ImageFieldId = "{EB75DFEF-C624-4571-B92B-8F37C36FCEE6}";
            public const string DescriptionFieldId = "{0905F092-B5B5-4971-B87E-57F4810BD729}";
            public const string FundFieldId = "{034CD348-2ADC-47E9-B263-2C839808DAE1}";
            public const string CTAFieldId = "{76AF5024-2B12-421A-96CD-46E756F1DB9A}";
        }

        public static class FundHeader
        {
            public const string FundFieldId = "{90BCAB34-B9E0-466E-97A4-73CEC67335F7}";
            public const string TitleFieldId = "{1ADF8B0C-A198-4CC7-81CC-CFAAC0FE3483}";
            public const string FundManagerFieldId = "{7A30D0A3-23FD-4863-99FD-A17525112A0E}";
            public const string ShareTextFieldId = "{651FF26A-8AF8-449E-B831-109F2F9C7851}";
            public const string ShareCopiedTextFieldId = "{8505F318-1D55-472B-ABEB-A4B4B372E2B2}";
            public const string BackgroundImageFieldId = "{5D55EE9C-7778-47E7-833D-9D692E0E8AAB}";
        }

        public static class FundDetailNavigation
        {
            public const string HeadingFieldId = "{88BD7907-3F4A-40EC-BB50-E24DF11609C5}";
        }

        public static class FundScroller
        {
            public const string HeadingFieldId = "{8E11C5F7-1FCA-4A99-AD05-B81511F973D9}";
            public const string DescriptionFieldId = "{451EFCD2-40A3-4153-8B02-BEC9572A9EC1}";
            public const string SubtitleFieldId = "{EB9E31A0-ED28-48FC-8CBF-820264B17306}";
            public const string FundsFieldId = "{DD98D557-6E80-4FFB-9E13-498E29DE5566}";

            public const string PrimaryCaFieldId = "{63C464EC-3657-4B91-ADF5-28AC9036774F}";
            public const string PrimaryCtaGoalFieldId = "{7DB368D2-CCCF-4423-80FC-2065EA9FA547}";
            public const string SecondaryCtaFieldId = "{E8A3C14F-9976-4734-9FE8-5BB53E1DC9B3}";
            public const string SecondaryCtaGoalId = "{0528E27D-0ACA-4D49-A01A-E3993A81764F}";
            public const string TextColourFieldId = "{81691A12-5AE2-433A-AEB4-7D7F5D4713E4}";
            public const string BackgroundImageFieldId = "{CF042356-7423-4D3E-A295-4F175C40DF99}";
        }

        public static class MyFundsScroller
        {
            public const string MaxFundsFieldId = "{77B3427C-896C-4F53-9DA4-7F8354D861D1}";
        }

        public static class FundManager
        {
            public static Guid TemplateId = Guid.Parse("{A0702CF0-48EB-48A9-8E89-8FDBCD2B9F1D}");
            public const string ManagerFieldId = "{6B4AFD6F-A05F-4141-8E9D-EC71E79A3936}";
        }

        public static class FundOverview
        {
            public const string LatestUpdateCTA_FieldId = "{624E51CE-F2EE-48A1-B49F-EB4A852B713F}";
            public const string DownloadCTA_FieldId = "{361F876B-0D9B-4827-A7AD-F9245A3A79A2}";
            public const string FundFieldId = "{8730606F-4236-4C61-8859-16CB35541365}";
        }

        public static class FundClassSelector
        {
            public const string DropDownLabelFieldId = "{306EAF63-2D34-4733-872C-2E086009E717}";
            public const string FundFieldId = "{D765263B-CE31-4E99-8AB3-E1D4594A9D01}";
        }

        public static class FourFundStats
        {
            public const string FundFieldId = "{1AE48213-0A33-405E-8575-EF6369491FA8}";
            public const string FundLaunchDateLabel_FieldId = "{41A6502D-CE3F-4938-9746-1B6D0EB9A057}";
            public const string FundSizeLabel_FieldId = "{3DC4DB39-AF37-4BDC-B8A8-7C06458BB9B5}";
            public const string NumberOfHoldingsLabel_FieldId = "{4D0F2400-C512-4457-BBE4-AFE776D25FEA}";
            public const string ManagedByCurrentTeamForLabel_FieldId = "{8E7A0A72-4F42-4FC3-8C5F-BB44152FD189}";
            public const string ManagedByCurrentTeamForValue_FieldId = "{B141A98C-B927-4C25-AA97-6314C24A974F}";
        }

        public static class FourFundStatsManual
        {
            public const string Heading_FieldId = "{06DA2ADC-4F41-4674-A432-50ADD4AE5220}";
            public const string FirstTitle_FieldId = "{7057D082-6FF9-4FCC-BC25-6E68D2E504D3}";
            public const string FirstValue_FieldId = "{C19C8970-62A2-4FBA-AAFA-BE7B112B2DCC}";
            public const string SecondTitle_FieldId = "{96BC6D5C-4CF5-4C01-9A05-84E935F76673}";
            public const string SecondValue_FieldId = "{CAA6C4AD-6997-4C8D-A16F-CD400FF7A355}";
            public const string ThirdTitle_FieldId = "{4770ED05-A29D-41A4-AC27-5E6F1F2105C5}";
            public const string ThirdValue_FieldId = "{C66D9A07-8DEE-483B-AD1D-5AA7AFFE3A34}";
            public const string FourthTitle_FieldId = "{AB724582-2780-4933-BA84-7B19502D8FA4}";
            public const string FourthValue_FieldId = "{CF15FA34-EB8F-4747-9BAC-35109A04FB6D}";
        }

        public static class FundPage
        {
            public static Guid TemplateId = Guid.Parse("{0CF5E9F6-FEE0-472C-B1D3-C4C68E20AA4D}");
            public const string PageFieldId = "{E43F6E86-0318-4766-B880-0C4F6512CA4E}";
        }

        public static class HoldingsTable
        {
            public const string HeadingFieldId = "{74B0C999-733F-4EDB-B896-D160EA93B6C8}";
        }

        public static class GraphSection
        {
            public const string HeadingFieldId = "{9D8219A9-94C4-41CA-9B93-7F4416D5A8CA}";
        }

        public static class FundSelector
        {
            public static Guid TemplateId = new Guid("{BC7B0647-141D-499D-964B-6F46DB539031}");
            public const string FundFieldId = "{3F407D7E-838C-4D99-A6AD-890B6A463037}";
        }

        public static class KeyFundDocuments
        {
            public const string Title_FieldId = "{10B9FA41-4F22-4FD2-81BE-EE105DFB4329}";
            public const string Description_FieldId = "{760716CF-64D5-4C0D-9DFF-2DBB9F0D476A}";
            public const string KeyDocuments_FieldId = "{4189960D-9AAE-42E3-8008-3EF3309002D6}";

            public const string ViewAllDocumentsLabel_FieldId = "{99B18B81-5C97-47E1-9EDC-9F83C5F49947}";
        }

        public static class FundManagers
        {
            public const string TitleFieldId = "{4EF859A1-6CD4-4213-8EAB-94CC974C6C08}";
            public const string FundManagersFieldId = "{742FA605-EC20-4EC8-87FD-9E7DAEB8BC84}";
        }

        public static class AdditionalInfoAndCharges
        {
            public const string ExDividendDate_FieldId = "{4BA432B4-B112-4A9F-9A06-6637F2211A9C}";
            public const string DistributionDate_FieldId = "{C3148BFB-6ED3-477A-B20E-BCA1446C7AC6}";
            public const string SedolCode_FieldId = "{429E1E3F-3D06-4CCE-919B-7D32BAB4E104}";
            public const string ISINCode_FieldId = "{8C679D78-EECB-492E-9F38-DEE0127CA516}";
            public const string BloombergCode_FieldId = "{BB426EFD-6B71-49A7-81FF-2CF062D29FD3}";
            public const string InitialCharge_FieldId = "{FA7F0518-4B26-4502-8BC1-281E7C2885FF}";
            public const string OngoingCharges_FieldId = "{557C0FB4-0CC7-4D50-A316-93A2BD948860}";
            public const string IncludedOFC_FieldId = "{C3006883-37FD-4736-8A68-977E5608AB1A}";
        }

        public static class SectorBreakdown
        {
            public const string HeadingFieldId = "{FE72E4F7-8806-4907-B17F-AA124431957F}";
        }

        public static class FundManagerPromo
        {
            public const string MangerFieldId = "{87A9CDD9-1B0D-466E-AA0C-36927B1954E4}";
        }

        public static class AdditionalInfoAndChargesComponent
        {
            public const string AdditionalInfoLabel_FieldId = "{CE9B820F-2C4E-458D-9A8C-C9C070389669}";
            public const string MinInitialLabel_FieldId = "{B790F7BC-1FA0-4B65-B59F-535DC77D9A86}";
            public const string MinAdditionalLabel_FieldId = "{23206349-163D-422B-8C88-4F3DF0E71B86}";
            public const string ExDividendDateLabel_FieldId = "{028D35EF-F418-4F05-A578-3F994B8CC68B}";
            public const string DistributionDateLabel_FieldId = "{50FF3D78-8147-4809-B0F4-95C8C07E21EA}";
            public const string SedolCodeLabel_FieldId = "{88273FD0-CEDC-4AE2-94A5-E4711FE083F6}";
            public const string ISINCodeLabel_FieldId = "{EAAC87D1-9F78-4756-B187-16F1D7E427FA}";
            public const string BloombergCodeLabel_FieldId = "{7C02B9A0-038C-41B6-ABC3-BFCB7DBB7CCA}";
            public const string ChargesLabel_FieldId = "{387F89AE-2912-44ED-84DC-4662A8ED3522}";
            public const string InitialChargeLabel_FieldId = "{CC91AAA4-ED02-49F4-9027-551ABFFDF4D4}";
            public const string OngoingChargesLabel_FieldId = "{5F132CC3-50F5-42F1-8308-9C751685F733}";
            public const string IncludedOFCLabel_FieldId = "{1417C905-79BB-487B-AF65-2209B53803F6}";
        }

        public static class KeyInfoPriceComponent
        {
            public const string KeyInformationLabel_FieldId = "{D1C8B6ED-F85E-4645-82EE-B239CFE6E9AA}";
            public const string ClassLaunchDateLabel_FieldId = "{FF6466A5-B6D9-499C-A338-42DB7AA720CF}";
            public const string ComparatorLabel_FieldId = "{B27C9CC5-F16C-4A75-852D-3A9B6FDBE497}";
            public const string Benchmark1Label_FieldId = "{85A7C8E7-FFFB-4EE9-9372-17DE13A3CEFA}";
            public const string FundSizeLabel_FieldId = "{9A89E450-3210-4694-8520-345EE6263302}";
            public const string NumberOfHoldingsLabel_FieldId = "{3368266D-AB74-4ACE-B89F-4C11488E8AC5}";
            public const string NetUnderlyingYieldLabel_FieldId = "{7F6507B6-3D95-413C-B85B-655688F19DB7}";
            public const string DurationLabel_FieldId = "{58D81F04-60A8-470A-8880-6D7DC3F4A460}";
            public const string PriceLabel_FieldId = "{435D1D54-8A35-4B13-B9CB-C59CA752F272}";
            public const string SinglePriceLabel_FieldId = "{6F5E00C3-5757-4E8C-A479-A753137A1BA1}";
            public const string OfferPriceLabel_FieldId = "{678F3E21-91D6-4021-BD2A-26C33B839CE1}";
            public const string PriceDateLabel_FieldId = "{D093B57A-44CA-4867-B0D5-52DB25E3CC11}";
            public const string PriceDateTooltipLabel_FieldId = "{80C12439-AAA4-4CAD-8787-AB7B35BA60B9}";
        }

        public static class KeyInfoPrice
        {
            public const string ClassLaunchDate_FieldId = "{009E0E82-76BC-4B6A-A326-6FB97237BA50}";
            public const string Comparator_FieldId = "{37BB71CA-84B0-4D80-9E3D-320A32C9D6C1}";
            public const string Duration_FieldId = "{7E88256A-2E5D-42EB-B340-7983804209B8}";
            public const string SinglePrice_FieldId = "{ACF74586-B31A-4812-A440-C4C50A6E4F70}";
            public const string OfferPrice_FieldId = "{B834FC27-880B-4ACD-BC10-D6158773B285}";
            public const string PriceDate_FieldId = "{366E54EF-CC6F-4F52-8953-6DDDEA6ED408}";
        }

        public static class TwoColumnComponent
        {
            public const string LeftColumnSize_FieldId = "{F6A1DC1F-D9C0-40B8-9590-D5C58D1F2346}";
            public const string LeftPaddingSize_FieldId = "{0B7847A5-F42B-42E0-B4D0-3CB4350CEE45}";
            public const string RightColumnSize_FieldId = "{1A02D0A6-1E81-4119-AB01-A0E90D5EB8A8}";
            public const string RightPaddingSize_FieldId = "{D3A375F2-CB82-4F2B-AF60-550FC662A00D}";
        }

        public static class FundDisclaimer
        {
            public const string SmallSize_FieldId = "{06013D0B-DB4E-474E-86E7-BB771D5E7577}";
            public const string TextColor_FieldId = "{98D89D89-CA0F-41FF-A3BF-E0F22F240335}";
        }

        public static class SalesforceFund
        {
            public const string SalesforceFundId_FieldId = "{CDEA92C0-C31B-4E6D-BC15-A772FF2AFA95}";
        }

        public static class FundCentre
        {
            public const string IFrameRootUrl_FieldId = "{96028547-DF10-4FEA-BC33-3C73107E64E5}";
        }

        public static class FundAccessPopUp
        {
            public const string Title_FieldId = "{09F68CB9-E658-46ED-BB71-06F8F2FFBC63}";
            public const string BackButtonText_FieldId = "{D6B06D59-9338-4D49-A00F-841D94E096CE}";
            public const string PrimaryCTA_FieldId = "{0068183C-62B7-412C-8B96-B6C1FEB2B3C7}";
            public const string SecondaryCTA_FieldId = "{11C16829-F61D-4638-A5AB-F50A2C21319C}";
        }
    }
}