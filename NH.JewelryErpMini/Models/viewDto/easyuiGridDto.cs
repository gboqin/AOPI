using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.viewDto
{
    public class easyuiGridDto<TDto>
    {
        public int? total { get; set; }
        public IList<TDto> rows { get; set; }
    }
}