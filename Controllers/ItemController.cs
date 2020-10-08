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
    public class ItemController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Item
        public IQueryable<ItemModel> GetItemModels()
        {
            return db.ItemModels;
        }

        // GET: api/Item/5
        [ResponseType(typeof(ItemModel))]
        public async Task<IHttpActionResult> GetItemModel(int id)
        {
            ItemModel itemModel = await db.ItemModels.FindAsync(id);
            if (itemModel == null)
            {
                return NotFound();
            }

            return Ok(itemModel);
        }

        // PUT: api/Item/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutItemModel(int id, ItemModel itemModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemModel.id)
            {
                return BadRequest();
            }

            db.Entry(itemModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemModelExists(id))
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

        // POST: api/Item
        [ResponseType(typeof(ItemModel))]
        public async Task<IHttpActionResult> PostItemModel(ItemModel itemModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ItemModels.Add(itemModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = itemModel.id }, itemModel);
        }

        // DELETE: api/Item/5
        [ResponseType(typeof(ItemModel))]
        public async Task<IHttpActionResult> DeleteItemModel(int id)
        {
            ItemModel itemModel = await db.ItemModels.FindAsync(id);
            if (itemModel == null)
            {
                return NotFound();
            }

            db.ItemModels.Remove(itemModel);
            await db.SaveChangesAsync();

            return Ok(itemModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemModelExists(int id)
        {
            return db.ItemModels.Count(e => e.id == id) > 0;
        }
    }
}