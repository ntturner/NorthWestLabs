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

        [DisplayName("Due Date")]
        public DateTime DateDue { get; set; }
        public decimal? Amount { get; set; }

        [DisplayName("Early Date")]
        public DateTime EarlyDate { get; set; }

        [DisplayName("Early Discount")]
        public string EarlyDiscount { get; set; }
    }
}