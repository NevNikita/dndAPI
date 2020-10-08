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
    public class WorldController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/World
        public IQueryable<WorldDTO> GetWorldModels()
        {
            var world = from w in db.WorldModels
                        select new WorldDTO()
                        {
                            id = w.id,
                            container = w.container,
                            isPrivate = w.isPrivate
                        };

            return world;
        }

        // GET: api/World/5
        [ResponseType(typeof(WorldModel))]
        public async Task<IHttpActionResult> GetWorldModel(int id)
        {
            var world = await db.WorldModels.Include(w => w.id).Select(w =>
        new WorldDTO()
        {
            id = w.id,
            container = w.container,
            isPrivate = w.isPrivate
        }).SingleOrDefaultAsync(w => w.id == id);
            if (world == null)
            {
                return NotFound();
            }

            return Ok(world);
        }

        // PUT: api/World/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWorldModel(int id, WorldModel worldModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != worldModel.id)
            {
                return BadRequest();
            }

            db.Entry(worldModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorldModelExists(id))
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

        // POST: api/World
        [ResponseType(typeof(WorldModel))]
        public async Task<IHttpActionResult> PostWorldModel(WorldModel worldModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorldModels.Add(worldModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = worldModel.id }, worldModel);
        }

        // DELETE: api/World/5
        [ResponseType(typeof(WorldModel))]
        public async Task<IHttpActionResult> DeleteWorldModel(int id)
        {
            WorldModel worldModel = await db.WorldModels.FindAsync(id);
            if (worldModel == null)
            {
                return NotFound();
            }

            db.WorldModels.Remove(worldModel);
            await db.SaveChangesAsync();

            return Ok(worldModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorldModelExists(int id)
        {
            return db.WorldModels.Count(e => e.id == id) > 0;
        }
    }
}