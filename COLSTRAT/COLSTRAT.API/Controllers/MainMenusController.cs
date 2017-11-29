﻿using System;
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

            return Ok(mainMenu);
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
            catch (DbUpdateConcurrencyException)
            {
                if (!MainMenuExists(id))
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

        // POST: api/MainMenus
        [ResponseType(typeof(MainMenu))]
        public async Task<IHttpActionResult> PostMainMenu(MainMenu mainMenu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MainMenu.Add(mainMenu);
            await db.SaveChangesAsync();

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
            await db.SaveChangesAsync();

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