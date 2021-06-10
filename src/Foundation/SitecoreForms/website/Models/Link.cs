namespace LionTrust.Foundation.SitecoreForms.Models
{
    using System;
    using System.Web;

    public class Link
    {
        public string Url { get; set; }
        public string Text { get; set; }
        public string Css { get; set; }
        public string TextToReplace { get; set; }
        public Guid TargetItemId { get; set; }
        public bool IsInternal { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(Text);
        }
        public void ExtendText(string additionalText)
        {
            TextToReplace = Text + additionalText;
        }

        public void ReplaceText(string newText, bool withEncoding)
        {
            if (withEncoding)
            {
                TextToReplace = HttpUtility.UrlEncode(newText);
            }
            else
            {
                TextToReplace = HttpUtility.UrlEncode(newText);
            }
        }


        public void CreateButton(bool hasArrow, string css)
        {
        }
    }
}
