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
    [Table("Assay")]
    public class Assay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssayID { get; set; }

        [DisplayName("Assay Name")]
        public string AssayName { get; set; }
        public int? Duration { get; set; }
        public string Protocol { get; set; }
        public string Description { get; set; }
    }
}