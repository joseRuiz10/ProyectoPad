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
    public class MarcasController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/Marcas
        public IHttpActionResult GetAllStudents()
        {
            IList<MarcaViewModel> students = null;

            using (var ctx = new Database1Entities())
            {
                students = ctx.Marca.Include("StudentAddress")
                            .Select(s => new MarcaViewModel()
                            {
                                 IdMarca = s. IdMarca,
                                Descripcion = s.Descripcion,
                               
                            }).ToList<MarcaViewModel>();
            }

            if (students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

        // GET: api/Marcas/5
        [ResponseType(typeof(Marca))]
        public IHttpActionResult GetMarca(int id)
        {
            Marca marca = db.Marca.Find(id);
            if (marca == null)
            {
                return NotFound();
            }

            return Ok(marca);
        }

        // PUT: api/Marcas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMarca(MarcaViewModel marca)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");

            using (var ctx = new Database1Entities())
            {
                var existingMarca = ctx.Marca.Where(s => s.IdMarca == marca.IdMarca).FirstOrDefault<Marca>();

                if (existingMarca != null)
                {
                    existingMarca.IdMarca = marca.IdMarca;
                    existingMarca. Descripcion = marca.Descripcion;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        // POST: api/Marcas
        [ResponseType(typeof(Marca))]
        public IHttpActionResult PostNewStudent(MarcaViewModel marca)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var db = new Database1Entities())
            {
                db.Marca.Add(new Marca()
                {
                    IdMarca = marca.IdMarca,
                    Descripcion = marca.Descripcion,
                     
                });

                db.SaveChanges();
            }

            return Ok();
        }

        // DELETE: api/Marcas/5
        [ResponseType(typeof(Marca))]
        public IHttpActionResult DeleteMarca(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var db = new Database1Entities())
            {
                var student = db.Marca
                    .Where(s => s.IdMarca == id)
                    .FirstOrDefault();

                db.Entry(student).State = System.Data.Entity.EntityState.Deleted;
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

        private bool MarcaExists(int id)
        {
            return db.Marca.Count(e => e.IdMarca == id) > 0;
        }
    }
}