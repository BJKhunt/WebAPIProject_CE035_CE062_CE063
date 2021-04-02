using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockWatchRestApi.Models
{
    public class Price
    {
        [Key]
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
