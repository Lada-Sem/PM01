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
using GrapherAPI;

namespace GrapherAPI.Controllers
{
    public class Ответы_на_тестыController : ApiController
    {
        private Численные_методыEntities db = new Численные_методыEntities();

        // GET: api/Ответы_на_тесты
        public IQueryable<Ответы_на_тесты> GetОтветы_на_тесты()
        {
            return db.Ответы_на_тесты;
        }

        //http://localhost:51262/api/getОтветы_на_тест?тестid=2
        [Route("api/getОтветы_на_тест")]
        public IHttpActionResult GetОтветы_на_тест(int тестid)
        {
            var ответы = db.Ответы_на_тесты.ToList().Where(p => p.тест_id == тестid).ToList();
            return Ok(ответы);
        }

        // GET: api/Ответы_на_тесты/5
        [ResponseType(typeof(Ответы_на_тесты))]
        public IHttpActionResult GetОтветы_на_тесты(int id)
        {
            Ответы_на_тесты ответы_на_тесты = db.Ответы_на_тесты.Find(id);
            if (ответы_на_тесты == null)
            {
                return NotFound();
            }

            return Ok(ответы_на_тесты);
        }

        // PUT: api/Ответы_на_тесты/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutОтветы_на_тесты(int id, Ответы_на_тесты ответы_на_тесты)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ответы_на_тесты.id)
            {
                return BadRequest();
            }

            db.Entry(ответы_на_тесты).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Ответы_на_тестыExists(id))
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

        // POST: api/Ответы_на_тесты
        [ResponseType(typeof(Ответы_на_тесты))]
        public IHttpActionResult PostОтветы_на_тесты(Ответы_на_тесты ответы_на_тесты)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ответы_на_тесты.Add(ответы_на_тесты);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ответы_на_тесты.id }, ответы_на_тесты);
        }

        // DELETE: api/Ответы_на_тесты/5
        [ResponseType(typeof(Ответы_на_тесты))]
        public IHttpActionResult DeleteОтветы_на_тесты(int id)
        {
            Ответы_на_тесты ответы_на_тесты = db.Ответы_на_тесты.Find(id);
            if (ответы_на_тесты == null)
            {
                return NotFound();
            }

            db.Ответы_на_тесты.Remove(ответы_на_тесты);
            db.SaveChanges();

            return Ok(ответы_на_тесты);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Ответы_на_тестыExists(int id)
        {
            return db.Ответы_на_тесты.Count(e => e.id == id) > 0;
        }
    }
}