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
using System.Web.Mvc;
using ProyectoPAD;
using ProyectoPAD.Models;

namespace ProyectoPAD.Controllers.api
{
    public class ProductoesController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/Productoes
        public IHttpActionResult GetProducto()
        {
            IList<ProductoViewModel> productos = null;

            productos = db.Producto.Include("Marca").Include("Color").Include("Talle")
                .Select(s => new ProductoViewModel()
                {
                    IdProducto = s.IdProducto,
                    Tipo = s.Tipo,
                    Codigo = s.Codigo,
                    Precio = (float)s.Precio,

                    Color = s.Color == null ? null : new ColorViewModel()
                    {
                        IdColor = s.Color.IdColor,
                        Descripcion = s.Color.Descripcion

                    },
                    Marca = s.Marca == null ? null : new MarcaViewModel()
                    {
                        IdMarca = s.Marca.IdMarca,
                        Descripcion = s.Marca.Descripcion

                    },
                    Talle = s.Talle == null ? null : new TalleViewModel()
                    {
                        IdTalle = s.Talle.IdTalle,
                        Descripcion = s.Talle.Descripcion


                    }
                }).ToList<ProductoViewModel>();


            return Ok(productos);
        }


        // GET: api/Productoes/5
        [ResponseType(typeof(Producto))]
        public IHttpActionResult GetProducto(int id)
        {
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/Productoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProducto(ProductoViewModel producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProducto = db.Producto.Where(s => s.IdProducto == producto.IdProducto)
                                                        .FirstOrDefault<Producto>();

            if (existingProducto != null)
            {
                existingProducto.IdProducto = producto.IdProducto;
                existingProducto.Tipo = producto.Tipo;

                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        // POST: api/Productoes
        [ResponseType(typeof(Producto))]
        public IHttpActionResult PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Producto.Add(new Producto()
            {
                Codigo=producto.Codigo,
                Tipo = producto.Tipo,
                Precio=producto.Precio,
                idColor=producto.Color.IdColor, 
                idMarca=producto.Marca.IdMarca,
                idTalle=producto.Talle.IdTalle
            });

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Productoes/5
        [ResponseType(typeof(Producto))]
        public IHttpActionResult DeleteProducto(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a valid student id");
            }

            Producto producto = db.Producto.Find(id);
            using (var db = new Database1Entities())
            {
                var student = db.Producto
                    .Where(s => s.IdProducto == id)
                    .FirstOrDefault();

                db.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return Ok(producto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductoExists(int id)
        {
            return db.Producto.Count(e => e.IdProducto == id) > 0;
        }
    }
}