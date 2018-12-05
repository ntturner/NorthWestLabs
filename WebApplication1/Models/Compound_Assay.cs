using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Compound_Assay")]
    public class Compound_Assay
    {
        [ForeignKey("Compound")]
        [Key, Column(Order = 1)]
        public string LTNumber { get; set; }
        public virtual Compound Compound { get; set; }

        [Key, Column(Order = 2)]
        public int SequenceNumber { get; set; }

        [ForeignKey("WorkOrder")]
        public int OrderID { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        [ForeignKey("Assay")]
        public int AssayID { get; set; }
        public virtual Assay Assay { get; set; }

        public int? TestAll { get; set; }
        public double? Quantity { get; set; }
        public DateTime? DateArrived { get; set; }
        public string ReceivedBy { get; set; }
        public DateTime? DateDue { get; set; }
        public string Appearance { get; set; }
        public double? ActualWeight { get; set; }
        public double? ClientWeight { get; set; }
        public double? MolecularMass { get; set; }
        public double? MTD { get; set; }

        public int? StatusID { get; set; }
        public Status Status { get; set; }
    }
}