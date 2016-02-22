using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.viewDto
{
    public enum OperationResultType
    {
        [Description("操作成功")]
        Success,
        [Description("警告")]
        Warning,
        [Description("操作引发错误")]
        Error
    }
}