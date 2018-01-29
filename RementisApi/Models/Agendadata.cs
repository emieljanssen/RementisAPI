using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RementisApi.Models
{
    public class Agendadata
    {
        [Key]
        public int MessageId { get; set; }
        public string Title { get; set; }
        public int CostumerId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Priority { get; set; }
        public string State { get; set; }

    }
}
