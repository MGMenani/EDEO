using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_EDEO.Models
{
    public class EstimatorModel
    {
        public Guid EstimatorModelID { get; set; }
        public string Name { get; set; }
        public string Directory { get; set; }
        public string PythonFile { get; set; }
        public bool Active { get; set; }
        public DateTime DateTime { get; set; }
    }
}