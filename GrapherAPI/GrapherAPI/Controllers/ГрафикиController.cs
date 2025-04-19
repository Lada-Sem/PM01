using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GrapherAPI;

namespace GrapherAPI.Controllers
{
    public class ГрафикиController : ApiController
    {
        private Численные_методыEntities db = new Численные_методыEntities();

        // GET: api/Графики
        public IQueryable<Графики> GetГрафики()
        {
            return db.Графики;
        }

        // GET: api/Графики/5
        [ResponseType(typeof(Графики))]
        public IHttpActionResult GetГрафики(int id)
        {
            Графики графики = db.Графики.Find(id);
            if (графики == null)
            {
                return NotFound();
            }

            return Ok(графики);
        }

        // PUT: api/Графики/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutГрафики(int id, Графики графики)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != графики.id)
            {
                return BadRequest();
            }

            db.Entry(графики).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ГрафикиExists(id))
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

        public string ConvertImageToBase64(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageBytes);
        }

        // POST: api/Графики
        [ResponseType(typeof(Графики))]
        public IHttpActionResult PostГрафики(Графики графики)
        {
            if (графики != null && (string.IsNullOrWhiteSpace(графики.описание) || графики.описание.Length > 150))
                ModelState.AddModelError("", "");
            if (графики != null && ( графики.изображение.Length > 150))
                   ModelState.AddModelError("", "");
            if (графики != null && (!(db.Методы.ToList().FirstOrDefault(p => p.id == графики.метод_id) is Методы)))
                ModelState.AddModelError("", "");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Графики.Add(графики);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = графики.id }, графики);
        }

        // DELETE: api/Графики/5
        [ResponseType(typeof(Графики))]
        public IHttpActionResult DeleteГрафики(int id)
        {
            Графики графики = db.Графики.Find(id);
            if (графики == null)
            {
                return NotFound();
            }

            db.Графики.Remove(графики);
            db.SaveChanges();

            return Ok(графики);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ГрафикиExists(int id)
        {
            return db.Графики.Count(e => e.id == id) > 0;
        }
    }
}