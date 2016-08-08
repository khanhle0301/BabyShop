using Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace Model.EF
{
    [Table("Users")]
    public partial class User
    {
        public int ID { get; set; }

        [MaxLength(50)]
        [Display(Name= "Tài khoản")]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        [MaxLength(50, ErrorMessage = "Họ tên không được quá 50 ký tự.")]
        [Display(Name = "Họ tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [MaxLength(200, ErrorMessage = "Địa chỉ không được quá 200 ký tự.")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [Column(TypeName = "varchar")]
        [MaxLength(100, ErrorMessage = "Email không được quá 100 ký tự.")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Vui lòng kiểm tra lại địa chỉ Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [Column(TypeName = "varchar")]
        [MaxLength(50, ErrorMessage = "Số điện thoại không được quá 50 ký tự.")]
        [RegularExpression(@"^\+?\d{1,3}?[- .]?\(?(?:\d{2,3})\)?[- .]?\d\d\d[- .]?\d\d\d\d$", ErrorMessage = "Vui lòng kiểm tra lại số điện thoại.")]
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
