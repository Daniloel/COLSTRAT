namespace COLSTRAT.Backend.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using COLSTRAT.Backend.Models;
    using COLSTRAT.Domain;
    [Authorize(Users = "danieldaniyyelda@gmail.com")]
    public class MohsScalesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: MohsScales
        public async Task<ActionResult> Index()
        {
            var mohsScales = db.MohsScales.Include(m => m.Category);
            return View(await mohsScales.ToListAsync());
        }

        // GET: MohsScales/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MohsScale mohsScale = await db.MohsScales.FindAsync(id);
            if (mohsScale == null)
            {
                return HttpNotFound();
            }
            return View(mohsScale);
        }

        // GET: MohsScales/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
            return View();
        }

        // POST: MohsScales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MohsScaleId,CategoryId,Scale,Mineral,Test")] MohsScale mohsScale)
        {
            if (ModelState.IsValid)
            {
                db.MohsScales.Add(mohsScale);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", mohsScale.CategoryId);
            return View(mohsScale);
        }

        // GET: MohsScales/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MohsScale mohsScale = await db.MohsScales.FindAsync(id);
            if (mohsScale == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", mohsScale.CategoryId);
            return View(mohsScale);
        }

        // POST: MohsScales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MohsScaleId,CategoryId,Scale,Mineral,Test")] MohsScale mohsScale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mohsScale).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", mohsScale.CategoryId);
            return View(mohsScale);
        }

        // GET: MohsScales/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MohsScale mohsScale = await db.MohsScales.FindAsync(id);
            if (mohsScale == null)
            {
                return HttpNotFound();
            }
            return View(mohsScale);
        }

        // POST: MohsScales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MohsScale mohsScale = await db.MohsScales.FindAsync(id);
            db.MohsScales.Remove(mohsScale);
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
