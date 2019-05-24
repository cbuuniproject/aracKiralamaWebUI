using AracKiralamaApp.Domains;
using aracKiralamaWebUI.Models;
using BankAppWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace aracKiralamaWebUI.Controllers
{
    public class giderController : Controller
    {
        // GET: gider
        public async System.Threading.Tasks.Task<ActionResult> IndexAsync()
        {
            List<Harcamalar> data = new List<Harcamalar>();
            List<HarcamaTuru> haracamaTurleri = new List<HarcamaTuru>();
            List<harcamaModel> modelData = new List<harcamaModel>();
            try
            {

                // Create a HttpClient
                using (var client = new System.Net.Http.HttpClient())
                {
                    // Setup basics
                    client.BaseAddress = new Uri("http://localhost:49774/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Get Request from the URI
                    using (var result = await client.GetAsync("api/Harcamalar"))
                    {
                        // Check the Result
                        if (result.IsSuccessStatusCode)
                        {
                            // Take the Result as a json string
                            var value = result.Content.ReadAsStringAsync().Result;

                            // Deserialize the string with a Json Converter to ResponseContent object and fill the datagrid
                            data = JsonConvert.DeserializeObject<ResponseContent<Harcamalar>>(value).Data.ToList();
                        }
                    }
                    using (var result = await client.GetAsync("api/HarcamaTuru"))
                    {
                        // Check the Result
                        if (result.IsSuccessStatusCode)
                        {
                            // Take the Result as a json string
                            var value = result.Content.ReadAsStringAsync().Result;

                            // Deserialize the string with a Json Converter to ResponseContent object and fill the datagrid
                            haracamaTurleri = JsonConvert.DeserializeObject<ResponseContent<HarcamaTuru>>(value).Data.ToList();
                        }
                    }
                }
                foreach (var item in data)
                {
                    harcamaModel yeniHarcama = new harcamaModel
                    {
                        aciklama = item.aciklama,
                        harcamaId = item.harcamaId,
                        harcamaTuruId = item.harcamaTuruId,
                        tarih = item.tarih,
                        ucret = item.ucret,
                    };
                    yeniHarcama.harcamaTuru = haracamaTurleri[item.harcamaTuruId-1].harcamaTuru;
                    modelData.Add(yeniHarcama);
                }

                return View(modelData);
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        // GET: gider/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: gider/Create
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync()
        {
            List<HarcamaTuru> haracamaTurleri = new List<HarcamaTuru>();
            List<harcamaTuruModel> modelData = new List<harcamaTuruModel>();
            try
            {

                // Create a HttpClient
                using (var client = new System.Net.Http.HttpClient())
                {
                    // Setup basics
                    client.BaseAddress = new Uri("http://localhost:49774/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Get Request from the URI
                    using (var result = await client.GetAsync("api/HarcamaTuru"))
                    {
                        // Check the Result
                        if (result.IsSuccessStatusCode)
                        {
                            // Take the Result as a json string
                            var value = result.Content.ReadAsStringAsync().Result;

                            // Deserialize the string with a Json Converter to ResponseContent object and fill the datagrid
                            haracamaTurleri = JsonConvert.DeserializeObject<ResponseContent<HarcamaTuru>>(value).Data.ToList();
                        }
                    }
                }
                foreach (var item in haracamaTurleri)
                {
                    harcamaTuruModel yeniHarcama = new harcamaTuruModel
                    {
                        harcamaTuru = item.harcamaTuru ,
                        harcamaId = item.harcamaId,
                    };
                    modelData.Add(yeniHarcama);
                }

                return View(modelData);
            }
            catch (Exception ex)
            {
                return View();
            }
            return View();
        }

        // POST: gider/Create
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync(FormCollection collection)
        {
            Harcamalar yArac = new Harcamalar();
            yArac.aciklama = collection["aciklama"].ToString();
            yArac.harcamaTuruId = Convert.ToInt32(collection["harcamaId"]);
            yArac.tarih = DateTime.Now;
            yArac.ucret= Convert.ToInt32(collection["ucret"]);
            try
            {
                // Create a HttpClient
                using (var client = new HttpClient())
                {
                    // Setup basics
                    client.BaseAddress = new Uri("http://localhost:49774/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    // Create post body object

                    // Serialize C# object to Json Object
                    var serializedProduct = JsonConvert.SerializeObject(yArac);
                    // Json object to System.Net.Http content type
                    var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                    // Post Request to the URI
                    var result = await client.PostAsync("api/Harcamalar", content);
                    // Check for result
                    return RedirectToAction("IndexAsync");
                }
            }
            catch (Exception ex)
            {
                // Inform user
                return View();
            }
        }

        // GET: gider/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: gider/Edit/5
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

        // GET: gider/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: gider/Delete/5
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
