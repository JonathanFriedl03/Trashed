using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trash_Collector3.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Address { get; set; }

        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }

        [Display(Name = "Pick Up Day")]
        public DayOfWeek? PickUpDay { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "One Time Pick Up ($30 Fee Applies!)")]
        public DateTime? OneTimePickUp { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Suspension Start")]
        public DateTime StartOfSuspension { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Suspension End")]
        public DateTime EndOfSupspension { get; set; }

        [Display(Name = "Balance (Increases after each Pick Up)")]
        public double Balance { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
