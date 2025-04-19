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
using GrapherAPI.Models;

namespace GrapherAPI.Controllers
{
    public class ТестыController : ApiController
    {
        private Численные_методыEntities db = new Численные_методыEntities();

        // GET: api/Тесты
        [ResponseType(typeof(List<Responsetest>))]
        public IHttpActionResult GetТесты()
        {
            // Загрузка тестов с включением связанных данных (название метода)
            var tests = db.Тесты.Include(t => t.Методы).ToList();

            // Преобразование тестов в Responsetest с использованием названия метода
            var responseTests = tests.ConvertAll(test => new Responsetest(test, test.Методы.название));

            return Ok(responseTests);

        }

        // GET: api/Тесты/5
        [ResponseType(typeof(Тесты))]
        public IHttpActionResult GetТесты(int id)
        {
            Тесты тесты = db.Тесты.Find(id);
            if (тесты == null)
            {
                return NotFound();
            }

            return Ok(тесты);
        }

        // PUT: api/Тесты/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutТесты(int id, Тесты тесты)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != тесты.id)
            {
                return BadRequest();
            }

            db.Entry(тесты).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ТестыExists(id))
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

        // POST: api/Тесты
        [ResponseType(typeof(Тесты))]
        public IHttpActionResult PostТесты(Тесты тесты)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Тесты.Add(тесты);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = тесты.id }, тесты);
        }

        // DELETE: api/Тесты/5
        [ResponseType(typeof(Тесты))]
        public IHttpActionResult DeleteТесты(int id)
        {
            Тесты тесты = db.Тесты.Find(id);
            if (тесты == null)
            {
                return NotFound();
            }

            db.Тесты.Remove(тесты);
            db.SaveChanges();

            return Ok(тесты);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ТестыExists(int id)
        {
            return db.Тесты.Count(e => e.id == id) > 0;
        }
    }
}