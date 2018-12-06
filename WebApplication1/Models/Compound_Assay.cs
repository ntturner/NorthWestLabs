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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineID { get; set; }

        [ForeignKey("Compound")]
        public string LTNumber { get; set; }
        public virtual Compound Compound { get; set; }

        public int SequenceNumber { get; set; }

        [ForeignKey("WorkOrder")]
        public int OrderID { get; set; }
        public virtual Work_Order WorkOrder { get; set; }

        [ForeignKey("Assay")]
        public int? AssayID { get; set; }
        public virtual Assay Assay { get; set; }

        [DisplayName("Run All Tests?")]
        public byte TestAll { get; set; }

        public decimal? Quantity { get; set; }

        [DisplayName("Arrival Date")]
        public DateTime? DateArrived { get; set; }

        [DisplayName("Recieved By")]
        public string ReceivedBy { get; set; }

        [DisplayName("Due Date")]
        public DateTime? DateDue { get; set; }

        public string Appearance { get; set; }

        [DisplayName("Actual Weight")]
        public decimal? ActualWeight { get; set; }

        [DisplayName("Weight listed by Client")]
        public decimal? ClientWeight { get; set; }

        [DisplayName("Molecular Mass")]
        public decimal? MolecularMass { get; set; }

        [DisplayName("Maximum Tolerated Dose")]
        public decimal? MTD { get; set; }

        public int? StatusID { get; set; }
        public Status Status { get; set; }

        public string Results { get; set; }

        //constructor
        public Compound_Assay(int orderID, string LTnumber, int sequence, Assay assay, byte test, DateTime datedue)
        {
            OrderID = orderID;
            LTNumber = LTnumber;
            SequenceNumber = sequence;
            Assay = assay;
            TestAll = test;
            DateDue = datedue;
        }

        public Compound_Assay()
        {

        }
    }
}