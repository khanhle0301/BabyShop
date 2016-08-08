using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.EF
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {  set; get; }

        [MaxLength(50)]
        public string CustomerName {  set; get; }

        [MaxLength(200)]
        public string CustomerAddress {  set; get; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string CustomerEmail {  set; get; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string CustomerMobile {  set; get; }

        [MaxLength(500)]
        public string CustomerMessage {  set; get; }

        public DateTime? CreatedDate {  set; get; }

        public bool Status {  set; get; }

        public virtual IEnumerable<OrderDetail> OrderDetails {  set; get; }
    }
}