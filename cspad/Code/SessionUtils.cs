using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cspad.web.Code
{
    public static class SessionUtils
    {
        public static bool IsOwner(int pasteId)
        {
            var s = HttpContext.Current.Session;
            return s["PasteList"] != null && (((List<int>) HttpContext.Current.Session["PasteList"]).Contains(pasteId));
        }

        internal static void AddPaste(int pasteId)
        {
            var s = HttpContext.Current.Session;
            if(s["PasteList"] == null)
            {
                s["PasteList"] = new List<int>();
            }

            ((List<int>) s["PasteList"]).Add(pasteId);
        }
    }
}
