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
    public class ValvulesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Valvules
        public async Task<ActionResult> Index()
        {
            var valvule = db.Valvule.Include(v => v.ValvulesMenu);
            return View(await valvule.ToListAsync());
        }

        // GET: Valvules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valvule valvule = await db.Valvule.FindAsync(id);
            if (valvule == null)
            {
                return HttpNotFound();
            }
            return View(valvule);
        }

        // GET: Valvules/Create
        public ActionResult Create()
        {
            ViewBag.ValvulesMenuId = new SelectList(db.ValvulesMenu, "ValvulesMenuId", "Name");
            return View();
        }

        // POST: Valvules/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ValvuleId,ValvulesMenuId,Image,Name,Descripcion,UseFor")] Valvule valvule)
        {
            if (ModelState.IsValid)
            {
                db.Valvule.Add(valvule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ValvulesMenuId = new SelectList(db.ValvulesMenu, "ValvulesMenuId", "Name", valvule.ValvulesMenuId);
            return View(valvule);
        }

        // GET: Valvules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valvule valvule = await db.Valvule.FindAsync(id);
            if (valvule == null)
            {
                return HttpNotFound();
            }
            ViewBag.ValvulesMenuId = new SelectList(db.ValvulesMenu, "ValvulesMenuId", "Name", valvule.ValvulesMenuId);
            return View(valvule);
        }

        // POST: Valvules/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ValvuleId,ValvulesMenuId,Image,Name,Descripcion,UseFor")] Valvule valvule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valvule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ValvulesMenuId = new SelectList(db.ValvulesMenu, "ValvulesMenuId", "Name", valvule.ValvulesMenuId);
            return View(valvule);
        }

        // GET: Valvules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valvule valvule = await db.Valvule.FindAsync(id);
            if (valvule == null)
            {
                return HttpNotFound();
            }
            return View(valvule);
        }

        // POST: Valvules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Valvule valvule = await db.Valvule.FindAsync(id);
            db.Valvule.Remove(valvule);
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
