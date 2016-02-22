using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Helpers
{
    public class ExcelStream : System.IO.MemoryStream
    {
        protected override void Dispose(bool disposing)
        {
            if (CanDispose)
            {
                base.Dispose(disposing);
            }
        }

        public bool CanDispose { get; set; }
    }
}