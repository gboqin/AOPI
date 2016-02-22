using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.viewDto
{
    public class BusinessSearchDto
    {
        public string AfterDates { get; set; }
        public string OrderNo { get; set; }
        public string StyleNo { get; set; }
        public string ModelNo { get; set; }
        public string BrandNo { get; set; }
        public string OrderTypeNo { get; set; }
        //public int? DateClassId { get; set; }
        //public DateTime? BeginDate { get; set; }
        //public DateTime? EndDate { get; set; }
        public string Finished { get; set; }
    }
}