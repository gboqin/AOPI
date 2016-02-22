using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.Domain
{
    //view_businessOrdersProgressMessage
    [Table("tb_BusinessMessage")]
    public class BusinessMessage
    {
        [Key]
        public int countTypeId { get; set; }
        [Required]
        [StringLength(16)]
        public string countType { get; set; }
        public int? total { get; set; }
    }
}