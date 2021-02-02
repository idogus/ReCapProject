using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI.Models
{
    public class CarDTO
    {
        public string Brand { get; set; }
        public string Color { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }

    }
}
