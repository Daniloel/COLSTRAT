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
using COLSTRAT.Domain.Menu.Entity.Geology.Rocks;

namespace COLSTRAT.API.Controllers
{
    [Authorize]
    public class RocksController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Rocks
        public IQueryable<Rock> GetRocks()
        {
            return db.Rocks;
        }

        // GET: api/Rocks/5
        [ResponseType(typeof(Rock))]
        public async Task<IHttpActionResult> GetRock(int id)
        {
            Rock rock = await db.Rocks.FindAsync(id);
            if (rock == null)
            {
                return NotFound();
            }

            return Ok(rock);
        }

        // PUT: api/Rocks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRock(int id, Rock rock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rock.RockId)
            {
                return BadRequest();
            }

            db.Entry(rock).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RockExists(id))
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

        // POST: api/Rocks
        [ResponseType(typeof(Rock))]
        public async Task<IHttpActionResult> PostRock(Rock rock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rocks.Add(rock);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rock.RockId }, rock);
        }

        // DELETE: api/Rocks/5
        [ResponseType(typeof(Rock))]
        public async Task<IHttpActionResult> DeleteRock(int id)
        {
            Rock rock = await db.Rocks.FindAsync(id);
            if (rock == null)
            {
                return NotFound();
            }

            db.Rocks.Remove(rock);
            await db.SaveChangesAsync();

            return Ok(rock);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RockExists(int id)
        {
            return db.Rocks.Count(e => e.RockId == id) > 0;
        }
    }
}