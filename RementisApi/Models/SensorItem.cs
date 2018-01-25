using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RementisApi.Models
{
    public class SensorItem
    {
        public int Id { get; set; }

        public string SensorId { get; set; }

        public string KlantId { get; set; }
        public string Value { get; set; }

        public string Timestamp { get; set; }

        public string Active { get; set; }
    }




}
