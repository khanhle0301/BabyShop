using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    [Table("AdminGroups")]
    public class AdminGroup
    {
        [Key]
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string ID { get; set; }

        [MaxLength(50)]     
        public string Name { get; set; }

        public virtual IEnumerable<Admin> Admins { set; get; }
        public virtual IEnumerable<Credential> Credentials { set; get; }
    }
}
