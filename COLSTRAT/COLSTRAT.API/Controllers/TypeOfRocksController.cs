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
using COLSTRAT.API.Models;

namespace COLSTRAT.API.Controllers
{
    public class TypeOfRocksController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/TypeOfRocks
        public IQueryable<TypeOfRock> GetTypeOfRocks()
        {
            return db.TypeOfRocks;
        }

        // GET: api/TypeOfRocks/5
        [ResponseType(typeof(TypeOfRock))]
        public async Task<IHttpActionResult> GetTypeOfRock(int id)
        {
            TypeOfRock typeOfRock = await db.TypeOfRocks.FindAsync(id);
            if (typeOfRock == null)
            {
                return NotFound();
            }
            
            var rocksResponse = new List<RockResponse>();
            foreach (var rock in typeOfRock.Rocks)
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
            var typeRocksResponse = new TypeOfRockResponse
            {
                TypeOfRockId = typeOfRock.TypeOfRockId,
                Name = typeOfRock.Name,
                Description = typeOfRock.Description,
                Rocks = rocksResponse
            };
            
            return Ok(typeRocksResponse);
        }

        // PUT: api/TypeOfRocks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTypeOfRock(int id, TypeOfRock typeOfRock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeOfRock.TypeOfRockId)
            {
                return BadRequest();
            }

            db.Entry(typeOfRock).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeOfRockExists(id))
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

        // POST: api/TypeOfRocks
        [ResponseType(typeof(TypeOfRock))]
        public async Task<IHttpActionResult> PostTypeOfRock(TypeOfRock typeOfRock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TypeOfRocks.Add(typeOfRock);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = typeOfRock.TypeOfRockId }, typeOfRock);
        }

        // DELETE: api/TypeOfRocks/5
        [ResponseType(typeof(TypeOfRock))]
        public async Task<IHttpActionResult> DeleteTypeOfRock(int id)
        {
            TypeOfRock typeOfRock = await db.TypeOfRocks.FindAsync(id);
            if (typeOfRock == null)
            {
                return NotFound();
            }

            db.TypeOfRocks.Remove(typeOfRock);
            await db.SaveChangesAsync();

            return Ok(typeOfRock);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeOfRockExists(int id)
        {
            return db.TypeOfRocks.Count(e => e.TypeOfRockId == id) > 0;
        }
    }
}