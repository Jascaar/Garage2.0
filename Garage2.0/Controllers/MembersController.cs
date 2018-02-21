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

namespace Garage2._0.Controllers
{
    public class MembersController : Controller
    {
        private Garage2_0Context db = new Garage2_0Context();

        // GET: Members
        public ActionResult Index(string searchBy, string search, int? page, string sortOrder)
        {

            ViewBag.SSNSortParm = String.IsNullOrEmpty(sortOrder) ? "SSN_desc" : "SSN";
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "LastName_desc" : "LastName";
            ViewBag.CellularSortParm = sortOrder == "Cellular" ? "Cellular_desc" : "Cellular";
            ViewBag.SignUpTimeSortParm = sortOrder == "SignUpTime" ? "SignUpTime_desc" : "SignUpTime";
            ViewBag.PostCodeSortParm = sortOrder == "PostCode" ? "PostCode_desc" : "PostCode";
            ViewBag.CitySortParm = sortOrder == "City" ? "City_desc" : "City";
            ViewBag.CountrySortParm = sortOrder == "Country" ? "Country_desc" : "Country";
            ViewBag.GenderSortParm = sortOrder == "Gender" ? "Gender_desc" : "Gender";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.StreetSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.StreetNumberSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.OfficialApartmentNumberSortParm = sortOrder == "Email" ? "Email_desc" : "Email";

            var members = db.Members.OrderBy(v => v.SSN).AsQueryable();



            if (searchBy == "SSN")
            {
                // listsearch
                return View(db.Members.OrderBy(v => v.SSN).Where(v => v.SSN.Replace("-", "").Contains(search)
                || search == null).ToList().ToPagedList(page ?? 1, 10));
            }
            else if (searchBy == "LastName")
            {
                return View(db.Members.OrderBy(v => v.LastName).Where(v => v.LastName.Contains(search)
                || search == null).ToList().ToPagedList(page ?? 1, 10));
            }
            else if (searchBy == "City")
            {
                return View(db.Members.OrderBy(v => v.City).Where(v => v.City.Contains(search)
                || search == null).ToList().ToPagedList(page ?? 1, 10));
            }
            else
            {
                switch (sortOrder)
                {
                    case "SSN_desc":
                        members = members.OrderByDescending(s => s.SSN);
                        break;
                    case "SSN":
                        members = members.OrderBy(s => s.SSN);
                        break;
                    case "FirstName_desc":
                        members = members.OrderByDescending(s => s.FirstName);
                        break;
                    case "FirstName":
                        members = members.OrderBy(s => s.FirstName);
                        break;
                    case "Cellular_desc":
                        members = members.OrderByDescending(s => s.FirstName);
                        break;
                    case "Cellular":
                        members = members.OrderBy(s => s.FirstName);
                        break;

                    case "LastName_desc":
                        members = members.OrderByDescending(s => s.LastName);
                        break;
                    case "LastName":
                        members = members.OrderBy(s => s.LastName);
                        break;
                    case "SignUpTime_desc":
                        members = members.OrderByDescending(s => s.SignUpTime);
                        break;
                    case "SignUpTime":
                        members = members.OrderBy(s => s.SignUpTime);
                        break;
                    case "Email_desc":
                        members = members.OrderByDescending(s => s.Email);
                        break;
                    case "Email":
                        members = members.OrderBy(s => s.Email);
                        break;
                    case "PostCode_desc":
                        members = members.OrderByDescending(s => s.PostCode);
                        break;
                    case "PostCode":
                        members = members.OrderBy(s => s.PostCode);
                        break;
                    case "City_desc":
                        members = members.OrderByDescending(s => s.City);
                        break;
                    case "City":
                        members = members.OrderBy(s => s.City);
                        break;
                    case "Country_desc":
                        members = members.OrderByDescending(s => s.Country);
                        break;
                    case "Country":
                        members = members.OrderBy(s => s.Country);
                        break;
                    case "Gender_desc":
                        members = members.OrderByDescending(s => s.Gender);
                        break;
                    case "Gender":
                        members = members.OrderBy(s => s.Gender);
                        break;
                    case "Street_desc":
                        members = members.OrderByDescending(s => s.Street);
                        break;
                    case "Street":
                        members = members.OrderBy(s => s.Street);
                        break;
                    case "StreetNumber_desc":
                        members = members.OrderByDescending(s => s.StreetNumber);
                        break;
                    case "StreetNumber":
                        members = members.OrderBy(s => s.StreetNumber);
                        break;
                    case "OfficialApartmentNumber_desc":
                        members = members.OrderByDescending(s => s.OfficialApartmentNumber);
                        break;
                    case "OfficialApartmentNumber":
                        members = members.OrderBy(s => s.OfficialApartmentNumber);
                        break;

                    default:
                        members = members.OrderBy(s => s.SSN);
                        break;
                }

                return View(members.ToList().ToPagedList(page ?? 1, 10));
            }

            // Dropdown searchfunction

            // https://www.youtube.com/watch?v=srN56uxw76s

            //SerchString for Paging


            // Ajax Action Methods

            //          var model = db.PSlots .OrderBy (e => e.TypeOfVehicle);
            //        return View(model.ToList());
        }

        // GET: PSlots/Details/5


        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "Id,SSN,FirstName,LastName,SignUpTime,Cellular,Email,Street,StreetNumber,OfficialApartmentNumber,PostCode,City,Country,Gender")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SSN,FirstName,LastName,SignUpTime,Cellular,Email,Street,StreetNumber,OfficialApartmentNumber,PostCode,City,Country,Gender")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Unregister(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Unregister")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
