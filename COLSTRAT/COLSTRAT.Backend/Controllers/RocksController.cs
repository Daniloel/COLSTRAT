namespace COLSTRAT.Backend.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using COLSTRAT.Backend.Models;
    using COLSTRAT.Domain;
    using COLSTRAT.Backend.Helpers;
    using System;
    using System.Linq;

    [Authorize(Users = "danieldaniyyelda@gmail.com")]
    public class RocksController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Rocks
        public async Task<ActionResult> Index()
        {
            var rocks = db.Rocks.Include(r => r.MohsScale).Include(r => r.TypeOfRock);
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
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Scale");
            ViewBag.TypeOfRockId = new SelectList(db.TypeOfRocks, "TypeOfRockId", "Name");
            return View();
        }

        // POST: Rocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var rock = ToRock(view);
                rock.Image = pic;
                db.Rocks.Add(rock);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                Console.WriteLine(errors);
            }
            
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Scale", view.MohsScaleId);
            ViewBag.TypeOfRockId = new SelectList(db.TypeOfRocks, "TypeOfRockId", "Name", view.TypeOfRockId);
            return View(view);
        }

        private Rock ToRock(RockView view)
        {
            return new Rock
            {
                Descripcion = view.Descripcion,
                Image = view.Image,
                TypeOfRock = view.TypeOfRock,
                TypeOfRockId = view.TypeOfRockId,
                Name = view.Name,
                Minerals_Composition = view.Minerals_Composition,
                UseFor = view.UseFor,
                Structure = view.Structure,
                Chemical_Composition = view.Chemical_Composition,
                Mechanical_Strength = view.Mechanical_Strength,
                Porosity = view.Porosity,
                MohsScale = view.MohsScale,
                MohsScaleId = view.MohsScaleId,
                RockId = view.RockId
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
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Scale", rock.MohsScaleId);
            ViewBag.TypeOfRockId = new SelectList(db.TypeOfRocks, "TypeOfRockId", "Name", rock.TypeOfRockId);
            var view = ToView(rock);
            return View(view);
        }

        private RockView ToView(Rock rock)
        {
            return new RockView
            {
                Descripcion = rock.Descripcion,
                Image = rock.Image,
                TypeOfRock = rock.TypeOfRock,
                TypeOfRockId = rock.TypeOfRockId,
                Name = rock.Name,
                Minerals_Composition = rock.Minerals_Composition,
                UseFor = rock.UseFor,
                Structure = rock.Structure,
                Chemical_Composition = rock.Chemical_Composition,
                Mechanical_Strength = rock.Mechanical_Strength,
                Porosity = rock.Porosity,
                MohsScale = rock.MohsScale,
                MohsScaleId = rock.MohsScaleId,
                RockId = rock.RockId
            };
        }

        // POST: Rocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
            
            ViewBag.MohsScaleId = new SelectList(db.MohsScales, "MohsScaleId", "Scale", view.MohsScaleId);
            ViewBag.TypeOfRockId = new SelectList(db.TypeOfRocks, "TypeOfRockId", "Name", view.TypeOfRockId);
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
