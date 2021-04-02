using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWatchClient.Models
{
    public class StockViewModel
    {
        public int StockId { get; set; }
        public string Logo { get; set; }
        public string Listdate { get; set; }
        public string Country { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }
        public string Marketcap { get; set; }
        public string Employees { get; set; }
        public string Phone { get; set; }
        public string Ceo { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Exchange { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Hq_address { get; set; }
    }
}
