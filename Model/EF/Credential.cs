using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    [Table("Credentials")]
    public class Credential
    {
        [Key]
        [Column(TypeName = "varchar", Order = 1)]
        [MaxLength(20)]
        public string AdminGroupID { set; get; }

        [Key]
        [Column(TypeName = "varchar", Order = 2)]
        [MaxLength(50)]
        public string RoleID { set; get; }

        [ForeignKey("AdminGroupID")]
        public virtual AdminGroup Post { set; get; }

        [ForeignKey("RoleID")]
        public virtual Role Role { set; get; }
    }
}
