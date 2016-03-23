using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel.OptionPicker;

namespace OptionsWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class YearTermController : Controller
    {
        private OptionPickerContext db = new OptionPickerContext();

        // GET: YearTerm
        public ActionResult Index()
        {
            return View(db.YearTerms.ToList());
        }

        // GET: YearTerm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm == null)
            {
                return HttpNotFound();
            }
            return View(yearTerm);
        }

        // GET: YearTerm/Create
        public ActionResult Create()
        {
            List<SelectListItem> terms = new List<SelectListItem>()
            {
                new SelectListItem { Selected = false, Text = "Winter", Value = "10"},
                new SelectListItem { Selected = false, Text = "Spring/Summer", Value = "20"},
                new SelectListItem { Selected = false, Text = "Fall", Value = "30"},
            };

            ViewBag.Term = new SelectList(terms, "Value", "Text");

            return View();
        }

        // POST: YearTerm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YearTermId,Year,Term,isDefault")] YearTerm yearTerm)
        {
            if (yearTerm.isDefault)
            {
                var unchoosen = (from c in db.YearTerms
                                 where c.isDefault == true
                                 select c).First();
                unchoosen.isDefault = false;
            }
            else
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                db.YearTerms.Add(yearTerm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<SelectListItem> terms = new List<SelectListItem>()
            {
                new SelectListItem { Selected = false, Text = "Winter", Value = "10"},
                new SelectListItem { Selected = false, Text = "Spring/Summer", Value = "20"},
                new SelectListItem { Selected = false, Text = "Fall", Value = "30"},
            };

            ViewBag.Term = new SelectList(terms, "Value", "Text", yearTerm.Term);

            return View(yearTerm);
        }

        // GET: YearTerm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> terms = new List<SelectListItem>()
            {
                new SelectListItem { Selected = false, Text = "Winter", Value = "10"},
                new SelectListItem { Selected = false, Text = "Spring/Summer", Value = "20"},
                new SelectListItem { Selected = false, Text = "Fall", Value = "30"},
            };

            ViewBag.Term = new SelectList(terms, "Value", "Text", yearTerm.Term);
            return View(yearTerm);
        }

        // POST: YearTerm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YearTermId,Year,Term,isDefault")] YearTerm yearTerm)
        {
            if(yearTerm.isDefault)
            {
                var unchoosen = (from c in db.YearTerms
                         where c.isDefault == true
                         select c).First();
                unchoosen.isDefault = false;
            }
            else
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                db.Entry(yearTerm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> terms = new List<SelectListItem>()
            {
                new SelectListItem { Selected = false, Text = "Winter", Value = "10"},
                new SelectListItem { Selected = false, Text = "Spring/Summer", Value = "20"},
                new SelectListItem { Selected = false, Text = "Fall", Value = "30"},
            };

            ViewBag.Term = new SelectList(terms, "Value", "Text", yearTerm.Term);
            return View(yearTerm);
        }

        // GET: YearTerm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm == null)
            {
                return HttpNotFound();
            }
            return View(yearTerm);
        }

        // POST: YearTerm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YearTerm yearTerm = db.YearTerms.Find(id);
            db.YearTerms.Remove(yearTerm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
