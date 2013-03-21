using System;
using System.Web.UI.HtmlControls;
using System.Web;

namespace cspad.web
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected override void OnPreRender(EventArgs e)
        {
 	         base.OnPreRender(e);

            // improve the Yslow rating
            JsonFx.Handlers.ResourceHandler.EnableStreamCompression(this.Context);

        }

        protected override void OnError(EventArgs e)
        {
            // remove compression
            JsonFx.Handlers.ResourceHandler.DisableStreamCompression(this.Context);

            base.OnError(e);
        }

        public void InitializePage(string _title, string _description, string _keywords)
        {
            InitializePage(_title, _description, _keywords, true);
        }

        public void InitializePage(string _title, string _description, string _keywords, bool allowRobots)
        {
            string title = "cspad - C# pastebin and code snippets";
            title = title.Substring(0, 1).ToUpper() + title.Substring(1);
            Page.Title = title;

            HtmlMeta keywords = new HtmlMeta();
            keywords.Name = "keywords";
            keywords.Content = HttpUtility.HtmlAttributeEncode(_keywords) + "," +
                               "C#, C Sharp, code, programming, visual studio, snippets, source code";

            HtmlMeta description = new HtmlMeta();
            description.Name = "description";
            description.Content = _description == null ? "" : HttpUtility.HtmlAttributeEncode(_description);

            HeadPlaceHolder.Controls.AddAt(0, description);
            HeadPlaceHolder.Controls.AddAt(1, keywords);

            if (!allowRobots)
            {
                //<META NAME="ROBOTS" CONTENT="NOINDEX, NOFOLLOW">
                HtmlMeta robots = new HtmlMeta();
                description.Name = "robots";
                description.Content = "NOINDEX, FOLLOW";
                HeadPlaceHolder.Controls.AddAt(2, description);
            }
        }
    }
}
