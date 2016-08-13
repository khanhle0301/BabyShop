using Model.EF;
using System;
using System.ComponentModel.DataAnnotations;

namespace BabyShop.Models
{
    public class FeedbackViewModel
    {
        public int ID { set; get; }

        [MaxLength(250, ErrorMessage = "Tên không được quá 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Name { set; get; }

        [MaxLength(250, ErrorMessage = "Email không được quá 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        public string Email { set; get; }

        [MaxLength(250, ErrorMessage = "Chủ đề không được quá 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập chủ đề")]
        public string Subject { set; get; }

        [MaxLength(500, ErrorMessage = "Nội dung không được quá 500 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }
      
        public Contact ContactDetail { set; get; }
    }
}