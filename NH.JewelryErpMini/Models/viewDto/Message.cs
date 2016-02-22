using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.viewDto
{
    public class Message<TEntity>
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public TEntity model { get; set; }
    }
}