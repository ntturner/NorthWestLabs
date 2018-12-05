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
    [Table("Work_Order")]
    public class Work_Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public decimal? QuotedPrice { get; set; }
        public decimal? ActualPrice { get; set; }
        public string Comments { get; set; }

        public int? StatusID { get; set; }
        public virtual Status Status { get; set; }

        public byte? Approved { get; set; }
    }
}