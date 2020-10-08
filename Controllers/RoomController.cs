using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.WebPages;
using dndAPI.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace dndAPI.Controllers
{
    [Authorize]
    public class RoomController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Room
        public IQueryable<RoomModel> GetRoomModels()
        {
            return db.RoomModels;
        }

        // GET: api/Room/5
        [ResponseType(typeof(RoomModel))]
        public IHttpActionResult GetRoomModel(int id)
        {
            var roomModel = db.RoomModels.OrderBy(x => x.id).Skip((id-1)*10).Take(10);
            if (roomModel == null)
            {
                return NotFound();
            }
            return Ok(roomModel);
        }

        // PUT: api/Room/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoomModel(int id, RoomModel roomModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomModel.id)
            {
                return BadRequest();
            }

            db.Entry(roomModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomModelExists(id))
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

        // POST: api/Room
        
        [ResponseType(typeof(RoomModel))]
        public async Task<IHttpActionResult> PostRoomModel(RoomModel roomModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UsersInRoomsModel usersInRooms = new UsersInRoomsModel();
            usersInRooms.userId = RequestContext.Principal.Identity.GetUserId();
            usersInRooms.Room = roomModel;
            db.UsersInRoomsModels.Add(usersInRooms);
            db.RoomModels.Add(roomModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = roomModel.id }, roomModel);
        }

        // DELETE: api/Room/5
        [ResponseType(typeof(RoomModel))]
        public async Task<IHttpActionResult> DeleteRoomModel(int id)
        {
            RoomModel roomModel = await db.RoomModels.FindAsync(id);
            if (roomModel == null)
            {
                return NotFound();
            }

            db.RoomModels.Remove(roomModel);
            await db.SaveChangesAsync();

            return Ok(roomModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomModelExists(int id)
        {
            return db.RoomModels.Count(e => e.id == id) > 0;
        }

    }
}