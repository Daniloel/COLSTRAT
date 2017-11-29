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
using COLSTRAT.Domain.Menu.Entity.Geology;

namespace COLSTRAT.API.Controllers
{
    [Authorize]
    public class GeologyCategoriesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/GeologyCategories
        public IQueryable<GeologyCategory> GetGeologyCategories()
        {
            return db.GeologyCategories;
        }

        // GET: api/GeologyCategories/5
        [ResponseType(typeof(GeologyCategory))]
        public async Task<IHttpActionResult> GetGeologyCategory(int id)
        {
            GeologyCategory geologyCategory = await db.GeologyCategories.FindAsync(id);
            if (geologyCategory == null)
            {
                return NotFound();
            }

            return Ok(geologyCategory);
        }

        // PUT: api/GeologyCategories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGeologyCategory(int id, GeologyCategory geologyCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != geologyCategory.GeologyCategoryId)
            {
                return BadRequest();
            }

            db.Entry(geologyCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeologyCategoryExists(id))
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

        // POST: api/GeologyCategories
        [ResponseType(typeof(GeologyCategory))]
        public async Task<IHttpActionResult> PostGeologyCategory(GeologyCategory geologyCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GeologyCategories.Add(geologyCategory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = geologyCategory.GeologyCategoryId }, geologyCategory);
        }

        // DELETE: api/GeologyCategories/5
        [ResponseType(typeof(GeologyCategory))]
        public async Task<IHttpActionResult> DeleteGeologyCategory(int id)
        {
            GeologyCategory geologyCategory = await db.GeologyCategories.FindAsync(id);
            if (geologyCategory == null)
            {
                return NotFound();
            }

            db.GeologyCategories.Remove(geologyCategory);
            await db.SaveChangesAsync();

            return Ok(geologyCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GeologyCategoryExists(int id)
        {
            return db.GeologyCategories.Count(e => e.GeologyCategoryId == id) > 0;
        }
    }
}