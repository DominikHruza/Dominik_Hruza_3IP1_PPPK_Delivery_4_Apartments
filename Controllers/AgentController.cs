using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Appartments.Controllers
{
    public class AgentController : Controller
    {

        private readonly ModelContainer db = new ModelContainer();
        ~AgentController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }

        // GET: Agent
        public ActionResult All()
        {
            return View(db.Agents);
        }

        // GET: Agent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Agent agent = db.Agents
               .Include(a => a.AgentFiles)
               .SingleOrDefault(a => a.IDAgent == id);

            if (agent == null)
            {
                return HttpNotFound();
            }

            return View(agent);
        }

        // GET: Agent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agent/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Firstname, Lastname, Email")] Agent agent,
            IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                agent.AgentFiles = new List<AgentFile>();
                foreach (var file in files)
                {

                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new AgentFile
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        agent.AgentFiles.Add(picture);
                    }
                }
                db.Agents.Add(agent);
                db.SaveChanges();
            }
            return RedirectToAction("All");
        }

        // GET: Agent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents
                .Include(a => a.AgentFiles)
                .SingleOrDefault(a => a.IDAgent == id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Agent/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(int id, IEnumerable<HttpPostedFileBase> files)
        {
            Agent agentToUpdate = db.Agents.Find(id);
            if (TryUpdateModel(agentToUpdate, "",
                            new string[] { "Firstname", "Lastname", "Email" }))
            {

                if (agentToUpdate.AgentFiles == null)
                {
                    agentToUpdate.AgentFiles = new List<AgentFile>();
                }

                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new AgentFile
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        agentToUpdate.AgentFiles.Add(picture);
                    }
                }

                db.Entry(agentToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("All");
            }

            return View(agentToUpdate);
        }

        // GET: Agent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents
                .Include(a => a.AgentFiles)
                .SingleOrDefault(a => a.IDAgent == id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Agent/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            IQueryable<Appartment> agentAppartments = db.Appartments.Where(a => a.AgentID == id);
            db.AgentFiles.RemoveRange(db.AgentFiles.Where(f => f.AgentID == id));

            
            foreach(Appartment appartment in agentAppartments)
            {
                db.AppartmentFiles.RemoveRange(
                    db.AppartmentFiles.Where(f => f.AppartmentID == appartment.IDAppartment));
            }
            db.Appartments.RemoveRange(agentAppartments);
           
            db.Agents.Remove(db.Agents.Find(id));
            db.SaveChanges();
            return RedirectToAction("All");
        }
    }
}
