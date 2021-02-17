using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using MobileAPI.Data;
using MobileAPI.Models;

namespace MobileAPI.Controllers
{
    [Authorize]
    public class RecipesController : ApiController
    {
        private RecipesContext db = new RecipesContext();


     //GET: ByID
        [Route("api/Recipes/ByID")]
        public IQueryable<Recipes> GetRecipesByID(int RecipeID)
        {
           

            return db.Recipes.Where(recipe => recipe.ID == RecipeID);
        }

        // GET: api/Recipes
        [Route("api/Recipes/ForCurrentUser")]
        public IQueryable<Recipes> GetRecipesForCurrentUser()
        {
            string userID = User.Identity.GetUserId();

            return db.Recipes.Where(recipe => recipe.UserId == userID);
        }
        // GET: api/Recipes
        public IQueryable<Recipes> GetRecipes()
        {
            string userID = User.Identity.GetUserId();

            return db.Recipes;
        }

        // GET: api/Recipes/5
        [ResponseType(typeof(Recipes))]
        public IHttpActionResult GetRecipes(int id)
        {
            Recipes recipes = db.Recipes.Find(id);
            if (recipes == null)
            {
                return NotFound();
            }

            return Ok(recipes);
        }

        // PUT: api/Recipes/5
        // user specific edit
    
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecipes(int id, Recipes recipes)
        {
            var userID = User.Identity.GetUserId();
            recipes.UserId = userID;

            recipes.UserId = userID;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != recipes.ID)
            {
                return BadRequest();
            }

           



            db.Entry(recipes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipesExists(recipes.ID))
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

        // POST: api/Recipes
        [ResponseType(typeof(Recipes))]
        public IHttpActionResult PostRecipes(Recipes recipes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //add user ID to entry
            string userID = User.Identity.GetUserId();
            recipes.UserId = userID;

            db.Recipes.Add(recipes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = recipes.ID }, recipes);
        }

        // DELETE: api/Recipes/5
        [ResponseType(typeof(Recipes))]
        public IHttpActionResult DeleteRecipes(int id)
        {
            Recipes recipes = db.Recipes.Find(id);
            if (recipes == null)
            {
                return NotFound();
            }

            var userID = User.Identity.GetUserId();

            if (userID != recipes.UserId)
            {
                return StatusCode(HttpStatusCode.Conflict);
            }

            db.Recipes.Remove(recipes);
            db.SaveChanges();

            return Ok(recipes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecipesExists(int id)
        {
            return db.Recipes.Count(e => e.ID == id) > 0;
        }
    }
}