using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyShop.Models
{
    public class ChangepassUser
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [MaxLength(50, ErrorMessage = "Mật khẩu không được quá 50 ký tự.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*\W).{6,50}$", ErrorMessage = "Mật khẩu phải chứa ít nhất 6 bao gồm ký tự thường, hoa, số, và ký tự đặc biệt.")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { get; set; }
    }
}