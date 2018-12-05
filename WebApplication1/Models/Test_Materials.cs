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
    [Table("Test_Materials")]
    public class Test_Materials
    {
        [ForeignKey("Test")]
        [Key, Column(Order = 1)]
        public int TestID { get; set; }
        public virtual Test Test { get; set; }

        [ForeignKey("Materials")]
        [Key, Column(Order = 2)]
        public int MaterialID { get; set; }
        public virtual Materials Materials { get; set; }

        public double? QuantityUsed { get; set; }
        public double? MaterialsCost { get; set; }
    }
}