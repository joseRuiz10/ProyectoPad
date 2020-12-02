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
using ProyectoPAD;
using ProyectoPAD.Models;

namespace ProyectoPAD.Controllers.api
{
    public class TallesController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/Talles
        public IHttpActionResult GetAllTalles()
        {
            IList<TalleViewModel> students = null;

            using (var db = new Database1Entities())
            {
                students = db.Talle
                            .Select(s => new TalleViewModel()
                            {
                                IdTalle = s.IdTalle,
                                Descripcion = s.Descripcion,
                                
                            }).ToList<TalleViewModel>();
            }

            if (students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

        // GET: api/Talles/5
        [ResponseType(typeof(Talle))]
        public IHttpActionResult GetTalle(int id)
        {
            Talle talle = db.Talle.Find(id);
            if (talle == null)
            {
                return NotFound();
            }

            return Ok(talle);
        }

        // PUT: api/Talles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(TalleViewModel talle)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var db = new Database1Entities())
            {
                var existingTalle = db.Talle.Where(s => s.IdTalle == talle.IdTalle)
                                                        .FirstOrDefault<Talle>();

                if (existingTalle != null)
                {
                    existingTalle.IdTalle = talle.IdTalle;
                    existingTalle.Descripcion = talle.Descripcion;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        // POST: api/Talles
        [ResponseType(typeof(Talle))]
        public IHttpActionResult PostNewTalle(TalleViewModel talle)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var db = new Database1Entities())
            {
                db.Talle.Add(new Talle()
                {
                    IdTalle = talle.IdTalle,
                    Descripcion = talle.Descripcion,
                });

                db.SaveChanges();
            }

            return Ok();
        }
        // DELETE: api/Talles/5
        [ResponseType(typeof(Talle))]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid TALLE id");

            using (var db = new Database1Entities())
            {
                var talle = db.Talle
                    .Where(s => s.IdTalle == id)
                    .FirstOrDefault();

                db.Entry(talle).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}