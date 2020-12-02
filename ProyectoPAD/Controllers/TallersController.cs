using ProyectoPAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPAD.Controllers
{
    public class TallersController : Controller
    {
        // GET: Tallers
        public ActionResult Index()
        {
            IEnumerable<TalleViewModel> students = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Talles");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TalleViewModel>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<TalleViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(students);
        }



        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
    public ActionResult Create(TalleViewModel talle)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/Talles");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<TalleViewModel>("Talles", talle);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(talle);
        }

       public ActionResult Edit(int id)
        {
           TalleViewModel talle = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Talles?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TalleViewModel>();
                    readTask.Wait();

                    talle = readTask.Result;
                }
            }

            return View(talle);
        }


        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Talles/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
    public ActionResult Edit(TalleViewModel talles)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44379/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<TalleViewModel>("Talles", talles);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(talles);
        }


    }
}