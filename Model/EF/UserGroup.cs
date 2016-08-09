using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    [Table("UserGroups")]
    public class UserGroup
    {
        [Key]
        [MaxLength(20)]
        [Required(ErrorMessage = "Vui lòng nhập ID")]
        [System.Web.Mvc.Remote("CheckID", "UserGroup", ErrorMessage = "Tài khoản đã tồn tại.")]
        [Column(TypeName = "varchar")]    
        public string ID { set; get; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [MaxLength(50)]
        public string Name { set; get; }

        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }

        public virtual IEnumerable<User> Users { set; get; }
        public virtual IEnumerable<Credential> Credentials { set; get; }
    }
}
