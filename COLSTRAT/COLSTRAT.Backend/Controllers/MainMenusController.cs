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
using COLSTRAT.Domain.Menu.Main;

namespace COLSTRAT.Backend.Controllers
{
    [Authorize(Users = "danieldaniyyelda@gmail.com")]
    public class MainMenusController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: MainMenus
        public async Task<ActionResult> Index()
        {
            return View(await db.MainMenu.ToListAsync());
        }

        // GET: MainMenus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainMenu mainMenu = await db.MainMenu.FindAsync(id);
            if (mainMenu == null)
            {
                return HttpNotFound();
            }
            return View(mainMenu);
        }

        // GET: MainMenus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MainMenus/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MainMenu mainMenu)
        {
            if (ModelState.IsValid)
            {
                db.MainMenu.Add(mainMenu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mainMenu);
        }

        // GET: MainMenus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainMenu mainMenu = await db.MainMenu.FindAsync(id);
            if (mainMenu == null)
            {
                return HttpNotFound();
            }
            return View(mainMenu);
        }

        // POST: MainMenus/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MainMenu mainMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mainMenu).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mainMenu);
        }

        // GET: MainMenus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainMenu mainMenu = await db.MainMenu.FindAsync(id);
            if (mainMenu == null)
            {
                return HttpNotFound();
            }
            return View(mainMenu);
        }

        // POST: MainMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MainMenu mainMenu = await db.MainMenu.FindAsync(id);
            db.MainMenu.Remove(mainMenu);
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
