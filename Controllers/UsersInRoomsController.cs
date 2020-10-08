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
using Microsoft.AspNet.Identity;

namespace dndAPI.Controllers
{
    [Authorize]
    public class UsersInRoomsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/UsersInRooms
        public IHttpActionResult GetUsersInRoomsModels()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            var userInRooms = db.UsersInRoomsModels.Where(r => r.userId == userId).Include(b => b.Room);
            List<RoomModel> rooms = new List<RoomModel>(); 
            foreach (UsersInRoomsModel room in userInRooms)
            {
                rooms.Add(room.Room);               
            }
            return Ok(rooms);
        }

        // GET: api/UsersInRooms/5
        [ResponseType(typeof(UsersInRoomsModel))]
        public async Task<IHttpActionResult> GetUsersInRoomsModel(int id)
        {
            UsersInRoomsModel usersInRoomsModel = await db.UsersInRoomsModels.FindAsync(id);
            if (usersInRoomsModel == null)
            {
                return NotFound();
            }

            return Ok(usersInRoomsModel);
        }

        // PUT: api/UsersInRooms/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUsersInRoomsModel(int id, UsersInRoomsModel usersInRoomsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usersInRoomsModel.id)
            {
                return BadRequest();
            }

            db.Entry(usersInRoomsModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersInRoomsModelExists(id))
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

        // POST: api/UsersInRooms
        [ResponseType(typeof(UsersInRoomsModel))]
        public async Task<IHttpActionResult> PostUsersInRoomsModel(UsersInRoomsModel usersInRoomsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UsersInRoomsModels.Add(usersInRoomsModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = usersInRoomsModel.id }, usersInRoomsModel);
        }

        // DELETE: api/UsersInRooms/5
        [ResponseType(typeof(UsersInRoomsModel))]
        public async Task<IHttpActionResult> DeleteUsersInRoomsModel(int id)
        {
            UsersInRoomsModel usersInRoomsModel = await db.UsersInRoomsModels.FindAsync(id);
            if (usersInRoomsModel == null)
            {
                return NotFound();
            }

            db.UsersInRoomsModels.Remove(usersInRoomsModel);
            await db.SaveChangesAsync();

            return Ok(usersInRoomsModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersInRoomsModelExists(int id)
        {
            return db.UsersInRoomsModels.Count(e => e.id == id) > 0;
        }

 
    }
}