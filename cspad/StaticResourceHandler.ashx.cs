using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Globalization;

namespace cspad.web
{
    public class StaticResourceHandler : IHttpHandler
    {
        private static Dictionary<string, string> MIME_MAP;
        static StaticResourceHandler() {
            MIME_MAP = new Dictionary<string, string>();
            MIME_MAP[".png"] = "image/png";
            MIME_MAP[".gif"] = "image/gif";
            MIME_MAP[".jpeg"] = "image/jpeg";
            MIME_MAP[".jpg"] = "image/jpeg";
            MIME_MAP[".tiff"] = "image/tiff";
            MIME_MAP[".tif"] = "image/tiff";
            MIME_MAP[".ico"] = "image/x-icon";
            MIME_MAP[".css"] = "text/css";
        }

        public void ProcessRequest(HttpContext context)
        {
            var Request = context.Request;
            var Response = context.Response;

            string filePath = Request.PhysicalPath;

            // Check for invalid file name
            if (!File.Exists(filePath))
            {
                Response.StatusCode = 404;
                context.ApplicationInstance.CompleteRequest();
                return;
            }

            // Check for cached image
            var textIfModifiedSince = Request.Headers["If-Modified-Since"];
            if (!String.IsNullOrEmpty(textIfModifiedSince))
            {
                DateTime clientCachedTime = DateTime.ParseExact(textIfModifiedSince, "r", CultureInfo.InvariantCulture).ToLocalTime();

                if (clientCachedTime >= new FileInfo(filePath).LastWriteTime)
                {
                    Response.ClearContent();
                    Response.StatusCode = (int) System.Net.HttpStatusCode.NotModified;
                    Response.StatusDescription = "Not Modified";

                    // this safely ends request without causing "Transfer-Encoding: Chunked" which chokes IE6
				    context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }

            Response.ContentType = MIME_MAP[Path.GetExtension(filePath).ToLower()];
            Response.AddFileDependency(filePath);
            Response.Cache.SetETagFromFileDependencies();
            Response.Cache.SetLastModifiedFromFileDependencies();
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.Cache.SetExpires(DateTime.Now.AddMonths(1));

            Response.WriteFile(filePath);
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
