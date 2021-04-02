using Microsoft.AspNetCore.Mvc;
using StockWatchClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Web;
using System.Text;
using Highsoft.Web.Mvc.Stocks;

namespace StockWatchClient.Controllers
{
    public class StockController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44373/api");
        HttpClient client;
        public StockController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public async Task<ActionResult> Index()
        {
            List<StockViewModel> modelList = new List<StockViewModel>();
            string requestUri = client.BaseAddress + "/stock";
            HttpResponseMessage response =await client.GetAsync(requestUri);
            if(response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<StockViewModel>>(data);
                return View(modelList);
            }
            return View("Error");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(StockViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response =await client.PostAsync(client.BaseAddress + "/stock", content);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> Edit(int id)
        {
            StockViewModel model = new StockViewModel();
            Console.WriteLine(model);
            string requestUri = client.BaseAddress + "/stock/"+id.ToString();
            HttpResponseMessage response =await client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<StockViewModel>(data);
                return View(model);
            }
            return View("Error");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(StockViewModel model)
        {
            Console.WriteLine(model);
            string data = JsonConvert.SerializeObject(model);
            string requestUri = client.BaseAddress + "/stock/" + model.StockId.ToString();
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response =await client.PutAsync(requestUri, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> Delete(int id)
        {
            string requestUri = client.BaseAddress + "/stock/" + id;
            HttpResponseMessage response =await client.DeleteAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
        public async Task<ActionResult> Details(int id)
        {
            StockViewModel model = new StockViewModel();
            string requestUri = client.BaseAddress + "/stock/" + id.ToString();
            HttpResponseMessage response =await client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<StockViewModel>(data);
                HttpClient client1 = new HttpClient();
                if( Uri.IsWellFormedUriString(model.Logo, UriKind.Absolute))
                {
                    HttpResponseMessage res = await client1.GetAsync(model.Logo);
                    bool IsTypeImage = res.ToString().Contains("Content-Type: image");
                    ViewData["isImage"] = IsTypeImage;
                }
                else
                {
                    ViewData["isImage"] = false;
                }
                ViewData["isUrl"] = Uri.IsWellFormedUriString(model.Url, UriKind.Absolute);
                return View(model);
            }
            return View("Error");
        }
        
        public async Task<ActionResult> CandlestickAndVolume(string symbol)
        {
            List<CandleStickSeriesData> appleData = new List<CandleStickSeriesData>();
            List<LineSeriesData> navigatorData = new List<LineSeriesData>();

            List<PriceViewModel> modelList = new List<PriceViewModel>();
            string requestUri = client.BaseAddress + "/price?symbol="+symbol;
            HttpResponseMessage response =await client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<PriceViewModel>>(data);
            }
            
            foreach (PriceViewModel data in modelList)
            {
                appleData.Add(new CandleStickSeriesData
                {
                    X = Convert.ToDouble(data.Date)*1000,
                    High = Convert.ToDouble(data.High),
                    Low = Convert.ToDouble(data.Low),
                    Open = Convert.ToDouble(data.Open),
                    Close = Convert.ToDouble(data.Close)
                });

                navigatorData.Add(new LineSeriesData
                {
                    X = Convert.ToDouble(data.Date)*1000,
                    Y = Convert.ToDouble(data.Close)
                });
            }

            ViewBag.AppleData = appleData.OrderBy(o => o.X).ToList();
            ViewBag.NavigatorData = navigatorData.OrderBy(o => o.X).ToList();

            return View(ViewBag);
        }
    }
}
