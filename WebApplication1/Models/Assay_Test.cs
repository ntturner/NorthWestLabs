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
    [Table("Assay_Test")]
    public class Assay_Test
    {
        [ForeignKey("Assay")]
        [Key, Column(Order = 1)]
        public int AssayID { get; set; }
        public virtual Assay Assay { get; set; }

        [ForeignKey("Test")]
        [Key, Column(Order = 2)]
        public int TestID { get; set; }
        public virtual Test Test { get; set; }

        public int? TestRequired { get; set; }
    }
}