using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RementisApi.Models
{
    public class SensorItem
    {
        [Key]
        public int? Id { get; set; }

        public string SensorId { get; set; }

        public int CostumerId { get; set; }
        public string Value { get; set; }

        public TimeSpan Timestamp { get; set; }

        public string Active { get; set; }
    }




}
