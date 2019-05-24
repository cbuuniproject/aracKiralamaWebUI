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
                    using (var result = await client.GetAsync("api/KiralamaBusiness"))
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
        }

        // GET: Kiralama/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Kiralama/Create
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync()
        {
            List<Arac> data = new List<Arac>();
            List<aracModel> aracData = new List<aracModel>();
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
                    using (var result = await client.GetAsync("api/Arac"))
                    {
                        // Check the Result
                        if (result.IsSuccessStatusCode)
                        {
                            // Take the Result as a json string
                            var value = result.Content.ReadAsStringAsync().Result;

                            // Deserialize the string with a Json Converter to ResponseContent object and fill the datagrid
                            data = JsonConvert.DeserializeObject<ResponseContent<Arac>>(value).Data.ToList();
                        }
                    }
                }
                foreach (var item in data)
                {
                    aracModel yeniArac = new aracModel
                    {
                        marka = item.marka,
                        model=item.model,
                        Id=item.aracId,
                    };
                    aracData.Add(yeniArac);
                }

                return View(aracData);
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        // POST: Kiralama/Create
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync(FormCollection collection)
        {
            Kiralama yKiralama = new Kiralama();
            yKiralama.aracId = Convert.ToInt32(collection["aracId"].ToString());
            yKiralama.geriAlisTarihi = Convert.ToDateTime(collection["alisTarihi"]);
            yKiralama.verilisTarihi = Convert.ToDateTime(collection["verisTarihi"]);
            yKiralama.musteriId = 1;
            yKiralama.sonKm = Convert.ToInt32(collection["sonKm"]);
            yKiralama.ucret= Convert.ToInt32(collection["ucret"]);
            yKiralama.verilisKm = Convert.ToInt32(collection["verilisKm"]);
            yKiralama.sirketId = 1;
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
                    var serializedProduct = JsonConvert.SerializeObject(yKiralama);
                    // Json object to System.Net.Http content type
                    var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                    // Post Request to the URI
                    var result = await client.PostAsync("api/KiralamaBusiness", content);
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
