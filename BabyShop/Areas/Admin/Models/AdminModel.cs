using System;
using System.ComponentModel.DataAnnotations;

namespace BabyShop.Areas.Admin.Models
{
    public class AdminModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tài khoản.")]
        [MaxLength(50, ErrorMessage = "Tài khoản không được quá 50 ký tự.")]
        [Display(Name = "Tài khoản")]
        [System.Web.Mvc.Remote("UserNameExists", "Admin", ErrorMessage = "Tài khoản đã tồn tại.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [MaxLength(50, ErrorMessage = "Mật khẩu không được quá 50 ký tự.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*\W).{6,50}$", ErrorMessage = "Mật khẩu phải chứa ít nhất 6 bao gồm ký tự thường, hoa, số, và ký tự đặc biệt.")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        [MaxLength(50, ErrorMessage = "Họ tên không được quá 50 ký tự.")]
        [Display(Name = "Họ tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [MaxLength(200, ErrorMessage = "Địa chỉ không được quá 200 ký tự.")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [System.Web.Mvc.Remote("EmailExists", "Admin", ErrorMessage = "Email đã tồn tại.")]
        [MaxLength(100, ErrorMessage = "Email không được quá 100 ký tự.")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Vui lòng kiểm tra lại địa chỉ Email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [MaxLength(50, ErrorMessage = "Số điện thoại không được quá 50 ký tự.")]
        [RegularExpression(@"^\+?\d{1,3}?[- .]?\(?(?:\d{2,3})\)?[- .]?\d\d\d[- .]?\d\d\d\d$", ErrorMessage = "Vui lòng kiểm tra lại số điện thoại.")]
        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public bool Status { get; set; }
    }
}