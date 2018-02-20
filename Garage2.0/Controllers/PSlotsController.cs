using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage2._0.Models;

namespace Garage2._0.Controllers
{
    public class PSlotsController : Controller
    {
        private Garage2_0Context db = new Garage2_0Context();

        // GET: PSlots
        public ActionResult Index()
        {
            var pSlots = db.PSlots.Include(p => p.Member).Include(p => p.VehicleType);
            return View(pSlots.ToList());
        }

        // GET: PSlots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PSlot pSlot = db.PSlots.Find(id);
            if (pSlot == null)
            {
                return HttpNotFound();
            }
            return View(pSlot);
        }

        // GET: PSlots/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "SSN");
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "TypeOfVehicle");
            return View();
        }

        // POST: PSlots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,VehicleTypeId,ParkingSlot,VehicleRegistrationNumber,VehicleBrand,VehicleModel,Color,TiresOnVehicle,StartParking")] PSlot pSlot)
        {
            if (ModelState.IsValid)
            {
                db.PSlots.Add(pSlot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "SSN", pSlot.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "TypeOfVehicle", pSlot.VehicleTypeId);
            return View(pSlot);
        }

        // GET: PSlots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PSlot pSlot = db.PSlots.Find(id);
            if (pSlot == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "SSN", pSlot.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "TypeOfVehicle", pSlot.VehicleTypeId);
            return View(pSlot);
        }

        // POST: PSlots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,VehicleTypeId,ParkingSlot,VehicleRegistrationNumber,VehicleBrand,VehicleModel,Color,TiresOnVehicle,StartParking")] PSlot pSlot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pSlot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "SSN", pSlot.MemberId);
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "TypeOfVehicle", pSlot.VehicleTypeId);
            return View(pSlot);
        }

        // GET: PSlots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PSlot pSlot = db.PSlots.Find(id);
            if (pSlot == null)
            {
                return HttpNotFound();
            }
            return View(pSlot);
        }

        // POST: PSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PSlot pSlot = db.PSlots.Find(id);
            db.PSlots.Remove(pSlot);
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
