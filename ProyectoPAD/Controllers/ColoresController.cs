using ProyectoPAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPAD.Controllers
{
    public class ColoresController : Controller
    {
        // GET: Colores
        public ActionResult Index()
        {
            IEnumerable<ColorViewModel> students = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Colors");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ColorViewModel>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<ColorViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(students);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
    public ActionResult create(ColorViewModel student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/Colors");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ColorViewModel>("Colors", student);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(student);
        }



      public ActionResult Edit(int id)
        {
            ColorViewModel color = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Colors?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ColorViewModel>();
                    readTask.Wait();

                    color = readTask.Result;
                }
            }

            return View(color);
        }



        [HttpPost]
        public ActionResult Edit(ColorViewModel color)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<ColorViewModel>("Colors", color);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(color);
        }



        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Colors/" + id.ToString());
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