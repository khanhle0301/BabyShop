using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyShop.Models
{
    public class OrderViewModel
    {
        public long ID { set; get; }

        [Required]
        public string Name { set; get; }

        [Required]
        public string Address { set; get; }

        [Required]
        public string Email { set; get; }

        public string Phone { set; get; }

        public string Message { set; get; }

        public bool Status { set; get; }       
    }
}