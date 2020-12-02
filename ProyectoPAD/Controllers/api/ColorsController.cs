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
    public class ColorsController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/Colors
        public IHttpActionResult GetAllColores()
        {
            IList<ColorViewModel> colores = null;

            using (var db = new Database1Entities())
            {
                colores = db.Color 
                            .Select(s => new ColorViewModel()
                            {
                                IdColor = s.IdColor,
                                Descripcion = s.Descripcion,
                                
                            }).ToList<ColorViewModel>();
            }

            if (colores.Count == 0)
            {
                return NotFound();
            }

            return Ok(colores);
        }

        // GET: api/Colors/5
        [ResponseType(typeof(Color))]
        public IHttpActionResult GetColor(int id)
        {
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return NotFound();
            }

            return Ok(color);
        }

        // PUT: api/Colors/5
        [ResponseType(typeof(void))]
        public   IHttpActionResult Put(ColorViewModel color)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var db = new Database1Entities())
            {
                var existingColor = db.Color.Where(s => s.IdColor == color.IdColor)
                                                        .FirstOrDefault<Color>();

                if (existingColor != null)
                {
                    existingColor.IdColor = color.IdColor;
                    existingColor.Descripcion = color.Descripcion;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        // POST: api/Colors
        [ResponseType(typeof(Color))]
        public  IHttpActionResult PostNewStudent(ColorViewModel color)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var db = new Database1Entities())
            {
                db.Color.Add(new Color()
                {
                    IdColor = color.IdColor,
                    Descripcion = color.Descripcion,
                    
                });

                db.SaveChanges();
            }

            return Ok();
        }

        // DELETE: api/Colors/5
        [ResponseType(typeof(Color))]
        public IHttpActionResult DeleteColor(int id)
        {
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return NotFound();
            }

            db.Color.Remove(color);
            db.SaveChanges();

            return Ok(color);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ColorExists(int id)
        {
            return db.Color.Count(e => e.IdColor == id) > 0;
        }
    }
}