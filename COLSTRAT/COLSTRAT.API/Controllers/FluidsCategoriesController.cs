using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using COLSTRAT.Domain;
using COLSTRAT.Domain.Menu.Entity.Fluids;

namespace COLSTRAT.API.Controllers
{
    [Authorize]
    public class FluidsCategoriesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/FluidsCategories
        public IQueryable<FluidsCategory> GetFluidsCategories()
        {
            return db.FluidsCategories;
        }

        // GET: api/FluidsCategories/5
        [ResponseType(typeof(FluidsCategory))]
        public async Task<IHttpActionResult> GetFluidsCategory(int id)
        {
            FluidsCategory fluidsCategory = await db.FluidsCategories.FindAsync(id);
            if (fluidsCategory == null)
            {
                return NotFound();
            }

            return Ok(fluidsCategory);
        }

        // PUT: api/FluidsCategories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFluidsCategory(int id, FluidsCategory fluidsCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fluidsCategory.FluidsCategoryId)
            {
                return BadRequest();
            }

            db.Entry(fluidsCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FluidsCategoryExists(id))
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

        // POST: api/FluidsCategories
        [ResponseType(typeof(FluidsCategory))]
        public async Task<IHttpActionResult> PostFluidsCategory(FluidsCategory fluidsCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FluidsCategories.Add(fluidsCategory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = fluidsCategory.FluidsCategoryId }, fluidsCategory);
        }

        // DELETE: api/FluidsCategories/5
        [ResponseType(typeof(FluidsCategory))]
        public async Task<IHttpActionResult> DeleteFluidsCategory(int id)
        {
            FluidsCategory fluidsCategory = await db.FluidsCategories.FindAsync(id);
            if (fluidsCategory == null)
            {
                return NotFound();
            }

            db.FluidsCategories.Remove(fluidsCategory);
            await db.SaveChangesAsync();

            return Ok(fluidsCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FluidsCategoryExists(int id)
        {
            return db.FluidsCategories.Count(e => e.FluidsCategoryId == id) > 0;
        }
    }
}