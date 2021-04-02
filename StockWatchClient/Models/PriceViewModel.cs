using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWatchClient.Models
{
    public class PriceViewModel
    {
        public int PriceId { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Date { get; set; }
        public double Volume { get; set; }
        public string Symbol { get; set; }
    }
}
