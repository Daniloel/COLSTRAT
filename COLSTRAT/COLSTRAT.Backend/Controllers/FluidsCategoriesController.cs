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
using COLSTRAT.Domain.Menu.Entity.Fluids;

namespace COLSTRAT.Backend.Controllers
{
    public class FluidsCategoriesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: FluidsCategories
        public async Task<ActionResult> Index()
        {
            var fluidsCategories = db.FluidsCategories.Include(f => f.Category);
            return View(await fluidsCategories.ToListAsync());
        }

        // GET: FluidsCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FluidsCategory fluidsCategory = await db.FluidsCategories.FindAsync(id);
            if (fluidsCategory == null)
            {
                return HttpNotFound();
            }
            return View(fluidsCategory);
        }

        // GET: FluidsCategories/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: FluidsCategories/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FluidsCategoryId,CategoryId,Name,Description")] FluidsCategory fluidsCategory)
        {
            if (ModelState.IsValid)
            {
                db.FluidsCategories.Add(fluidsCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", fluidsCategory.CategoryId);
            return View(fluidsCategory);
        }

        // GET: FluidsCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FluidsCategory fluidsCategory = await db.FluidsCategories.FindAsync(id);
            if (fluidsCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", fluidsCategory.CategoryId);
            return View(fluidsCategory);
        }

        // POST: FluidsCategories/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FluidsCategoryId,CategoryId,Name,Description")] FluidsCategory fluidsCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fluidsCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", fluidsCategory.CategoryId);
            return View(fluidsCategory);
        }

        // GET: FluidsCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FluidsCategory fluidsCategory = await db.FluidsCategories.FindAsync(id);
            if (fluidsCategory == null)
            {
                return HttpNotFound();
            }
            return View(fluidsCategory);
        }

        // POST: FluidsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FluidsCategory fluidsCategory = await db.FluidsCategories.FindAsync(id);
            db.FluidsCategories.Remove(fluidsCategory);
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
