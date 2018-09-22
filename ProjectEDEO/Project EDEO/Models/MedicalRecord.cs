using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_EDEO.Models
{
    public class MedicalRecord
    {
        public Guid MedicalRecordID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        [Column(TypeName = "Date")]
        public DateTime BornDate { get; set; }

        public virtual ICollection<Diagnostic> Diagnostics { get; set; }

        // FK to entity owner (AKA Logged in user)
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}