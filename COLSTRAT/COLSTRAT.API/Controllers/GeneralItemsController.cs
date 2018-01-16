using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using COLSTRAT.API.Helpers;
using COLSTRAT.API.Models;
using COLSTRAT.Domain;
using COLSTRAT.Domain.Menu.Entity.Generic;

namespace COLSTRAT.API.Controllers
{
    [Authorize]
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
        public async Task<IHttpActionResult> PutGeneralItem(int id, GeneralItemsResponse generalItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != generalItem.GeneralItemId)
            {
                return BadRequest();
            }
            if (generalItem.ImageArray != null && generalItem.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(generalItem.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    generalItem.Image = fullPath;
                }
            }

            var item = ToGeneralItem(generalItem);
            db.Entry(item).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("Index"))
                {
                    return BadRequest("1oGVEdBYMPQ2yLGq3HnZOzYFmOtfErKHYtyLPO95mdf/BbS7b1DYbDgiMJQi/blDoVi/I1NSS9Ria3sOeX3wOaBCZGatrfNiI4rjkM3XYw8");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/GeneralItems
        [ResponseType(typeof(GeneralItem))]
        public async Task<IHttpActionResult> PostGeneralItem(GeneralItemsResponse generalItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (generalItem.ImageArray != null && generalItem.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(generalItem.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    generalItem.Image = fullPath;
                }
            }

            var item = ToGeneralItem(generalItem);
            db.GeneralItems.Add(item);
            try
            {
                await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("Index"))
                {
                    return BadRequest("1oGVEdBYMPQ2yLGq3HnZOzYFmOtfErKHYtyLPO95mdf/BbS7b1DYbDgiMJQi/blDoVi/I1NSS9Ria3sOeX3wOaBCZGatrfNiI4rjkM3XYw8");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = item.GeneralItemId }, item);
        }

        private GeneralItem ToGeneralItem(GeneralItemsResponse itemResponse)
        {
            return new GeneralItem
            {
                Category = itemResponse.Category,
                CategoryId = itemResponse.CategoryId,
                GeneralItemId = itemResponse.GeneralItemId,
                Name = itemResponse.Name,
                Description = itemResponse.Description,
                Image = itemResponse.Image
            };
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
            try
            {
                await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    return BadRequest("mdg4ymQsXUPdMYLR74DMSqdwMdppHC1yssL5+SuIvJ8B3a7Pf2PIBULCV1+0oQEXewaNRYU09w76N1tktNaPxQ==");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

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