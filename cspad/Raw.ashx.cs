using System.Web;
using System.Web.Services;
using cspad.web.Services;
using System;

namespace cspad.web
{
    public class Raw : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string key = context.Request.QueryString["key"];
            if(String.IsNullOrEmpty(key))
            {
                // TODO: Write something
            }
            else
            {
                // TODO: Check access
                var paste = PasteService.GetPaste(key, false);

                string code = (paste == null || paste.CompressedText == null)
                                  ? ""
                                  : CompressionUtils.Decompress(paste.CompressedText.ToArray());
                context.Response.Write(code);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
