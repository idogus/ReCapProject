using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
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
