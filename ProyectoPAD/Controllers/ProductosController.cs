using ProyectoPAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPAD.Controllers
{
    public class ProductosController : Controller
    {
        // GET: Productos
        public ActionResult Index()
        {

            IEnumerable<ProductoViewModel> productos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Productoes");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductoViewModel>>();
                    readTask.Wait();

                    productos = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    productos = Enumerable.Empty<ProductoViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(productos);
        }


        public ActionResult Create()
        {
            IList<MarcaViewModel> marcas = null;

            using (var ctx = new Database1Entities())
            {
                marcas = ctx.Marca.Include("StudentAddress")
                            .Select(s => new MarcaViewModel()
                            {
                                IdMarca = s.IdMarca,
                                Descripcion = s.Descripcion,

                            }).ToList<MarcaViewModel>();
            }

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

            IList<TalleViewModel> talles = null;

            using (var db = new Database1Entities())
            {
                talles = db.Talle
                            .Select(s => new TalleViewModel()
                            {
                                IdTalle = s.IdTalle,
                                Descripcion = s.Descripcion,

                            }).ToList<TalleViewModel>();
            }

            ViewBag.idColor = new SelectList(colores, "idColor", "color");
            ViewBag.idMarca = new SelectList(marcas, "idMarca", "Descripcion");
            ViewBag.idTalle = new SelectList(talles, "idTalle", "Descripcion");
            return View();

        }
        [HttpPost]
        public ActionResult Create(ProductoViewModel producto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/Productoes");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ProductoViewModel>("Productoes", producto);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(producto);
        }


        public ActionResult Edit(int id)
        {
            ProductoViewModel producto = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Productoes?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ProductoViewModel>();
                    readTask.Wait();

                    producto = readTask.Result;
                }
            }

            return View(producto);
        }


        [HttpPost]
        public ActionResult Edit(ProductoViewModel producto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<ProductoViewModel>("Productoes", producto);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(producto);
        }


        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Productoes/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

    }
}