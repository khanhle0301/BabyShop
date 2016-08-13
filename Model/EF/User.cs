using Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.EF
{
    [Table("Users")]
    public class User : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Display(Name = "Tài khoản")]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string UserName { set; get; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Password { set; get; }

        [Display(Name = "Nhóm người dùng")]
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string GroupID { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        [MaxLength(50, ErrorMessage = "Họ tên không được quá 50 ký tự.")]
        [Display(Name = "Họ tên")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [MaxLength(200, ErrorMessage = "Địa chỉ không được quá 200 ký tự.")]
        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [MaxLength(100, ErrorMessage = "Email không được quá 100 ký tự.")]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Vui lòng kiểm tra lại địa chỉ Email.")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [MaxLength(50, ErrorMessage = "Số điện thoại không được quá 50 ký tự.")]
        [RegularExpression(@"^\+?\d{1,3}?[- .]?\(?(?:\d{2,3})\)?[- .]?\d\d\d[- .]?\d\d\d\d$", ErrorMessage = "Vui lòng kiểm tra lại số điện thoại.")]
        [Display(Name = "Điện thoại")]
        public string Phone { set; get; }

        [ForeignKey("GroupID")]
        public virtual UserGroup UserGroups { set; get; }
    }
}