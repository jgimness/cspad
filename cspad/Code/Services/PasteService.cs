using System;
using System.Linq;
using cspad.web.Model;
using CSharpVitamins;
using cspad.web.Code;

namespace cspad.web.Services
{
    public static class PasteService
    {
        //public static int Publish(string code)
        //{
        //    var db = new ModelDataContext();

        //    var paste = new Model.Paste
        //                    {
        //                        Created = DateTime.UtcNow,
        //                        LastAccess = DateTime.UtcNow,
        //                        CompressedText = CompressionUtils.Compress(code),
        //                        ViewCount = 0
        //                    };

        //    db.Pastes.InsertOnSubmit(paste);
        //    db.SubmitChanges();

        //    return paste.PasteId;
        //}

        internal static Model.Paste NewPaste()
        {
            var db = new ModelDataContext();
            string guid = ShortGuid.NewGuid();

            var paste = new Model.Paste
            {
                Created = DateTime.UtcNow,
                LastAccess = DateTime.UtcNow,
                ViewCount = 0,
                Guid = guid, 
                CreatorIp = Genovix.Utils.WebUtils.GetIPAddress()
            };

            db.Pastes.InsertOnSubmit(paste);
            db.SubmitChanges();

            SessionUtils.AddPaste(paste.PasteId);

            return paste;
        }

        internal static Model.Paste GetPaste(string key, bool updateViews)
        {
            int id = ZBase32.Decode(key);
            ModelDataContext db = new ModelDataContext();

            var paste = db.Pastes.FirstOrDefault(f => f.PasteId == id);
            if(paste != null)
            {
                if (updateViews)
                {
                    paste.ViewCount++;
                }

                paste.LastAccess = DateTime.UtcNow;
                db.SubmitChanges();
            }

            return paste;
        }

        internal static bool Save(string key, string code)
        {
            int id = ZBase32.Decode(key);
            ModelDataContext db = new ModelDataContext();

            var paste = db.Pastes.FirstOrDefault(f => f.PasteId == id);
            if(paste != null)
            {
                // Must be owner to make changes
                if (!SessionUtils.IsOwner(paste.PasteId)) { return false; }

                paste.CompressedText = CompressionUtils.Compress(code);
                db.SubmitChanges();
                return true;
            }

            return false;
        }

        internal static Model.Paste Clone(string parentKey, string code)
        {
            int parentId = ZBase32.Decode(parentKey);
            var db = new ModelDataContext();
            string guid = ShortGuid.NewGuid();

            var paste = new Model.Paste
            {
                Created = DateTime.UtcNow,
                LastAccess = DateTime.UtcNow,
                ViewCount = 0,
                Guid = guid,
                ParentId = parentId,
                CompressedText = CompressionUtils.Compress(code),
                CreatorIp = Genovix.Utils.WebUtils.GetIPAddress()
            };

            db.Pastes.InsertOnSubmit(paste);
            db.SubmitChanges();

            SessionUtils.AddPaste(paste.PasteId);

            return paste;
        }

        internal static Paste NewPaste(string key)
        {
            throw new NotImplementedException();
        }
    }
}