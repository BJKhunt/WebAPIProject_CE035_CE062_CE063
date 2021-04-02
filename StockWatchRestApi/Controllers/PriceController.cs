using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockWatchRestApi.Models;
using StockWatchRestApi.Data;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace StockWatchRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : Controller
    {
        private readonly DatabaseContext db;
        public PriceController(DatabaseContext context)
        {
            db = context;
        }
        /*
        [HttpGet]
        public ActionResult GetPrices()
        {
            //List<Price> modelList = new List<Price>();
            //string Json = System.IO.File.ReadAllText(@"C:\Users\Brijesh\source\repos\StockWatchRestApi\StockWatchRestApi\stockprices.json");
            //modelList = JsonConvert.DeserializeObject<List<Price>>(Json);
            var prices = db.Prices.ToList();
            //db.Prices.BulkDelete(prices);
            //db.Prices.BulkInsert(modelList,options=> { options.AutoMapOutputDirection = false;options.BatchSize = 100; });
            //db.SaveChanges();
            return Ok(prices);
        }*/
        [HttpGet]
        public ActionResult GetPrice(string symbol)
        {
            if (symbol == null)
            {
                return BadRequest();
            }
            try
            {
                var price = db.Prices.Where(p => p.Symbol == symbol).ToList();
                if (price == null)
                {
                    return NotFound();
                }

                return Ok(price);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
