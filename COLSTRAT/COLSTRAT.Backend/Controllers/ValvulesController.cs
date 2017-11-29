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
using COLSTRAT.Backend.Helpers;

namespace COLSTRAT.Backend.Controllers
{
    [Authorize(Users = "danieldaniyyelda@gmail.com")]
    public class ValvulesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Valvules
        public async Task<ActionResult> Index()
        {
            var valvule = db.Valvule.Include(v => v.FluidsCategory);
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
            ViewBag.FluidsCategoryId = new SelectList(db.FluidsCategories, "FluidsCategoryId", "Name");
            return View();
        }

        // POST: Valvules/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ValvuleView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/ValvulesImages";
                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var valvule = ToValvule(view);

                valvule.Image = pic;
                db.Valvule.Add(valvule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FluidsCategoryId = new SelectList(db.FluidsCategories, "FluidsCategoryId", "Name", view.FluidsCategoryId);
            return View(view);
        }

        private Valvule ToValvule(ValvuleView view)
        {
            return new Valvule
            {
                ValvuleId = view.ValvuleId,
                FluidsCategoryId = view.FluidsCategoryId,
                Image = view.Image,
                Name = view.Name,
                Descripcion = view.Descripcion,
                UseFor = view.UseFor
            };
        }

        // GET: Valvules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var valvule = await db.Valvule.FindAsync(id);
            if (valvule == null)
            {
                return HttpNotFound();
            }

            ViewBag.FluidsCategoryId = new SelectList(db.FluidsCategories, "FluidsCategoryId", "Name", valvule.FluidsCategoryId);
            var view = ToView(valvule);
            return View(view);
        }

        private ValvuleView ToView(Valvule valvule)
        {
            return new ValvuleView
            {
                ValvuleId = valvule.ValvuleId,
                FluidsCategoryId = valvule.FluidsCategoryId,
                Image = valvule.Image,
                Name = valvule.Name,
                Descripcion = valvule.Descripcion,
                UseFor = valvule.UseFor
            };
        }

        // POST: Valvules/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ValvuleView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.Image;
                var folder = "~/Content/ValvulesImages";
                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var valvule = ToValvule(view);
                valvule.Image = pic;
                db.Entry(valvule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FluidsCategoryId = new SelectList(db.FluidsCategories, "FluidsCategoryId", "Name", view.FluidsCategoryId);
            return View(view);
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
