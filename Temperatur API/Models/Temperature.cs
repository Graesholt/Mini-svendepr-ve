using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Temperatur_API.Models
{
    public class Temperature
    {
        public int ID { get; set; }
        public float TemperatureCentigrade { get; set; }
        public DateTime Time { get; set; }
        public Room Room { get; set; }
    }
}
