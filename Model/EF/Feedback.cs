using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.EF
{
    [Table("Feedbacks")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {  set; get; }

        [MaxLength(50)]
        [Display(Name = "Tên")]
        public string Name {  set; get; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Email {  set; get; }

        [MaxLength(100)]
        [Display(Name = "Chủ đề")]
        public string Subject {  set; get; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Nội dung")]
        public string Message {  set; get; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreateDate {  set; get; }

        [Display(Name = "Trạng thái")]
        public bool Status {  set; get; }
    }
}