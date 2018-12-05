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
    [Table("Test")]
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestID { get; set; }
        public string TestDescription { get; set; }
        public string Protocol { get; set; }
        public double? WageCost { get; set; }
        public double? TotalMaterialsCost { get; set; }
        public double? OverallCost { get; set; }


    }
}