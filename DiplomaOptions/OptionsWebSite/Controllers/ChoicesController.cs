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
    public class ChoicesController : Controller
    {
        private OptionPickerContext db = new OptionPickerContext();

        // GET: Choices
        [Authorize]
        public ActionResult Index()
        {
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerm);
            return View(choices.ToList());
        }


        // GET: Choices/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerm);
            Choice choice = choices.Where(c => c.ChoiceId == id).First();
            if (choice == null)
            {
                return HttpNotFound();
            }

            if (choice.YearTerm.Term == 10)
            {
                ViewBag.yeartermDetail = choice.YearTerm.Year + " Winter";
            } else if (choice.YearTerm.Term == 20)
            {
                ViewBag.yeartermDetail = choice.YearTerm.Year + " Sping/Summer";
            } else if (choice.YearTerm.Term == 30)
            {
                ViewBag.yeartermDetail = choice.YearTerm.Year + " Fall";
            }

            return View(choice);
        }

        // GET: Choices/Create
        [Authorize]
        public ActionResult Create()
        {
            var validOptions = getActiveOptions();
            ViewBag.FirstChoiceOptionId = new SelectList(validOptions, "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(validOptions, "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(validOptions, "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(validOptions, "OptionId", "Title");

            Dictionary<string, object> yearTermValues = getUsefulYearTerm();
            ViewBag.yearTermId = yearTermValues["yearTermId"];
            ViewBag.yearTermName = yearTermValues["yearTermName"];
            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId")] Choice choice)
        {
            choice.SelectionDate = DateTime.Now;

            bool isValid = true;
            if (!validChoices(choice))
            {
                ModelState.AddModelError("", "Cannot pick duplicate options");
                isValid = false;
            }

            if (!multiPick(choice))
            {
                ModelState.AddModelError("", "Cannot pick option for the same year term");
                isValid = false;
            }
  
            if (ModelState.IsValid && isValid)
            {
                db.Choices.Add(choice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var validOptions = getActiveOptions();
            ViewBag.FirstChoiceOptionId = new SelectList(validOptions, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(validOptions, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(validOptions, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(validOptions, "OptionId", "Title", choice.ThirdChoiceOptionId);

            Dictionary<string, object> yearTermValues = getUsefulYearTerm();
            ViewBag.yearTermId = yearTermValues["yearTermId"];
            ViewBag.yearTermName = yearTermValues["yearTermName"];

            return View(choice);
        }

        // GET: Choices/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            var validOptions = getActiveOptions();
            ViewBag.FirstChoiceOptionId = new SelectList(validOptions, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(validOptions, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(validOptions, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(validOptions, "OptionId", "Title", choice.ThirdChoiceOptionId);

            IEnumerable<SelectListItem> termItems = db.YearTerms.Select(c => new SelectListItem()
            {
                Value = c.YearTermId.ToString(),
                Text = (c.Term == 10 ? "Winter " + c.Year :
                        c.Term == 20 ? "Spring/Summer " + c.Year :
                        c.Term == 30 ? "Fall " + c.Year : "Error"),
            });
            ViewBag.YearTermID = new SelectList(termItems, "Value", "Text", choice.YearTermId.ToString());

            return View(choice);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            bool isValid = true;
            if (!validChoices(choice))
            {
                ModelState.AddModelError("", "Cannot pick duplicate options");
                isValid = false;
            }

            if (ModelState.IsValid && isValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Retrieve only the active choices
            var validOptions = getActiveOptions();

            ViewBag.FirstChoiceOptionId = new SelectList(validOptions, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(validOptions, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(validOptions, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(validOptions, "OptionId", "Title", choice.ThirdChoiceOptionId);

            IEnumerable<SelectListItem> termItems = db.YearTerms.Select(c => new SelectListItem()
            {
                Value = c.YearTermId.ToString(),
                Text = (c.Term == 10 ? "Winter " + c.Year :
                c.Term == 20 ? "Spring/Summer " + c.Year :
                c.Term == 30 ? "Fall " + c.Year : "Error"),
            });
            ViewBag.YearTermID = new SelectList(termItems, "Value", "Text", choice.YearTermId.ToString());
            return View(choice);
        }

        // GET: Choices/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
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

        // Check for non-duplicate options
        private bool validChoices(Choice choice)
        {           
            var list = new List<int>();
            list.Add((int)choice.FirstChoiceOptionId);
            list.Add((int)choice.SecondChoiceOptionId);
            list.Add((int)choice.ThirdChoiceOptionId);
            list.Add((int)choice.FourthChoiceOptionId);

            if (list.Count != list.Distinct().Count())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //build collection of active options
        private IQueryable<Option> getActiveOptions()
        {
            return db.Options.Where(c => c.isActive == true);
        }

        //generate useful year term string according to Term number
        private string getYearTermPair(int termVal, int year)
        {
            string pair = "";

            switch (termVal)
            {
                case 10:
                    pair = "Winter " + year;
                    break;
                case 20:
                    pair = "Spring/Summer " + year;
                    break;
                case 30:
                    pair = "Fall " + year;
                    break;
                default:
                    break;
            }

            return pair;
        }

        //making year term pair
        private Dictionary<string, object> getUsefulYearTerm()
        {
            var current = db.YearTerms.Where(c => c.isDefault == true).First();
            var yearTermId = current.YearTermId;
            var yearTermVal = current.Term;
            var yearTermYr = current.Year;
            var yearTermName = "";

            yearTermName = getYearTermPair(yearTermVal, yearTermYr);

            Dictionary<String, Object> dict = new Dictionary<string, object>();
            dict.Add("yearTermId", yearTermId);
            dict.Add("yearTermName", yearTermName);

            return dict;
        }

        //check if the same user has made choices for the same year and term
        private bool multiPick(Choice choice)
        {
            if (choice != null)
            {
                var sameStudentYearTerm = db.Choices.Where(c => c.StudentId == choice.StudentId 
                && c.YearTermId == choice.YearTermId).Count();

                if (sameStudentYearTerm != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
