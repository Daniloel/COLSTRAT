namespace COLSTRAT.Backend.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using COLSTRAT.Backend.Models;
    using COLSTRAT.Domain;
    [Authorize(Users = "danieldaniyyelda@gmail.com")]
    public class RocksController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Rocks
        public async Task<ActionResult> Index()
        {
            var rocks = db.Rocks.Include(r => r.Category).Include(r => r.MohsScale).Include(r => r.TypeOfRock);
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
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Scale");
            ViewBag.TypeOfRockId = new SelectList(db.TypeOfRocks, "TypeOfRockId", "Name");
            return View();
        }

        // POST: Rocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Rock rock)
        {
            if (ModelState.IsValid)
            {
                db.Rocks.Add(rock);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", rock.CategoryId);
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Scale", rock.MohsScaleId);
            ViewBag.TypeOfRockId = new SelectList(db.TypeOfRocks, "TypeOfRockId", "Name", rock.TypeOfRockId);
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
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", rock.CategoryId);
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Scale", rock.MohsScaleId);
            ViewBag.TypeOfRockId = new SelectList(db.TypeOfRocks, "TypeOfRockId", "Name", rock.TypeOfRockId);
            return View(rock);
        }

        // POST: Rocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RockId,CategoryId,TypeOfRockId,Image,Name,Descripcion,Minerals_Composition,UseFor,Structure,Chemical_Composition,Mechanical_Strength,Porosity,MohsScaleId")] Rock rock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rock).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", rock.CategoryId);
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Scale", rock.MohsScaleId);
            ViewBag.TypeOfRockId = new SelectList(db.TypeOfRocks, "TypeOfRockId", "Name", rock.TypeOfRockId);
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
