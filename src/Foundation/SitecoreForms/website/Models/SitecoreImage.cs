namespace LionTrust.Foundation.SitecoreForms.Models
{
    using System;
    using Sitecore.Collections;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Web;
    using Sitecore.Web.UI.WebControls;
    using SitecoreFormImage = LionTrust.Foundation.SitecoreForms.Models.Image;

    public class SitecoreImage : IProperty<SitecoreFormImage>
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly Item _item;

        public SitecoreImage(Guid id, string name, SitecoreFormImage value)
        {
            _id = id;
            _name = name;
            Value = value;
        }

        public SitecoreImage(Item item, string name, SitecoreFormImage value)
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

        public SitecoreFormImage Value
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
            var paramDict = new SafeDictionary<string>();
            if (Value != null && Value.MaxWidth > 0)
            {
                paramDict.Add("mw", Value.MaxWidth.ToString());
            }
            if (Value != null && Value.MaxHeight > 0)
            {
                paramDict.Add("mh", Value.MaxHeight.ToString());
            }

            renderer.Parameters = WebUtil.BuildQueryString(paramDict, false);

            return renderer.Render();
        }
    }
}
