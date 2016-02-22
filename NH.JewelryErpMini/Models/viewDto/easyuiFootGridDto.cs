using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.viewDto
{
    public class easyuiFootGridDto<TRowDto,TFootDto>
    {
        public int? total { get; set; }
        public IList<TRowDto> rows { get; set; }
        public IList<TFootDto> footer { get; set; }
    }
}