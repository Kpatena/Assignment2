using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel.CourseOption;
using DiplomaDataModel.CourseOption.Seed;
using Microsoft.AspNet.Identity;
using DiplomaOptions.Models;

namespace DiplomaOptions.Controllers
{
    public class ChoicesController : Controller
    {
        private CourseOptionContext db = new CourseOptionContext();
        private ApplicationDbContext userdb = new ApplicationDbContext();

        // GET: Choices
        [Authorize(Roles = "Admin")]
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
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // GET: Choices/Create
        [Authorize(Roles = "Student, Admin")]
        public ActionResult Create()
        {
            string term;

            //Get options that have IsActive that are true
            var options = db.Options.Where(p => p.IsActive);
            
            //Get the year term that has IsDefault that equals to true
            var yearTerm = db.YearTerms.FirstOrDefault(p => p.IsDefault);
            
            //Get the user student id
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = userdb.Users.FirstOrDefault(x => x.Id == currentUserId);
            ViewBag.StudentId = currentUser.UserName;

            //Makes out the term number into specified terms
            switch (yearTerm.Term)
            {
                case 10:
                    term = "Winter";
                    break;
                case 20:
                    term = "Spring/Summer";
                    break;
                case 30:
                    term = "Fall";
                    break;
                default:
                    term = "Chaos";
                    break;
            }
            ViewBag.YearTerm = term + " " + yearTerm.Year;
            ViewBag.FirstChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(options, "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(options, "OptionId", "Title");
            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student, Admin")]
        public ActionResult Create([Bind(Include = "StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId")] Choice choice)
        {
            string term;
            bool choicesDiff;
            bool notEntered;

            var yearTerm = db.YearTerms.FirstOrDefault(p => p.IsDefault);

            var choiceTable = db.Choices.FirstOrDefault(c => c.YearTermId == yearTerm.YearTermId && c.StudentId == choice.StudentId);

            switch (yearTerm.Term)
            {
                case 10:
                    term = "Winter";
                    break;
                case 20:
                    term = "Spring/Summer";
                    break;
                case 30:
                    term = "Fall";
                    break;
                default:
                    term = "Chaos";
                    break;
            };
          
            //look if year term is already in table if 
            if (choiceTable != null) { 
                
                 notEntered = false;
                
            } else
            {
                notEntered = true;
            }

            //Checking if the choices are different from each other
            if ((choice.FirstChoiceOptionId != choice.SecondChoiceOptionId && choice.FirstChoiceOptionId != choice.ThirdChoiceOptionId &&
                     choice.FirstChoiceOptionId != choice.FourthChoiceOptionId) && (choice.SecondChoiceOptionId != choice.ThirdChoiceOptionId
                     && choice.SecondChoiceOptionId != choice.FourthChoiceOptionId) && (choice.ThirdChoiceOptionId != choice.FirstChoiceOptionId))
            {
                choicesDiff = true;
            }
            else
            {
                choicesDiff = false;
            }

            if (ModelState.IsValid)
            {
                //if choices are different and not entered in year/term already else give back error
                if (choicesDiff)
                {
                    if (notEntered)
                    {
                        choice.YearTermId = yearTerm.YearTermId;
                        choice.SelectionDate = DateTime.Now;
                        db.Choices.Add(choice);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
              
            }

            if(!notEntered)
            {
                ViewData["term_error"] = "Already entered into term!";
            }

            if (!choicesDiff)
            {
                ViewData["choice_error"] = "Cannot choose the same course option more than once!";
            }

            var options = db.Options.Where(p => p.IsActive);

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = userdb.Users.FirstOrDefault(x => x.Id == currentUserId);
            ViewBag.StudentId = currentUser.UserName;

            ViewBag.YearTerm = term + " " + yearTerm.Year;
            ViewBag.FirstChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
            return View(choice);
        }

        // GET: Choices/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            var options = db.Options.Where(p => p.IsActive);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = userdb.Users.FirstOrDefault(x => x.Id == currentUserId);
            ViewBag.StudentId = currentUser.UserName;

            ViewBag.FirstChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
            return View(choice);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            bool choicesDiff;

            if ((choice.FirstChoiceOptionId != choice.SecondChoiceOptionId && choice.FirstChoiceOptionId != choice.ThirdChoiceOptionId &&
                    choice.FirstChoiceOptionId != choice.FourthChoiceOptionId) && (choice.SecondChoiceOptionId != choice.ThirdChoiceOptionId
                    && choice.SecondChoiceOptionId != choice.FourthChoiceOptionId) && (choice.ThirdChoiceOptionId != choice.FirstChoiceOptionId))
            {
                choicesDiff = true;
            }
            else
            {
                choicesDiff = false;
            }

            if (ModelState.IsValid)
            {
                if(choicesDiff)
                {
                    db.Entry(choice).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            if (!choicesDiff)
            {
                ViewData["choice_error"] = "Cannot choose the same course option more than once!";
            }

            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
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
    }
}
