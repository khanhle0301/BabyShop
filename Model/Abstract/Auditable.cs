using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Abstract
{
    public abstract class Auditable : IAuditable
    {

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { set; get; }

        [Display(Name = "Tạo bởi")]    
        [MaxLength(50)]
        public string CreatedBy { set; get; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedDate { set; get; }

        [Display(Name = "Cập nhật bởi")]   
        [MaxLength(50)]
        public string UpdatedBy { set; get; }

        [Display(Name = "Trạng thái")]
        public bool Status { set; get; }
    }
}