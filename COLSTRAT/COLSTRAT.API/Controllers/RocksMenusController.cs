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
    public class RocksMenusController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/RocksMenus
        public IQueryable<RocksMenu> GetRocksMenu()
        {
            return db.RocksMenu;
        }

        // GET: api/RocksMenus/5
        [ResponseType(typeof(RocksMenu))]
        public async Task<IHttpActionResult> GetRocksMenu(int id)
        {
            RocksMenu rocksMenu = await db.RocksMenu.FindAsync(id);
            if (rocksMenu == null)
            {
                return NotFound();
            }

            return Ok(rocksMenu);
        }

        // PUT: api/RocksMenus/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRocksMenu(int id, RocksMenu rocksMenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rocksMenu.RocksMenuId)
            {
                return BadRequest();
            }

            db.Entry(rocksMenu).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RocksMenuExists(id))
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

        // POST: api/RocksMenus
        [ResponseType(typeof(RocksMenu))]
        public async Task<IHttpActionResult> PostRocksMenu(RocksMenu rocksMenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RocksMenu.Add(rocksMenu);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rocksMenu.RocksMenuId }, rocksMenu);
        }

        // DELETE: api/RocksMenus/5
        [ResponseType(typeof(RocksMenu))]
        public async Task<IHttpActionResult> DeleteRocksMenu(int id)
        {
            RocksMenu rocksMenu = await db.RocksMenu.FindAsync(id);
            if (rocksMenu == null)
            {
                return NotFound();
            }

            db.RocksMenu.Remove(rocksMenu);
            await db.SaveChangesAsync();

            return Ok(rocksMenu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RocksMenuExists(int id)
        {
            return db.RocksMenu.Count(e => e.RocksMenuId == id) > 0;
        }
    }
}