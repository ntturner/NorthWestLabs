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

        [DisplayName("Test Description")]
        public string TestDescription { get; set; }

        public string Protocol { get; set; }

        [DisplayName("Labor Cost")]
        public decimal? WageCost { get; set; }

        [DisplayName("Materials Cost")]
        public decimal? TotalMaterialsCost { get; set; }

        [DisplayName("Overall Cost")]
        public decimal? OverallCost { get; set; }


    }
}