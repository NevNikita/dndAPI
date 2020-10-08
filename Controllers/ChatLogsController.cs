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
    public class ChatLogsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ChatLogs
        public IQueryable<ChatLogsModel> GetChatLogsModels()
        {
            return db.ChatLogsModels;
        }

        // GET: api/ChatLogs/5
        [ResponseType(typeof(ChatLogsModel))]
        public async Task<IHttpActionResult> GetChatLogsModel(int id)
        {
            ChatLogsModel chatLogsModel = await db.ChatLogsModels.FindAsync(id);
            if (chatLogsModel == null)
            {
                return NotFound();
            }

            return Ok(chatLogsModel);
        }

        // PUT: api/ChatLogs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChatLogsModel(int id, ChatLogsModel chatLogsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chatLogsModel.id)
            {
                return BadRequest();
            }

            db.Entry(chatLogsModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatLogsModelExists(id))
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

        // POST: api/ChatLogs
        [ResponseType(typeof(ChatLogsModel))]
        public async Task<IHttpActionResult> PostChatLogsModel(ChatLogsModel chatLogsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChatLogsModels.Add(chatLogsModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = chatLogsModel.id }, chatLogsModel);
        }

        // DELETE: api/ChatLogs/5
        [ResponseType(typeof(ChatLogsModel))]
        public async Task<IHttpActionResult> DeleteChatLogsModel(int id)
        {
            ChatLogsModel chatLogsModel = await db.ChatLogsModels.FindAsync(id);
            if (chatLogsModel == null)
            {
                return NotFound();
            }

            db.ChatLogsModels.Remove(chatLogsModel);
            await db.SaveChangesAsync();

            return Ok(chatLogsModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChatLogsModelExists(int id)
        {
            return db.ChatLogsModels.Count(e => e.id == id) > 0;
        }
    }
}