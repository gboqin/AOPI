using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.viewDto
{
    public class BusinessPartMasterFilesDto
    {
        /// <summary>
        /// 工厂號碼
        /// </summary>
        public string FtyReference { get; set; }
        /// <summary>
        /// 工厂款號
        /// </summary>
        public string FactoryStyleNumber { get; set; }
        /// <summary>
        /// 電鍍規格
        /// </summary>
        public string PlatingMethod { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 材料
        /// </summary>
        public string Material { get; set; }
        /// <summary>
        /// QRS Styles
        /// </summary>
        public string QRSStyles { get; set; }
        /// <summary>
        /// 類型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 訂單號碼
        /// </summary>
        public string PONO { get; set; }
        /// <summary>
        /// CUST SO NO
        /// </summary>
        public string CustSONO { get; set; }
        /// <summary>
        /// 人客款號
        /// </summary>
        public string FossilStyle { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string JWLSize { get; set; }
        /// <summary>
        /// 出貨國家
        /// </summary>
        public string ShipTo { get; set; }
        /// <summary>
        /// PO總訂單量
        /// </summary>
        public int? POTotalQty { get; set; }
        /// <summary>
        /// 訂單類型
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 訂單日期
        /// </summary>
        public DateTime? PODate { get; set; }
        /// <summary>
        /// SAP REQT DATE
        /// </summary>
        public DateTime? SapReqtDate { get; set; }
        /// <summary>
        /// ORG DEL DATE 原貨期
        /// </summary>
        public DateTime? OrgDelDate { get; set; }
        /// <summary>
        /// 月份
        /// </summary>
        public string Month { get; set; }
        /// <summary>
        /// 訂單數量
        /// </summary>
        public int? POQty { get; set; }
        /// <summary>
        /// Accumulated shipped qty  累計出貨數量
        /// </summary>
        public int? AccumulatedShippedQty { get; set; }
        /// <summary>
        /// OPEN QTY 欠數
        /// </summary>
        public int? OpenQty { get; set; }
        /// <summary>
        /// inspected 已Q未出
        /// </summary>
        public int? InspectedQty { get; set; }
        /// <summary>
        /// 實際欠數
        /// </summary>
        public int? RealQty { get; set; }
        /// <summary>
        /// 做货部门
        /// </summary>
        public string MakingDept { get; set; }
        /// <summary>
        /// PO Line
        /// </summary>
        public int? POLine { get; set; }
    }
}