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
using COLSTRAT.Backend.Helpers;

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
        public async Task<ActionResult> Create(RockView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/RocksImages";
                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder,pic);
                }

                var rock = ToRock(view);
                rock.Image = pic;
                db.Rocks.Add(rock);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Mineral", view.MohsScaleId);
            ViewBag.RocksMenuId = new SelectList(db.RocksMenu, "RocksMenuId", "Name", view.RocksMenuId);
            return View(view);
        }

        private Rock ToRock(RockView view)
        {
            return new Rock
            {
                RockId = view.RockId,
                RocksMenuId = view.RocksMenuId,
                Image = view.Image,
                Name = view.Name,
                Descripcion = view.Descripcion,
                Minerals_Composition = view.Minerals_Composition,
                UseFor = view.UseFor,
                Structure = view.Structure,
                Chemical_Composition = view.Chemical_Composition,
                Mechanical_Strength = view.Mechanical_Strength,
                Porosity = view.Porosity,
                MohsScaleId = view.MohsScaleId
             };
        }

        // GET: Rocks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rock = await db.Rocks.FindAsync(id);
            if (rock == null)
            {
                return HttpNotFound();
            }
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Mineral", rock.MohsScaleId);
            ViewBag.RocksMenuId = new SelectList(db.RocksMenu, "RocksMenuId", "Name", rock.RocksMenuId);
            var view = ToView(rock);
            return View(view);
        }

        private RockView ToView(Rock rock)
        {
            return new RockView
            {
                RockId = rock.RockId,
                RocksMenuId = rock.RocksMenuId,
                Image = rock.Image,
                Name = rock.Name,
                Descripcion = rock.Descripcion,
                Minerals_Composition = rock.Minerals_Composition,
                UseFor = rock.UseFor,
                Structure = rock.Structure,
                Chemical_Composition = rock.Chemical_Composition,
                Mechanical_Strength = rock.Mechanical_Strength,
                Porosity = rock.Porosity,
                MohsScaleId = rock.MohsScaleId
            };
        }

        // POST: Rocks/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RockView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.Image;
                var folder = "~/Content/RocksImages";
                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var rock = ToRock(view);
                rock.Image = pic;
                db.Entry(rock).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Mineral", view.MohsScaleId);
            ViewBag.RocksMenuId = new SelectList(db.RocksMenu, "RocksMenuId", "Name", view.RocksMenuId);
            return View(view);
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
