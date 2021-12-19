using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Appartments.Controllers
{
    public class AppartmentController : Controller
    {

        private readonly ModelContainer db = new ModelContainer();
        

        ~AppartmentController()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }


        // GET: Appartment
        public ActionResult Index()
        {
            return View(db.Appartments);
        }

        // GET: Appartment/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Appartment apartment = db.Appartments
               .Include(a => a.AppartmentFiles)
               .SingleOrDefault(a => a.IDAppartment == id);

            if (apartment == null)
            {
                return HttpNotFound();
            }

            return View(apartment);
        }

        // GET: Appartment/Create
        public ActionResult Create()
        {
            var agentOptions = db.Agents.ToList();

            ViewBag.agentOptions = agentOptions;
            return View();
        }

        

        // POST: Appartment/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Address, City, AgentID")] Appartment apartment,
            IEnumerable<HttpPostedFileBase> files )
        {
            if (ModelState.IsValid)
            {
                apartment.AppartmentFiles = new List<AppartmentFile>();
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new AppartmentFile
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        apartment.AppartmentFiles.Add(picture);
                    }
                }
                db.Appartments.Add(apartment);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        // GET: Appartment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Appartment apartment = db.Appartments
                .Include(a => a.AppartmentFiles)
                .SingleOrDefault(a => a.IDAppartment == id);
            if (apartment == null)
            {
                return HttpNotFound();
            }

            var agentOptions = db.Agents.ToList();
            ViewBag.agentOptions = agentOptions;

            return View(apartment);
        }

        // POST: Apartments/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(int id, IEnumerable<HttpPostedFileBase> files)
        {
            Appartment apartmentToUpdate = db.Appartments.Find(id);
            if (TryUpdateModel(apartmentToUpdate, "",
                            new string[] { "Address", "City", "AgentID" }))
            {

                if (apartmentToUpdate.AppartmentFiles == null)
                {
                    apartmentToUpdate.AppartmentFiles = new List<AppartmentFile>();
                }

                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new AppartmentFile
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        apartmentToUpdate.AppartmentFiles.Add(picture);
                    }
                }

                db.Entry(apartmentToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apartmentToUpdate);
        }

        // GET: Apartments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Appartment apartment = db.Appartments
                .Include(a => a.AppartmentFiles)
                .SingleOrDefault(a => a.IDAppartment == id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.AppartmentFiles.RemoveRange(db.AppartmentFiles.Where(f => f.AppartmentID == id));
            db.Appartments.Remove(db.Appartments.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

     
    }
}
