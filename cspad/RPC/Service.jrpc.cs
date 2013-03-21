using System.Web;
using JsonFx.JsonRpc;
using cspad.web.Services;

namespace cspad.web.RPC
{
    [JsonService(Namespace = "cspad", Name = "rpc")]
    public class Service
    {
        [JsonMethod(Name = "save")]
        public bool Save(string code)
        {
            var key = HttpContext.Current.Request.UrlReferrer.AbsolutePath.Substring(1);

            return PasteService.Save(key, code);
        }

        [JsonMethod(Name = "clone")]
        public string Clone(string code)
        {
            var parentKey = HttpContext.Current.Request.UrlReferrer.AbsolutePath.Substring(1);

            var paste = PasteService.Clone(parentKey, code);

            return ZBase32.Encode(paste.PasteId);
        }
    }
}