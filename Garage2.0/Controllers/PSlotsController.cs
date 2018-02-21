using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage2._0.Models;
using PagedList;
using PagedList.Mvc;

namespace PSlots2._0.Controllers
{
    public class PSlotsController : Controller
    {


        private Garage2_0Context db = new Garage2_0Context();

        public static int Capacity = 35;
        public static int MinutePrice = 2;

        public ActionResult Summary()
        {

            var model = db.PSlots.GroupBy(p => p.VehicleType.TypeOfVehicle)
                            .Select(g => new SummaryViewModel
                            {
                                Name = g.Key,
                                Count = g.Count(),
                                SumTires = g.Sum(f => f.TiresOnVehicle),
                                ParkingTime = g.Sum(h => DbFunctions.DiffMinutes(h.StartParking, DateTime.Now)),
                                AccumulatedRevenue = g.Sum(h => DbFunctions.DiffMinutes(h.StartParking, DateTime.Now) * MinutePrice)
                            }).ToList();

            return View(model);

        }


        // GET: PSlots
        public ActionResult Index(string searchBy, string search, int? page, string sortOrder)
        {

            ViewBag.FreeSlots = Capacity - db.PSlots.Where(g => (g.ParkingSlot > 0)).Count();
            ViewBag.Capacity = Capacity;


            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "ParkingSlot_desc" : "ParkingSlot";
            ViewBag.ParkingSlotSortParm = sortOrder == "ParkingSlot" ? "ParkingSlot_desc" : "ParkingSlot";
            ViewBag.TypeOfVehicleSortParm = sortOrder == "TypeOfVehicle" ? "TypeOfVehicle_desc" : "TypeOfVehicle";
            ViewBag.VehicleRegistrationNumberSortParm = sortOrder == "VehicleRegistrationNumber" ? "VehicleRegistrationNumber_desc" : "VehicleRegistrationNumber";
            ViewBag.VehicleBrandSortParm = sortOrder == "VehicleBrand" ? "VehicleBrand_desc" : "VehicleBrand";
            ViewBag.VehicleModelSortParm = sortOrder == "VehicleModel" ? "VehicleModel_desc" : "VehicleModel";
            ViewBag.ColorSortParm = sortOrder == "Color" ? "Color_desc" : "Color";
            ViewBag.TiresOnVehicleSortParm = sortOrder == "TiresOnVehicle" ? "TiresOnVehicle_desc" : "TiresOnVehicle";
            ViewBag.StartParkingSortParm = sortOrder == "StartParking" ? "StartParking_desc" : "StartParking";

            var vehicles = db.PSlots.OrderBy(v => v.ParkingSlot).AsQueryable();



            if (searchBy == "TypeOfVehicle")
            {
                // listsearch
                return View(db.PSlots.OrderBy(v => v.ParkingSlot).Where(v => v.VehicleType.TypeOfVehicle.ToString().Contains(search)
                || search == null).ToList().ToPagedList(page ?? 1, 10));
            }
            else if (searchBy == "VehicleRegistrationNumber")
            {
                return View(db.PSlots.OrderBy(v => v.ParkingSlot).Where(v => v.VehicleRegistrationNumber.Contains(search)
                || search == null).ToList().ToPagedList(page ?? 1, 10));
            }
            else if (searchBy == "VehicleBrand")
            {
                return View(db.PSlots.OrderBy(v => v.ParkingSlot).Where(v => v.VehicleBrand.Contains(search)
                || search == null).ToList().ToPagedList(page ?? 1, 10));
            }
            else
            {
                switch (sortOrder)
                {
                    case "ParkingSlot_desc":
                        vehicles = vehicles.OrderByDescending(s => s.ParkingSlot);
                        break;
                    case "ParkingSlot":
                        vehicles = vehicles.OrderBy(s => s.ParkingSlot);
                        break;
                    case "VehicleRegistrationNumber_desc":
                        vehicles = vehicles.OrderByDescending(s => s.VehicleRegistrationNumber);
                        break;
                    case "VehicleRegistrationNumber":
                        vehicles = vehicles.OrderBy(s => s.VehicleRegistrationNumber);
                        break;

                    case "TypeOfVehicle_desc":
                        vehicles = vehicles.OrderByDescending(s => s.VehicleType.TypeOfVehicle);
                        break;
                    case "TypeOfVehicle":
                        vehicles = vehicles.OrderBy(s => s.VehicleType.TypeOfVehicle);
                        break;
                    case "VehicleBrand_desc":
                        vehicles = vehicles.OrderByDescending(s => s.VehicleBrand);
                        break;
                    case "VehicleBrand":
                        vehicles = vehicles.OrderBy(s => s.VehicleBrand);
                        break;
                    case "VehicleModel_desc":
                        vehicles = vehicles.OrderByDescending(s => s.VehicleModel);
                        break;
                    case "VehicleModel":
                        vehicles = vehicles.OrderBy(s => s.VehicleModel);
                        break;
                    case "Color_desc":
                        vehicles = vehicles.OrderByDescending(s => s.Color);
                        break;
                    case "Color":
                        vehicles = vehicles.OrderBy(s => s.Color);
                        break;
                    case "TiresOnVehicle_desc":
                        vehicles = vehicles.OrderByDescending(s => s.TiresOnVehicle);
                        break;
                    case "TiresOnVehicle":
                        vehicles = vehicles.OrderBy(s => s.TiresOnVehicle);
                        break;
                    case "StartParking_desc":
                        vehicles = vehicles.OrderByDescending(s => s.StartParking);
                        break;
                    case "StartParking":
                        vehicles = vehicles.OrderBy(s => s.TiresOnVehicle);
                        break;

                    default:
                        vehicles = vehicles.OrderBy(s => s.ParkingSlot);
                        break;
                }

                return View(vehicles.ToList().ToPagedList(page ?? 1, 10));
            }

            // Dropdown searchfunction

            // https://www.youtube.com/watch?v=srN56uxw76s

            //SerchString for Paging


            // Ajax Action Methods

            //          var model = db.PSlots .OrderBy (e => e.TypeOfVehicle);
            //        return View(model.ToList());
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
        public ActionResult Park()
        {


        ViewBag.FreeSlots = Capacity - db.PSlots.Where(g => (g.ParkingSlot > 0)).Count();
        ViewBag.Capacity = Capacity;

        var model = db.PSlots.OrderBy(db => db.ParkingSlot);
        var usedSlots = model.Where(db => (db.ParkingSlot > 0));
        int tester1 = 0;
        int tester2 = 0;
        int tester3 = 0;
        ViewBag.FreeSlot = 0;
        foreach (var item in usedSlots)
        {
            tester2 = item.ParkingSlot;
            if (tester2 - tester1 > 1)
            {
                ViewBag.FreeSlot = tester2 - 1;
                break;
            }
            tester1++;
            tester3 = item.ParkingSlot;
        }
        if (ViewBag.FreeSlot == 0 && tester3 < Capacity)
            ViewBag.FreeSlot = tester3 + 1;
        var model2 = new PSlot{ ParkingSlot = ViewBag.FreeSlot };





        ViewBag.MemberId = new SelectList(db.Members, "Id", "SSN");
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "TypeOfVehicle");
            return View(model2);
        }

        // POST: PSlots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Park")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,VehicleTypeId,ParkingSlot,VehicleRegistrationNumber,VehicleBrand,VehicleModel,Color,TiresOnVehicle,StartParking")] PSlot pSlot)
        {

        ViewBag.FreeSlots = Capacity - db.PSlots.Where(g => (g.ParkingSlot > 0)).Count();
        ViewBag.Capacity = Capacity;

        var model = db.PSlots.OrderBy(db => db.ParkingSlot);
        var usedSlots = model.Where(db => (db.ParkingSlot > 0));
        int tester1 = 0;
        int tester2 = 0;
        int tester3 = 0;
        ViewBag.FreeSlot = 0;
        foreach (var item in usedSlots)
        {
            tester2 = item.ParkingSlot;
            if (tester2 - tester1 > 1)
            {
                ViewBag.FreeSlot = tester2 - 1;
                break;
            }
            tester1++;
            tester3 = item.ParkingSlot;
        }
        if (ViewBag.FreeSlot == 0 && tester3 < Capacity)
            ViewBag.FreeSlot = tester3 + 1;
        var model2 = new PSlot{ ParkingSlot = ViewBag.FreeSlot };





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
        public ActionResult Unpark(int? id)
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

        ViewBag.timeInPSlots = Math.Round((DateTime.Now - db.PSlots.Find(id).StartParking).TotalHours, 2);
        //ViewBag.CurrentCost = Math.Max(Math.Round(ViewBag.timeInPSlots*2*60, 0),45);
        ViewBag.CurrentCost = Math.Round(ViewBag.timeInPSlots * MinutePrice * 60, 0);
        ViewBag.CurrentPricingModel = MinutePrice + " SEK/minute";


        return View(pSlot);
        }

        // POST: PSlots/Delete/5
        [HttpPost, ActionName("Unpark")]
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
