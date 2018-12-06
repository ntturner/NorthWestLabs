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
    [Table("Compound")]
    public class Compound
    {
        [Key]
        public string LTNumber { get; set; }

        [DisplayName("Compound Name")]
        public string CompoundName { get; set; }
    }
}