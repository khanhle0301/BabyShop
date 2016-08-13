using Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace Model.EF
{
    [Table("Customers")]
    public partial class Customer
    {
        public int ID { get; set; }

        [MaxLength(50)]
        [Display(Name= "Tài khoản")]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }
      
        [Display(Name = "Họ tên")]
        public string Name { get; set; }
     
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
     
        public string Email { get; set; }

        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
