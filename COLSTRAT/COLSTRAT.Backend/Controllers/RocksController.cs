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
    public class RocksController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Rocks
        public async Task<ActionResult> Index()
        {
            var rocks = db.Rocks.Include(r => r.MohsScale).Include(r => r.RocksMenu);
            return View(await rocks.ToListAsync());
        }

        // GET: Rocks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rock rock = await db.Rocks.FindAsync(id);
            if (rock == null)
            {
                return HttpNotFound();
            }
            return View(rock);
        }

        // GET: Rocks/Create
        public ActionResult Create()
        {
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Mineral");
            ViewBag.RocksMenuId = new SelectList(db.RocksMenu, "RocksMenuId", "Name");
            return View();
        }

        // POST: Rocks/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RockId,RocksMenuId,Image,Name,Descripcion,Minerals_Composition,UseFor,Structure,Chemical_Composition,Mechanical_Strength,Porosity,MohsScaleId")] Rock rock)
        {
            if (ModelState.IsValid)
            {
                db.Rocks.Add(rock);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Mineral", rock.MohsScaleId);
            ViewBag.RocksMenuId = new SelectList(db.RocksMenu, "RocksMenuId", "Name", rock.RocksMenuId);
            return View(rock);
        }

        // GET: Rocks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rock rock = await db.Rocks.FindAsync(id);
            if (rock == null)
            {
                return HttpNotFound();
            }
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Mineral", rock.MohsScaleId);
            ViewBag.RocksMenuId = new SelectList(db.RocksMenu, "RocksMenuId", "Name", rock.RocksMenuId);
            return View(rock);
        }

        // POST: Rocks/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RockId,RocksMenuId,Image,Name,Descripcion,Minerals_Composition,UseFor,Structure,Chemical_Composition,Mechanical_Strength,Porosity,MohsScaleId")] Rock rock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rock).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Mineral", rock.MohsScaleId);
            ViewBag.RocksMenuId = new SelectList(db.RocksMenu, "RocksMenuId", "Name", rock.RocksMenuId);
            return View(rock);
        }

        // GET: Rocks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rock rock = await db.Rocks.FindAsync(id);
            if (rock == null)
            {
                return HttpNotFound();
            }
            return View(rock);
        }

        // POST: Rocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rock rock = await db.Rocks.FindAsync(id);
            db.Rocks.Remove(rock);
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
