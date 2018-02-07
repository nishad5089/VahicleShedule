using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Identity.Controllers
{
   
    public class SheduleVahiclesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SheduleVahicles
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {


                if (!isAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            var sheduleVahicles = db.SheduleVahicles.Include(s => s.Shift).Include(s => s.VahicleInfo);
            return View(sheduleVahicles.ToList());
        }
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        // GET: SheduleVahicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SheduleVahicle sheduleVahicle = db.SheduleVahicles.Find(id);
            if (sheduleVahicle == null)
            {
                return HttpNotFound();
            }
            return View(sheduleVahicle);
        }

        // GET: SheduleVahicles/Create
        public ActionResult Create()
        {

            if (User.Identity.IsAuthenticated)
            {


                if (!isAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


            ViewBag.ShiftId = new SelectList(db.VahicleShifts, "Id", "ShiftTime");
            ViewBag.VahicleInfoId = new SelectList(db.VahicleInfo, "Id", "RegNo");
            return View();
        }

        // POST: SheduleVahicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SheduleVahicle sheduleVahicle)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {


                    if (!isAdminUser())
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

                db.SheduleVahicles.Add(sheduleVahicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShiftId = new SelectList(db.VahicleShifts, "Id", "ShiftTime", sheduleVahicle.ShiftId);
            ViewBag.VahicleInfoId = new SelectList(db.VahicleInfo, "Id", "RegNo", sheduleVahicle.VahicleInfoId);
            return View(sheduleVahicle);
        }

        // GET: SheduleVahicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SheduleVahicle sheduleVahicle = db.SheduleVahicles.Find(id);
            if (sheduleVahicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShiftId = new SelectList(db.VahicleShifts, "Id", "ShiftTime", sheduleVahicle.ShiftId);
            ViewBag.VahicleInfoId = new SelectList(db.VahicleInfo, "Id", "RegNo", sheduleVahicle.VahicleInfoId);
            return View(sheduleVahicle);
        }

        // POST: SheduleVahicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VahicleInfoId,SheduleDate,ShiftId,BookedBy,Address")] SheduleVahicle sheduleVahicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sheduleVahicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShiftId = new SelectList(db.VahicleShifts, "Id", "ShiftTime", sheduleVahicle.ShiftId);
            ViewBag.VahicleInfoId = new SelectList(db.VahicleInfo, "Id", "RegNo", sheduleVahicle.VahicleInfoId);
            return View(sheduleVahicle);
        }

        // GET: SheduleVahicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SheduleVahicle sheduleVahicle = db.SheduleVahicles.Find(id);
            if (sheduleVahicle == null)
            {
                return HttpNotFound();
            }
            return View(sheduleVahicle);
        }

        // POST: SheduleVahicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SheduleVahicle sheduleVahicle = db.SheduleVahicles.Find(id);
            db.SheduleVahicles.Remove(sheduleVahicle);
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
        /*
         * customize
         */
        public ActionResult ViewShedule()
        {
            ViewBag.VahicleList = db.VahicleInfo.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewShedule(int vhicleId)
        {
            /*ViewBag.SheduleList = db.SheduleVahicles.Where(m => m.VahicleInfoId == vhicleId).ToList();*/
         
            ViewBag.VahicleList = db.VahicleInfo.ToList();

            /*     List<SheduleVahicle> SheduleList = db.SheduleVahicles.Where(m => m.VahicleInfoId == vhicleId).ToList();*/
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;
            List<SheduleVahicle> SheduleList = db.SheduleVahicles.Where(m => m.VahicleInfoId == vhicleId && m.SheduleDate >= date).ToList();
            return View(SheduleList);
        }
    }
}
