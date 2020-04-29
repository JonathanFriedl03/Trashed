using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trash_Collector3.Models
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public Customer Customer { get; set; }
        public List<Customer> Customers { get; set; }
        [Display(Name = "Pick Up Day")]
        public List<SelectListItem> PickupDay { get; set; }
        public DayOfWeek DaySelected { get; set; }
    }
}
