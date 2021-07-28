namespace LionTrust.Foundation.Contact.Models.Types
{
    using System;
    using System.Globalization;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Web.UI.WebControls;

    class SitecoreText : IProperty<string>
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly Item _item;

        public SitecoreText(Guid id, string name, string value)
        {
            _id = id;
            _name = name;
            Value = value;
        }
        public SitecoreText(Item item, string name, string value)
        {
            _item = item;
            _name = name;
            _id = item.ID.Guid;
            Value = value;
        }

        public Guid Id
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string Value
        {
            get;
            set;
        }

        public string Render(bool disableWebEditing = false)
        {
            var item = _item ?? Sitecore.Context.Item;

            if (!item.ID.Guid.Equals(_id))
            {
                item = Sitecore.Context.Database.GetItem(new ID(_id));
            }
            var renderer = new FieldRenderer { Item = item, FieldName = _name, DisableWebEditing = disableWebEditing };

            var renderResult = renderer.Render();

            if (!Sitecore.Context.PageMode.IsExperienceEditor)
            {
                renderResult = ReplaceSitecoreTokens(renderResult);
            }

            return renderResult;
        }

        private string ReplaceSitecoreTokens(string text)
        {
            text = text.Replace(Constants.SitecoreTokens.CurrentYear, DateTime.Now.Year.ToString(CultureInfo.InvariantCulture));
            return text;
        }
    }
}
