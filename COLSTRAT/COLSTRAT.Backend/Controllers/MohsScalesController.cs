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
    [Authorize(Users = "danieldaniyyelda@gmail.com")]
    public class MohsScalesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: MohsScales
        public async Task<ActionResult> Index()
        {
            return View(await db.MohsScales.ToListAsync());
        }

        // GET: MohsScales/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MohsScale mohsScale = await db.MohsScales.FindAsync(id);
            if (mohsScale == null)
            {
                return HttpNotFound();
            }
            return View(mohsScale);
        }

        // GET: MohsScales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MohsScales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MohsScaleId,Scale,Mineral,Test")] MohsScale mohsScale)
        {
            if (ModelState.IsValid)
            {
                db.MohsScales.Add(mohsScale);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mohsScale);
        }

        // GET: MohsScales/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MohsScale mohsScale = await db.MohsScales.FindAsync(id);
            if (mohsScale == null)
            {
                return HttpNotFound();
            }
            return View(mohsScale);
        }

        // POST: MohsScales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MohsScaleId,Scale,Mineral,Test")] MohsScale mohsScale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mohsScale).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mohsScale);
        }

        // GET: MohsScales/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MohsScale mohsScale = await db.MohsScales.FindAsync(id);
            if (mohsScale == null)
            {
                return HttpNotFound();
            }
            return View(mohsScale);
        }

        // POST: MohsScales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MohsScale mohsScale = await db.MohsScales.FindAsync(id);
            db.MohsScales.Remove(mohsScale);
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
