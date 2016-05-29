using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWSDBBenchmark.Models.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Open { get; set; }
        public DateTime? Close { get; set; }
    }
}