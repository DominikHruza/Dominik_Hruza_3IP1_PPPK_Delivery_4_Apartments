using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appartments.Controllers
{
    public class AgentFilesController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();

        ~AgentFilesController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }



        // GET: AgenttFiles
        public ActionResult Index(int id)
        {
            AgentFile agentFile = db.AgentFiles.Find(id);
            return File(agentFile.Content, agentFile.ContentType);
        } 

        public ActionResult Delete(int id)
        {
            db.AgentFiles.Remove(db.AgentFiles.Find(id));
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}