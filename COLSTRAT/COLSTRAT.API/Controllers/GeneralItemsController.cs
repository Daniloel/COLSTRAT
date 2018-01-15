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
using COLSTRAT.API.Models;
using COLSTRAT.Domain;
using COLSTRAT.Domain.Menu.Entity.Generic;

namespace COLSTRAT.API.Controllers
{
    public class GeneralItemsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/GeneralItems
        public async Task<IHttpActionResult> GetGeneralItems()
        {
            var generalitems = await db.GeneralItems.ToListAsync();
            var generalitemsResponse = new List<GeneralItemsResponse>();


            return Ok(generalitems);
        }

        // GET: api/GeneralItems/5
        [ResponseType(typeof(GeneralItem))]
        public async Task<IHttpActionResult> GetGeneralItem(int id)
        {
            GeneralItem generalItem = await db.GeneralItems.FindAsync(id);
            if (generalItem == null)
            {
                return NotFound();
            }

            return Ok(generalItem);
        }

        // PUT: api/GeneralItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGeneralItem(int id, GeneralItem generalItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != generalItem.GeneralItemId)
            {
                return BadRequest();
            }

            db.Entry(generalItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneralItemExists(id))
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

        // POST: api/GeneralItems
        [ResponseType(typeof(GeneralItem))]
        public async Task<IHttpActionResult> PostGeneralItem(GeneralItem generalItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GeneralItems.Add(generalItem);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = generalItem.GeneralItemId }, generalItem);
        }

        // DELETE: api/GeneralItems/5
        [ResponseType(typeof(GeneralItem))]
        public async Task<IHttpActionResult> DeleteGeneralItem(int id)
        {
            GeneralItem generalItem = await db.GeneralItems.FindAsync(id);
            if (generalItem == null)
            {
                return NotFound();
            }

            db.GeneralItems.Remove(generalItem);
            await db.SaveChangesAsync();

            return Ok(generalItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GeneralItemExists(int id)
        {
            return db.GeneralItems.Count(e => e.GeneralItemId == id) > 0;
        }
    }
}