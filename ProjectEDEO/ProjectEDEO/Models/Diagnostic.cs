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

        public int ChronologicalAge { get; set; }
        public int ModelEstimatedAge { get; set; }
        public int DoctorEstimatedAge { get; set; }

        // For image routes
     
        public string Image { set; get; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        // Fk with its corresponding navigation property
        
        public Guid MedicalRecordID { get; set; }
        public virtual MedicalRecord MedicalRecord { set; get; }

        // FK to entity owner (AKA Logged in user)
        public string UserID { get; set; }
    }
}