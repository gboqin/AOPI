using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.Domain
{
    //view_businessOrdersProgress视图太慢，还是食资料到新的资料库
    [Table("tb_BusinessOrdersProgress")]
    public class BusinessProgress
    {
        [Key]
        public long Id { get; set; }
        public string PS { get; set; }
        public string DT { get; set; }
        public string Model { get; set; }
        public string Material { get; set; }
        public string ProductName { get; set; }
        public string Lengths { get; set; }
        public decimal? Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Delivery { get; set; }
        public string PersonGuest { get; set; }
        public string OrderType { get; set; }
        public string Brand { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PeopleDelivery { get; set; }
        public string PeopleStyle { get; set; }
        public int? ShipType { get; set; }
        public int ononeIcomeState { get; set; }
        public int packIcomeState { get; set; }
        public int packShipState { get; set; }
    }
}