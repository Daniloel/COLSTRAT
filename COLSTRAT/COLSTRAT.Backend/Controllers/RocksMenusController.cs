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
using COLSTRAT.Domain.Menu.Entity.Geology.Rocks;

namespace COLSTRAT.Backend.Controllers
{
    [Authorize(Users = "danieldaniyyelda@gmail.com")]
    public class RocksMenusController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: RocksMenus
        public async Task<ActionResult> Index()
        {
            var rocksMenu = db.RocksMenu.Include(r => r.GeologyCategory);
            return View(await rocksMenu.ToListAsync());
        }

        // GET: RocksMenus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RocksMenu rocksMenu = await db.RocksMenu.FindAsync(id);
            if (rocksMenu == null)
            {
                return HttpNotFound();
            }
            return View(rocksMenu);
        }

        // GET: RocksMenus/Create
        public ActionResult Create()
        {
            ViewBag.GeologyCategoryId = new SelectList(db.GeologyCategories, "GeologyCategoryId", "Name");
            return View();
        }

        // POST: RocksMenus/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RocksMenuId,GeologyCategoryId,Name,Description")] RocksMenu rocksMenu)
        {
            if (ModelState.IsValid)
            {
                db.RocksMenu.Add(rocksMenu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GeologyCategoryId = new SelectList(db.GeologyCategories, "GeologyCategoryId", "Name", rocksMenu.GeologyCategoryId);
            return View(rocksMenu);
        }

        // GET: RocksMenus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RocksMenu rocksMenu = await db.RocksMenu.FindAsync(id);
            if (rocksMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeologyCategoryId = new SelectList(db.GeologyCategories, "GeologyCategoryId", "Name", rocksMenu.GeologyCategoryId);
            return View(rocksMenu);
        }

        // POST: RocksMenus/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RocksMenuId,GeologyCategoryId,Name,Description")] RocksMenu rocksMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rocksMenu).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GeologyCategoryId = new SelectList(db.GeologyCategories, "GeologyCategoryId", "Name", rocksMenu.GeologyCategoryId);
            return View(rocksMenu);
        }

        // GET: RocksMenus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RocksMenu rocksMenu = await db.RocksMenu.FindAsync(id);
            if (rocksMenu == null)
            {
                return HttpNotFound();
            }
            return View(rocksMenu);
        }

        // POST: RocksMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RocksMenu rocksMenu = await db.RocksMenu.FindAsync(id);
            db.RocksMenu.Remove(rocksMenu);
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
