using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using COLSTRAT.Backend.Models;
using COLSTRAT.Domain;

namespace COLSTRAT.Backend.Controllers
{
    public class TypeOfRocksController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: TypeOfRocks
        public async Task<ActionResult> Index()
        {
            return View(await db.TypeOfRocks.ToListAsync());
        }

        // GET: TypeOfRocks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfRock typeOfRock = await db.TypeOfRocks.FindAsync(id);
            if (typeOfRock == null)
            {
                return HttpNotFound();
            }
            return View(typeOfRock);
        }

        // GET: TypeOfRocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeOfRocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TypeOfRockId,Name,Description")] TypeOfRock typeOfRock)
        {
            if (ModelState.IsValid)
            {
                db.TypeOfRocks.Add(typeOfRock);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(typeOfRock);
        }

        // GET: TypeOfRocks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfRock typeOfRock = await db.TypeOfRocks.FindAsync(id);
            if (typeOfRock == null)
            {
                return HttpNotFound();
            }
            return View(typeOfRock);
        }

        // POST: TypeOfRocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TypeOfRockId,Name,Description")] TypeOfRock typeOfRock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeOfRock).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(typeOfRock);
        }

        // GET: TypeOfRocks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfRock typeOfRock = await db.TypeOfRocks.FindAsync(id);
            if (typeOfRock == null)
            {
                return HttpNotFound();
            }
            return View(typeOfRock);
        }

        // POST: TypeOfRocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TypeOfRock typeOfRock = await db.TypeOfRocks.FindAsync(id);
            db.TypeOfRocks.Remove(typeOfRock);
            await db.SaveChangesAsync();
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
