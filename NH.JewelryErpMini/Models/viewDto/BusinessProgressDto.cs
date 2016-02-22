using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.viewDto
{
    public class BusinessProgressDto
    {
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
        public string ononeIcomeState { get; set; }
        public string packIcomeState { get; set; }
        public string packShipState { get; set; }
    }
}