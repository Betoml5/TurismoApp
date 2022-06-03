using System;
using System.Collections.Generic;
using System.Text;

namespace TurismoApp.Models
{
    public class City
    {

        public string Name { get; set; }
        public bool isFav { get; set; }

        public string Type { get; set; }

        public string Image { get; set; } = "";

        public double AvgPrice { get; set; }

        public string Description { get; set; } = "";
    }
}
