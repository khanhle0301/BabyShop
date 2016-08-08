using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.EF
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Display(Name = "Tên")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Name { set; get; }

        [Display(Name = "Điện thoại")]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^\+?\d{1,3}?[- .]?\(?(?:\d{2,3})\)?[- .]?\d\d\d[- .]?\d\d\d\d$", ErrorMessage = "Vui lòng kiểm tra lại số điện thoại.")]
        public string Phone { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$", ErrorMessage = "Vui lòng kiểm tra lại địa chỉ Email.")]
        public string Email { set; get; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ Website")]
        public string Website { set; get; }

        [MaxLength(200)]
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập Lat")]
        public double Lat { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập Lng")]
        public double Lng { set; get; }

        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
    }
}