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
using dndAPI.Models;

namespace dndAPI.Controllers
{
    public class CharacterController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Character
        public IQueryable<CharacterModel> GetCharacterModels()
        {
            return db.CharacterModels;
        }

        // GET: api/Character/5
        [ResponseType(typeof(CharacterModel))]
        public async Task<IHttpActionResult> GetCharacterModel(int id)
        {
            CharacterModel characterModel = await db.CharacterModels.FindAsync(id);
            if (characterModel == null)
            {
                return NotFound();
            }

            return Ok(characterModel);
        }

        // PUT: api/Character/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCharacterModel(int id, CharacterModel characterModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != characterModel.id)
            {
                return BadRequest();
            }

            db.Entry(characterModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterModelExists(id))
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

        // POST: api/Character
        [ResponseType(typeof(CharacterModel))]
        public async Task<IHttpActionResult> PostCharacterModel(CharacterModel characterModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CharacterModels.Add(characterModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = characterModel.id }, characterModel);
        }

        // DELETE: api/Character/5
        [ResponseType(typeof(CharacterModel))]
        public async Task<IHttpActionResult> DeleteCharacterModel(int id)
        {
            CharacterModel characterModel = await db.CharacterModels.FindAsync(id);
            if (characterModel == null)
            {
                return NotFound();
            }

            db.CharacterModels.Remove(characterModel);
            await db.SaveChangesAsync();

            return Ok(characterModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CharacterModelExists(int id)
        {
            return db.CharacterModels.Count(e => e.id == id) > 0;
        }
    }
}