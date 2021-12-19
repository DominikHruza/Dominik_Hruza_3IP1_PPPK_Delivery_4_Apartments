using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appartments.Controllers
{
    public class AppartmentFilesController : Controller
    {

        private readonly ModelContainer db = new ModelContainer();
        
        ~AppartmentFilesController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }

        // GET: AppartmentFiles
        public ActionResult Index(int? id)
        {
            AppartmentFile appartmentFile = db.AppartmentFiles.Find(id);
            return File(appartmentFile.Content, appartmentFile.ContentType);
        }

        public ActionResult Delete(int id)
        {
            db.AppartmentFiles.Remove(db.AppartmentFiles.Find(id));
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}