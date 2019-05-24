using aracKiralamaWebUI.Models;
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
        public ActionResult Index()
        {
            List<aracModel> aracData = new List<aracModel>();
            aracData = GetApiData();
            return View();
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
        public async System.Threading.Tasks.Task<ActionResult> CreateAsync(FormCollection collection)
        {
            aracModel yArac = new aracModel();
            yArac.marka = collection["aracMarka"].ToString();
            yArac.model= collection["aracModel"].ToString();
            yArac.minEhliyetYasi = Convert.ToInt32(collection["minEhliyetYasi"]);
            yArac.minYasSiniri = Convert.ToInt32(collection["minEhliyetYasi"]);
            yArac.gunlukMaxKm = Convert.ToInt32(collection["gunlukMaxKmSiniri"]);
            yArac.anlikKm = Convert.ToInt32(collection["anlikKm"]);
            yArac.airbagSayisi = Convert.ToInt32(collection["airbagSayisi"]);
            yArac.bagajHacmi = Convert.ToInt32(collection["bagajHacmi"]);
            yArac.koltukSayisi = Convert.ToInt32(collection["koltukSayisi"]);
            yArac.gunlukFiyat = Convert.ToDecimal(collection["gunlukFiyat"]);
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
                    return RedirectToAction("Index");
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

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Arac/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Arac/Delete/5
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

        public List<aracModel> GetApiData()
        {

            var apiUrl = "http://localhost:49774/api/Arac";

            //Connect API
            Uri url = new Uri(apiUrl);
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            string json = client.DownloadString(url);
            //END

            //JSON Parse START
            JObject data = JObject.Parse(json);
            string a = data.Last.ToString() ;
            //data = JObject.Parse(a);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            //List<aracModel> jsonList = JsonConvert.DeserializeObject<List<aracModel>>(json);
            //END

            return null;
        }
    }
}
