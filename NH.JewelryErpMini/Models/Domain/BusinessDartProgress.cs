using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.Domain
{
    //view_businessDartProgress
    [Table("tb_BusinessDartProgress")]
    public class BusinessDartProgress
    {
        public long? id { get; set; }
        public string PS { get; set; }
        public string DT { get; set; }
        public string Model { get; set; }
        public string AccessoriesNo { get; set; }
        public string AccessoriesName { get; set; }
        public string Specifications { get; set; }
        public decimal? CC { get; set; }
        public decimal? EachDosage { get; set; }
        public decimal? Requirement { get; set; }
        public string Department { get; set; }
        [DisplayFormat(DataFormatString ="{0:d}",ApplyFormatInEditMode =true)]
        public DateTime? NextPreliminaryDelivery { get; set; }
        public decimal? shipQuantity { get; set; }
        public string NextDepartment { get; set; }
        public decimal? incomeQuantity { get; set; }
    }
}