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
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceID { get; set; }

        [ForeignKey("WorkOrder")]
        public int OrderID { get; set; }
        public virtual Work_Order WorkOrder { get; set; }

        public DateTime DateDue { get; set; }
        public double Amount { get; set; }
        public DateTime EarlyDate { get; set; }
        public string EarlyDiscount { get; set; }
    }
}