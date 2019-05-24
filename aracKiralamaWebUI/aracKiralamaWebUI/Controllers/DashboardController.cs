using AracKiralamaApp.Domains;
using aracKiralamaWebUI.Models;
using BankAppWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace aracKiralamaWebUI.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public async System.Threading.Tasks.Task<ActionResult> IndexAsync()
        {
            decimal gelirMiktar=0, giderMiktar=0;
            List<gelirModel> gelirData = new List<gelirModel>();
            List<giderModel> giderData = new List<giderModel>();
            List<kiralamaModel> modelData = new List<kiralamaModel>();
            try
            {

                // Create a HttpClient
                using (var client = new HttpClient())
                {
                    // Setup basics
                    client.BaseAddress = new Uri("http://localhost:49774/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Get Request from the URI
                    using (var result = await client.GetAsync("api/Gelir?sirketId=1"))
                    {
                        // Check the Result
                        if (result.IsSuccessStatusCode)
                        {
                            // Take the Result as a json string
                            var value = result.Content.ReadAsStringAsync().Result;

                            // Deserialize the string with a Json Converter to ResponseContent object and fill the datagrid
                            gelirData = JsonConvert.DeserializeObject<ResponseContent<gelirModel>>(value).Data.ToList();
                        }
                    }
                    using (var result = await client.GetAsync("api/Gider"))
                    {
                        // Check the Result
                        if (result.IsSuccessStatusCode)
                        {
                            // Take the Result as a json string
                            var value = result.Content.ReadAsStringAsync().Result;

                            // Deserialize the string with a Json Converter to ResponseContent object and fill the datagrid
                            giderData = JsonConvert.DeserializeObject<ResponseContent<giderModel>>(value).Data.ToList();
                        }
                    }
                }
                foreach (var item in gelirData)
                {
                    gelirMiktar += item.gelir;
                }
                foreach (var item in giderData)
                {
                    giderMiktar += item.gider;
                }
                gelirGiderModel data = new gelirGiderModel
                {
                    gelirMiktari = gelirMiktar,
                    giderMiktari = giderMiktar,
                };
                return View(data);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
