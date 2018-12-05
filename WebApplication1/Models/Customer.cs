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
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required(ErrorMessage ="Please enter your first name")]
        [DisplayName("First Name")]
        public string CustomerFirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [DisplayName("Last Name")]
        public string CustomerLastName { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage ="Please enter a valid Email address")]
        public string Email { get; set; }


        public double? Balance { get; set; }
    }
}