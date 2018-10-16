using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_EDEO.Models
{
    public class Diagnostic
    {
        public Guid DiagnosticID { get; set; }

  
        public int EstimatedAge { get; set; }

        // For image routes
     
        public string Image { set; get; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        // Fk with its corresponding navigation property
        
        public Guid MedicalRecordID { get; set; }
        public virtual MedicalRecord MedicalRecord { set; get; }
    }
}