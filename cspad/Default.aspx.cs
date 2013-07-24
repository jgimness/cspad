using System;
using cspad.web.Services;
using System.Web;
using cspad.web.Code;

namespace cspad.web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Action newPaste = () =>
              {
                  var paste = PasteService.NewPaste();
                  Response.Redirect("/" + ZBase32.Encode(paste.PasteId));                  
              };

            var rc = RewriteModule.RewriteContext.Current;
            string key = rc.Params["key"];
            if(String.IsNullOrEmpty(key))
            {
                newPaste();
                return;
            }
            else
            {
                var paste = PasteService.GetPaste(key, true);

                if(paste == null)
                {
                    newPaste();
                    return;
                }

                string code = paste.CompressedText == null ? "" :
                    CompressionUtils.Decompress(paste.CompressedText.ToArray());

                CodeLiteral.Text = System.Web.HttpUtility.HtmlEncode(code);
                string href = "http://cspad.com/" + key;
                TwitterShareLink.HRef = HttpUtility.HtmlAttributeEncode(String.Format("http://twitter.com/home?status=Viewing some code at {0}", href));
                FacebookShareLink.HRef = HttpUtility.HtmlAttributeEncode(String.Format("http://www.facebook.com/sharer.php?u={0}&t=Viewing some code at cspad", href));
                ViewRawLink.HRef = HttpUtility.HtmlAttributeEncode(String.Format("{0}.raw", key));
                if (paste.Created.AddMinutes(2) >= DateTime.UtcNow) 
                {
                    CreatedTime.Visible = false;
                }
                else
                {
                    CreatedTime.InnerHtml = String.Format("created {0}", SharpTimeAgo.TimeAgo.GetTimeAgo(paste.Created, true));
                }

                if(paste.ViewCount < 2)
                {
                    ViewCount.Visible = false;
                }
                else
                {
                    ViewCount.InnerHtml = String.Format("{0} views", paste.ViewCount);
                }
            
                if(!SessionUtils.IsOwner(paste.PasteId))
                {
                    SaveContainer.Visible = false;
                    CloneContainer.Visible = true;
                }
                ShareLink.Value = href;
            }
        }
    }
}
