using AracKiralamaApp.Domains;
using aracKiralamaWebUI.Models;
using BankAppWebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace aracKiralamaWebUI.Controllers
{
    public class AracController : Controller
    {
        // GET: Arac
        public async System.Threading.Tasks.Task<ActionResult> IndexAsync()
        {
            List<Arac> data = new List<Arac>();
            List<aracModel> modelData = new List<aracModel>();
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
                    using (var result = await client.GetAsync("api/Arac"))
                    {
                        // Check the Result
                        if (result.IsSuccessStatusCode)
                        {
                            // Take the Result as a json string
                            var value = result.Content.ReadAsStringAsync().Result;

                            // Deserialize the string with a Json Converter to ResponseContent object and fill the datagrid
                            data= JsonConvert.DeserializeObject<ResponseContent<Arac>>(value).Data.ToList();
                        }
                    }
                }
                foreach (var item in data)
                {
                    aracModel yeniArac = new aracModel
                    {
                        marka = item.marka,
                        model = item.model,
                        airbagSayisi = item.airbagSayisi,
                        anlikKm = item.anlikKm,
                        Id = item.aracId,
                        bagajHacmi = item.bagajHacmi,
                        gunlukFiyat = item.gunlukFiyat,
                        gunlukMaxKmSiniri = item.gunlukMaxKmSiniri,
                        koltukSayisi = item.koltukSayisi,
                        minEhliyetYasi = item.minEhliyetYasi,
                        minYasSiniri = item.minYasSiniri,
                        sirketId = item.sirketId,
                    };
                    modelData.Add(yeniArac);  
                }
                
                return View(modelData);
            }
            catch (Exception ex)
            {
                return View();
            }
            
        }

        // GET: Arac/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Arac/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Arac/Create
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(FormCollection collection)
        {
            Arac yArac = new Arac();
            yArac.marka = collection["aracMarka"].ToString();
            yArac.model= collection["aracModel"].ToString();
            yArac.minEhliyetYasi = Convert.ToInt32(collection["minEhliyetYasi"]);
            yArac.minYasSiniri = Convert.ToByte(collection["minEhliyetYasi"]);
            yArac.gunlukMaxKmSiniri = Convert.ToByte(collection["gunlukMaxKmSiniri"]);
            yArac.anlikKm = Convert.ToInt32(collection["anlikKm"]);
            yArac.airbagSayisi = Convert.ToByte(collection["airbagSayisi"]);
            yArac.bagajHacmi = short.Parse(collection["bagajHacmi"]);
            yArac.koltukSayisi = Convert.ToByte(collection["koltukSayisi"]);
            yArac.gunlukFiyat = Convert.ToInt32(collection["gunlukFiyat"]);
            yArac.sirketId = 1;
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
                    var result = await client.PostAsync("api/Arac", content);
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

        // GET: Arac/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Arac/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("IndexAsync");
            }
            catch
            {
                return View();
            }
        }

        // GET: Arac/Delete/5
        public ActionResult Delete(int id)
        {
            return View("IndexAsync");
        }

        // POST: Arac/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("IndexAsync");
            }
            catch
            {
                return View();
            }
        }

    }
}
