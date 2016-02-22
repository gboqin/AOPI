using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.Domain
{
    [Table("tb_EveryPartWarehouse")]
    public class DeptWarehouse
    {
        public long id { get; set; }
        public long POID { get; set; }
        public string PS { get; set; }
        public string DT { get; set; }
        public string Model { get; set; }
        public string AccessoriesNo { get; set; }
        public string AccessoriesName { get; set; }
        public string Specifications { get; set; }
        public decimal? EachDosage { get; set; }
        public decimal? Requirement { get; set; }
        public decimal? DeliveryNumber { get; set; }
        public string Suppliers { get; set; }
        public decimal? DPCC { get; set; }
        public decimal? WGCC { get; set; }
        public decimal? DMCC { get; set; }
        public decimal? JGBCC { get; set; }
        public decimal? JJGACC { get; set; }
        public decimal? JJGBCC { get; set; }
        public decimal? JJGCCC { get; set; }
        public decimal? MGCC { get; set; }
        public decimal? ZP1ACC { get; set; }
        public decimal? ZP1BCC { get; set; }
        public decimal? ZP2CC { get; set; }
        public decimal? ZP3ACC { get; set; }
        public decimal? ZP3BCC { get; set; }
        public decimal? BZACC { get; set; }
        public decimal? BZBCC { get; set; }
        public decimal? SJSL { get; set; }
        public decimal? DDQS { get; set; }
    }
}