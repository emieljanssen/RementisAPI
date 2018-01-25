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
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Priority { get; set; }
        public string State { get; set; }
    }
}
