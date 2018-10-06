using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_EDEO.Models
{
    public enum Gender
    {
        male, female
    }

    public class Estimation
    {
        public Guid EstimationID { get; set; }
        public Gender Gender { get; set; }
        public string Image { set; get; }
        public float EstimatedAge { get; set; }
        public string IPAddress { set; get; }
        public DateTime DateTime { get; set; }
    }
}