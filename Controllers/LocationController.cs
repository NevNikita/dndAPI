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
    public class LocationController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Location
        public IQueryable<LocationModel> GetLocationModels()
        {
            return db.LocationModels;
        }

        // GET: api/Location/5
        [ResponseType(typeof(LocationModel))]
        public async Task<IHttpActionResult> GetLocationModel(int id)
        {
            LocationModel locationModel = await db.LocationModels.FindAsync(id);
            if (locationModel == null)
            {
                return NotFound();
            }

            return Ok(locationModel);
        }

        // PUT: api/Location/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLocationModel(int id, LocationModel locationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locationModel.id)
            {
                return BadRequest();
            }

            db.Entry(locationModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationModelExists(id))
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

        // POST: api/Location
        [ResponseType(typeof(LocationModel))]
        public async Task<IHttpActionResult> PostLocationModel(LocationModel locationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LocationModels.Add(locationModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = locationModel.id }, locationModel);
        }

        // DELETE: api/Location/5
        [ResponseType(typeof(LocationModel))]
        public async Task<IHttpActionResult> DeleteLocationModel(int id)
        {
            LocationModel locationModel = await db.LocationModels.FindAsync(id);
            if (locationModel == null)
            {
                return NotFound();
            }

            db.LocationModels.Remove(locationModel);
            await db.SaveChangesAsync();

            return Ok(locationModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationModelExists(int id)
        {
            return db.LocationModels.Count(e => e.id == id) > 0;
        }
    }
}