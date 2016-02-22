using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.Domain
{
    [Table("tb_User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [ForeignKey("Dept")]
        public int DeptId { get; set; }
        public virtual Dept Dept { get; set; }
        public bool State { get; set; }
    }
}