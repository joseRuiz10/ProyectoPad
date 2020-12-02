using ProyectoPAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace ProyectoPAD.Controllers
{
    public class MarcaesController : Controller
    {
        // GET: Marcaes
        public ActionResult Index()
        {
            IEnumerable<MarcaViewModel> marcas = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Marcas");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<MarcaViewModel>>();
                    readTask.Wait();

                    marcas = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    marcas = Enumerable.Empty<MarcaViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(marcas);
        }


        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
    public ActionResult create(MarcaViewModel marca)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/Marcas");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<MarcaViewModel>("Marcas", marca);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(marca);
        }



        public ActionResult Edit(int id)
        {
            MarcaViewModel marca = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/ ");
                //HTTP GET
                var responseTask = client.GetAsync("Marcas?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<MarcaViewModel>();
                    readTask.Wait();

                    marca = readTask.Result;
                }
            }

            return View(marca);
        }

       [HttpPost]
    public ActionResult Edit(MarcaViewModel marca)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/Marcas");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<MarcaViewModel>("Marcas", marca);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(marca);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Marcas/" + id.ToString());
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