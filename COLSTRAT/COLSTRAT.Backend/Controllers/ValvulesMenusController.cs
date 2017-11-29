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
using COLSTRAT.Domain.Menu.Entity.Fluids.Valvules;

namespace COLSTRAT.Backend.Controllers
{
    [Authorize(Users = "danieldaniyyelda@gmail.com")]
    public class ValvulesMenusController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: ValvulesMenus
        public async Task<ActionResult> Index()
        {
            var valvulesMenu = db.ValvulesMenu.Include(v => v.FluidsCategory);
            return View(await valvulesMenu.ToListAsync());
        }

        // GET: ValvulesMenus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValvulesMenu valvulesMenu = await db.ValvulesMenu.FindAsync(id);
            if (valvulesMenu == null)
            {
                return HttpNotFound();
            }
            return View(valvulesMenu);
        }

        // GET: ValvulesMenus/Create
        public ActionResult Create()
        {
            ViewBag.FluidsCategoryId = new SelectList(db.FluidsCategories, "FluidsCategoryId", "Name");
            return View();
        }

        // POST: ValvulesMenus/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ValvulesMenuId,FluidsCategoryId,Name,Description")] ValvulesMenu valvulesMenu)
        {
            if (ModelState.IsValid)
            {
                db.ValvulesMenu.Add(valvulesMenu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FluidsCategoryId = new SelectList(db.FluidsCategories, "FluidsCategoryId", "Name", valvulesMenu.FluidsCategoryId);
            return View(valvulesMenu);
        }

        // GET: ValvulesMenus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValvulesMenu valvulesMenu = await db.ValvulesMenu.FindAsync(id);
            if (valvulesMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.FluidsCategoryId = new SelectList(db.FluidsCategories, "FluidsCategoryId", "Name", valvulesMenu.FluidsCategoryId);
            return View(valvulesMenu);
        }

        // POST: ValvulesMenus/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ValvulesMenuId,FluidsCategoryId,Name,Description")] ValvulesMenu valvulesMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valvulesMenu).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FluidsCategoryId = new SelectList(db.FluidsCategories, "FluidsCategoryId", "Name", valvulesMenu.FluidsCategoryId);
            return View(valvulesMenu);
        }

        // GET: ValvulesMenus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ValvulesMenu valvulesMenu = await db.ValvulesMenu.FindAsync(id);
            if (valvulesMenu == null)
            {
                return HttpNotFound();
            }
            return View(valvulesMenu);
        }

        // POST: ValvulesMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ValvulesMenu valvulesMenu = await db.ValvulesMenu.FindAsync(id);
            db.ValvulesMenu.Remove(valvulesMenu);
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
