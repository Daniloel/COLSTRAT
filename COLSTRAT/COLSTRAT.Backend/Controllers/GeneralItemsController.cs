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
using COLSTRAT.Domain.Menu.Entity.Generic;
using COLSTRAT.Backend.Helpers;
using System.IO;

namespace COLSTRAT.Backend.Controllers
{
    [Authorize(Users = "danieldaniyyelda@gmail.com")]
    public class GeneralItemsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: GeneralItems
        public async Task<ActionResult> Index()
        {
            var generalItems = db.GeneralItems.Include(g => g.Category);
            return View(await generalItems.ToListAsync());
        }

        // GET: GeneralItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralItem generalItem = await db.GeneralItems.FindAsync(id);
            if (generalItem == null)
            {
                return HttpNotFound();
            }
            return View(generalItem);
        }

        // GET: GeneralItems/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: GeneralItems/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GeneralItemView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/GenericItemsImages";
                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                GeneralItem item = ToItem(view);
                item.Image = pic;
                db.GeneralItems.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", view.CategoryId);
            return View(view);
        }

        GeneralItem ToItem(GeneralItemView view)
        {
            return new GeneralItem
            {
                CategoryId = view.CategoryId,
                GeneralItemId = view.GeneralItemId,
                Image = view.Image,
                Name = view.Name,
                Description = view.Description
            };
        }

        // GET: GeneralItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralItem generalItem = await db.GeneralItems.FindAsync(id);
            if (generalItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", generalItem.CategoryId);
            var view = ToView(generalItem);
            return View(view);
        }

        GeneralItemView ToView(GeneralItem generalItem)
        {
            return new GeneralItemView
            {
                CategoryId = generalItem.CategoryId,
                GeneralItemId = generalItem.GeneralItemId,
                Image = generalItem.Image,
                Name = generalItem.Name,
                Description = generalItem.Description
            };
        }

        // POST: GeneralItems/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GeneralItemView view)
        {
            if (ModelState.IsValid)
            {
                
                var pic = view.Image;
                var folder = "~/Content/GenericItemsImages";
                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                    DeleteFromFolder(view.Image);
                }
                
                var item = ToItem(view);
                item.Image = pic;
                db.Entry(item).State = EntityState.Modified;
                
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", view.CategoryId);

            return View(view);
        }

        public void DeleteFromFolder(string img)
        {
            var pathOlder = Server.MapPath(Url.Content(img));
            FileInfo file = new FileInfo(pathOlder);
            if (file.Exists)//check file exsit or not
            {
                file.Delete();
            }
        }

        // GET: GeneralItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralItem generalItem = await db.GeneralItems.FindAsync(id);
            if (generalItem == null)
            {
                return HttpNotFound();
            }
            return View(generalItem);
        }

        // POST: GeneralItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GeneralItem generalItem = await db.GeneralItems.FindAsync(id);
            db.GeneralItems.Remove(generalItem);
            var pathOlder = Server.MapPath(Url.Content(generalItem.Image));
            FileInfo file = new FileInfo(pathOlder);
            if (file.Exists)//check file exsit or not
            {
                file.Delete();
            }
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
