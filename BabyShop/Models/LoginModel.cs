using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyShop.Models
{
    public class LoginModel
    {
        [Key]        
        [Required(ErrorMessage = "Vui lòng nhập tài khoản.")]
        public string UserName { set; get; }
      
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]       
        public string Password { set; get; }

    }
}