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
using COLSTRAT.Domain.Menu.Entity.Geology;

namespace COLSTRAT.Backend.Controllers
{
    [Authorize(Users = "danieldaniyyelda@gmail.com")]
    public class GeologyCategoriesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: GeologyCategories
        public async Task<ActionResult> Index()
        {
            var geologyCategories = db.GeologyCategories.Include(g => g.Category);
            return View(await geologyCategories.ToListAsync());
        }

        // GET: GeologyCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeologyCategory geologyCategory = await db.GeologyCategories.FindAsync(id);
            if (geologyCategory == null)
            {
                return HttpNotFound();
            }
            return View(geologyCategory);
        }

        // GET: GeologyCategories/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: GeologyCategories/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GeologyCategoryId,CategoryId,Name,Description")] GeologyCategory geologyCategory)
        {
            if (ModelState.IsValid)
            {
                db.GeologyCategories.Add(geologyCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", geologyCategory.CategoryId);
            return View(geologyCategory);
        }

        // GET: GeologyCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeologyCategory geologyCategory = await db.GeologyCategories.FindAsync(id);
            if (geologyCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", geologyCategory.CategoryId);
            return View(geologyCategory);
        }

        // POST: GeologyCategories/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "GeologyCategoryId,CategoryId,Name,Description")] GeologyCategory geologyCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(geologyCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", geologyCategory.CategoryId);
            return View(geologyCategory);
        }

        // GET: GeologyCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeologyCategory geologyCategory = await db.GeologyCategories.FindAsync(id);
            if (geologyCategory == null)
            {
                return HttpNotFound();
            }
            return View(geologyCategory);
        }

        // POST: GeologyCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GeologyCategory geologyCategory = await db.GeologyCategories.FindAsync(id);
            db.GeologyCategories.Remove(geologyCategory);
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
