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
using COLSTRAT.Domain.Menu.Entity.Geology.Rocks;

namespace COLSTRAT.API.Controllers
{
    [Authorize]
    public class RocksController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Rocks
        public async Task<IHttpActionResult> GetRocks()
        {
            var rocks = await db.Rocks.ToListAsync();
            var rocksResponse = new List<GeneralItemsResponse>();
            return Ok(rocks);
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
        public async Task<IHttpActionResult> PutRock(int id, RockResponse rock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rock.RockId)
            {
                return BadRequest();
            }
            if (rock.ImageArray != null && rock.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(rock.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    rock.Image = fullPath;
                }
            }
            var item = ToRock(rock);
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

        // POST: api/Rocks
        [ResponseType(typeof(Rock))]
        public async Task<IHttpActionResult> PostRock(RockResponse rock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (rock.ImageArray != null && rock.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(rock.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = string.Format("{0}.jpg", guid);
                var folder = "~/Content/Images";
                var fullPath = string.Format("{0}/{1}", folder, file);
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    rock.Image = fullPath;
                }
            }

            var item = ToRock(rock);
            db.Rocks.Add(item);
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

            return CreatedAtRoute("DefaultApi", new { id = item.RockId }, item);
        }
        private Rock ToRock(RockResponse rock)
        {
            return new Rock
            {
                RockId = rock.RockId,
                RocksMenuId = rock.RocksMenuId,
                Image = rock.Image,
                Name = rock.Name,
                Descripcion = rock.Descripcion,
                Minerals_Composition = rock.Minerals_Composition,
                UseFor = rock.UseFor,
                Structure = rock.Structure,
                Chemical_Composition = rock.Chemical_Composition,
                Mechanical_Strength = rock.Mechanical_Strength,
                Porosity = rock.Porosity,
                MohsScaleId = rock.MohsScaleId
            };
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