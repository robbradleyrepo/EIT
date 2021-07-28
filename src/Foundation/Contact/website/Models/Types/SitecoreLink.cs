namespace LionTrust.Foundation.Contact.Models.Types
{
    using System;
    using Sitecore.Collections;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Web;
    using Sitecore.Web.UI.WebControls;

    public class SitecoreLink : IProperty<Link>
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly Item _item;

        public SitecoreLink(Guid id, string name, Link value)
        {
            _id = id;
            _name = name;
            Value = value;
        }

        public SitecoreLink(Item item, string name, Link value)
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

        public Link Value
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

            var renderer = new FieldRenderer { Item = item, FieldName = _name, DisableWebEditing = disableWebEditing, };

            var paramDict = new SafeDictionary<string>();


            if (!string.IsNullOrEmpty(Value.TextToReplace))
            {
                paramDict.Add("text", Value.TextToReplace);
            }
            if (!string.IsNullOrEmpty(Value.Css))
            {
                paramDict.Add("class", Value.Css);
            }

            renderer.Parameters = WebUtil.BuildQueryString(paramDict, false);

            return renderer.Render();
        }
    }
}
