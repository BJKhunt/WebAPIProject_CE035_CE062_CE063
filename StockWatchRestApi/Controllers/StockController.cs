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
    public class StockController : ControllerBase
    {
        private readonly DatabaseContext db;
        public StockController(DatabaseContext context)
        {
            db = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            try
            {
                var stocks = await db.Stocks.ToListAsync();
                if (stocks == null)
                {
                    return NotFound();
                }

                return Ok(stocks);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var stock = await db.Stocks.FirstOrDefaultAsync(s => s.StockId == id);

                if (stock == null)
                {
                    return NotFound();
                }

                return Ok(stock);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddStock([FromBody]Stock model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await db.Stocks.AddAsync(model);
                    await db.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id,[FromBody]Stock model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(model).State = EntityState.Modified;
                    //await db.Stocks.Update(model);
                    await db.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName=="Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                Stock model =await db.Stocks.FirstOrDefaultAsync(s => s.StockId == id);
                if(model!=null)
                {
                    db.Stocks.Remove(model);
                    await db.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
