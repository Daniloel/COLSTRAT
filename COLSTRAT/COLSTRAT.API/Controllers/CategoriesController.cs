namespace COLSTRAT.API.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using COLSTRAT.Domain;
    using COLSTRAT.Domain.Menu.Categories;
    using COLSTRAT.API.Models;
    [Authorize]
    public class CategoriesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Categories
        public async Task<IHttpActionResult> GetCategories()
        {
            var categories = await db.Categories.ToListAsync();
            var categoriesResponse = new List<CategoryResponse>();
            

            return Ok(categories);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> GetCategory(int id)
        {
            var category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            if (id == CategoryResponse.GEOLOGY)
            {
                var categoryResponse = new List<GeologyCategoryResponse>();

                foreach (var item in category.GeologyCategory)
                {
                    var rocksmenu = new List<RocksMenuResponse>();
                    foreach (var rocksm in item.RocksMenu)
                    {
                        var rocksResponse = new List<RockResponse>();
                        foreach (var rocks in rocksm.Rock)
                        {
                            rocksResponse.Add(new RockResponse
                            {
                                RockId = rocks.RockId,
                                Image = rocks.Image,
                                Name = rocks.Name,
                                Descripcion = rocks.Descripcion,
                                Minerals_Composition = rocks.Minerals_Composition,
                                UseFor = rocks.UseFor,
                                Structure = rocks.Structure,
                                Chemical_Composition = rocks.Chemical_Composition,
                                Mechanical_Strength = rocks.Mechanical_Strength,
                                Porosity = rocks.Porosity,
                                MohsScaleId = rocks.MohsScaleId
                            });
                        }

                        rocksmenu.Add(new RocksMenuResponse
                        {
                            RocksMenuId = rocksm.RocksMenuId,
                            Name = rocksm.Name,
                            Description = rocksm.Description,
                            Rocks = rocksResponse
                        });
                    }
                    categoryResponse.Add(new GeologyCategoryResponse
                    {
                        GeologyCategoryId = item.GeologyCategoryId,
                        Name = item.Name,
                        Description = item.Description,
                        RocksMenu = rocksmenu
                    });
                }
                return Ok(categoryResponse);

            }
            else if(id == CategoryResponse.FLUIDS)
            {
                var categoryResponse = new List<FluidsCategoryResponse>();

                foreach (var item in category.FluidsCategory)
                {
                    var valvuleResponse = new List<ValvuleResponse>();

                    foreach (var valvule in item.Valvules)
                    {

                        valvuleResponse.Add(new ValvuleResponse
                        {
                            ValvuleId = valvule.ValvuleId,
                            Image = valvule.Image,
                            Name = valvule.Name,
                            Descripcion = valvule.Descripcion,
                            UseFor = valvule.UseFor
                        });
                    }
                    
                    categoryResponse.Add(new FluidsCategoryResponse
                    {
                        FluidsCategoryId = item.FluidsCategoryId,
                        Name = item.Name,
                        Description = item.Description,
                        Valvules = valvuleResponse
                    });
                }

                return Ok(categoryResponse);

            }



            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> DeleteCategory(int id)
        {
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            await db.SaveChangesAsync();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.CategoryId == id) > 0;
        }
    }
}