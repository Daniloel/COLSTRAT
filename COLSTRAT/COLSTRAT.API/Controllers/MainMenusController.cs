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
using COLSTRAT.Domain.Menu.Main;
using COLSTRAT.API.Models;

namespace COLSTRAT.API.Controllers
{
    [Authorize]
    public class MainMenusController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/MainMenus
        public async Task<IHttpActionResult> GetMainMenu()
        {
            var mainmenus = await db.MainMenu.ToListAsync();
            var mainmenusResponse = new List<MainMenuResponse>();

            foreach (var category in mainmenus)
            {
                var categoriesResponse = new List<CategoryResponse>();
                
                foreach (var categories in category.Category)
                {
                    categoriesResponse.Add(new CategoryResponse
                    {
                        CategoryId = categories.CategoryId,
                        Name = categories.Name,
                        Description = categories.Description
                    });
                }


                mainmenusResponse.Add(new MainMenuResponse
                {
                    MainMenuId = category.MainMenuId,
                    Description = category.Description,
                    Category = categoriesResponse
                });
            }

            return Ok(mainmenusResponse);
        }

        // GET: api/MainMenus/5
        [ResponseType(typeof(MainMenu))]
        public async Task<IHttpActionResult> GetMainMenu(int id)
        {
            MainMenu mainMenu = await db.MainMenu.FindAsync(id);
            if (mainMenu == null)
            {
                return NotFound();
            }
            var mainMenuResponse = new List<MainMenuResponse>();

            foreach (var item in mainMenu.Category)
            {
                mainMenuResponse.Add(new MainMenuResponse
                {
                    CategoryId = item.CategoryId,
                    Name = item.Name,
                    Description = item.Description
                });
            }

            return Ok(mainMenuResponse);
        }

        // PUT: api/MainMenus/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMainMenu(int id, MainMenu mainMenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mainMenu.MainMenuId)
            {
                return BadRequest();
            }

            db.Entry(mainMenu).State = EntityState.Modified;

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

        // POST: api/MainMenus
        [ResponseType(typeof(MainMenu))]
        public async Task<IHttpActionResult> PostMainMenu(MainMenu mainMenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MainMenu.Add(mainMenu);

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
            return CreatedAtRoute("DefaultApi", new { id = mainMenu.MainMenuId }, mainMenu);
        }

        // DELETE: api/MainMenus/5
        [ResponseType(typeof(MainMenu))]
        public async Task<IHttpActionResult> DeleteMainMenu(int id)
        {
            MainMenu mainMenu = await db.MainMenu.FindAsync(id);
            if (mainMenu == null)
            {
                return NotFound();
            }

            db.MainMenu.Remove(mainMenu);
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

            return Ok(mainMenu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MainMenuExists(int id)
        {
            return db.MainMenu.Count(e => e.MainMenuId == id) > 0;
        }
    }
}