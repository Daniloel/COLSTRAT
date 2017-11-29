namespace COLSTRAT.API.Controllers
{
    using COLSTRAT.API.Models;
    using COLSTRAT.Domain;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    [Authorize]
    public class CategoriesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Categories
        public async Task<IHttpActionResult> GetCategories()
        {
            var categories = db.Categories.ToListAsync();
            
            return Ok(categories);
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> GetCategory(int id)
        {
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            switch (category.CategoryId)
            {
                case ConstBase.ROCKS_CATEGORY:
                    var rocksCategory = LoadRocks();
                    return Ok(rocksCategory);
                    break;
                case ConstBase.SCALES_CATEGORY:
                    break;
                case ConstBase.COLUMN_STRATIGRAPH_CATEGORY:
                    break;
                default:
                    break;
            }



            CategoryRockResponse LoadRocks(){

                CategoryRockResponse categoriyResponse = new CategoryRockResponse();
                if (category.TypesOfRocks != null)
                {
                    var typeRockResponseList = new List<TypeOfRockResponse>();
                    
                    foreach (var typeRock in category.TypesOfRocks)
                    {
                        var typeRockResponse = new TypeOfRockResponse();
                        var rocksResponse = new List<RockResponse>();
                        foreach (var rock in typeRock.Rocks)
                        {
                            rocksResponse.Add(new RockResponse
                            {
                                Descripcion = rock.Descripcion,
                                Image = rock.Image,
                                TypeOfRockId = rock.TypeOfRockId,
                                Name = rock.Name,
                                Minerals_Composition = rock.Descripcion,
                                UseFor = rock.UseFor,
                                Structure = rock.Structure,
                                Chemical_Composition = rock.Chemical_Composition,
                                Mechanical_Strength = rock.Mechanical_Strength,
                                Porosity = rock.Porosity,
                                MohsScaleId = rock.MohsScaleId,
                                RockId = rock.RockId
                            });
                            
                        }
                        typeRockResponseList.Add(new TypeOfRockResponse
                        {
                            TypeOfRockId = typeRock.TypeOfRockId,
                            Name = typeRock.Name,
                            Description = typeRock.Description,
                            Rocks = rocksResponse
                        });
                    }

                    categoriyResponse.CategoryId = category.CategoryId;
                    categoriyResponse.Description = category.Description;
                    categoriyResponse.TypeOfRocks = typeRockResponseList;
                }
                return categoriyResponse;
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