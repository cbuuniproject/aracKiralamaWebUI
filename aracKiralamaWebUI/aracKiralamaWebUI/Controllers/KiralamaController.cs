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
    public class KiralamaController : Controller
    {
        // GET: Kiralama
        public async System.Threading.Tasks.Task<ActionResult> IndexAsync()
        {
            List<Kiralama> data = new List<Kiralama>();
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
                    using (var result = await client.GetAsync("api/Kiralamalar"))
                    {
                        // Check the Result
                        if (result.IsSuccessStatusCode)
                        {
                            // Take the Result as a json string
                            var value = result.Content.ReadAsStringAsync().Result;

                            // Deserialize the string with a Json Converter to ResponseContent object and fill the datagrid
                            data = JsonConvert.DeserializeObject<ResponseContent<Kiralama>>(value).Data.ToList();
                        }
                    }
                }
                foreach (var item in data)
                {
                    kiralamaModel yeniKiralama = new kiralamaModel
                    {
                        aracId = item.aracId,
                        geriAlisTarihi= item.geriAlisTarihi,
                        musteriId = item.musteriId,
                        sonKm = item.sonKm,
                        ucret = item.ucret,
                        verilisKm = item.verilisKm,
                        verilisTarihi = item.verilisTarihi,
                        sirketId = item.sirketId,
                    };
                    modelData.Add(yeniKiralama);
                }

                return View(modelData);
            }
            catch (Exception ex)
            {
                return View();
            }

            return View();
        }

        // GET: Kiralama/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Kiralama/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kiralama/Create
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

        // GET: Kiralama/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Kiralama/Edit/5
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

        // GET: Kiralama/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kiralama/Delete/5
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
