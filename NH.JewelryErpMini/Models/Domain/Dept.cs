using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.Domain
{
    [Table("tb_Dept")]
    public class Dept
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string DeptName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}