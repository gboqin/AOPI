using NH.JewelryErpMini.Models.Domain;
using NH.JewelryErpMini.Models.Initial;
using NH.JewelryErpMini.Models.viewDto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.Repository
{
    public class BusinessMsgRepository
    {
        private JewelryDbContext db;
        public BusinessMsgRepository()
        {
            this.db = new JewelryDbContext();
        }
        public IQueryable<BusinessMessage> GetList()
        {
            return this.db.BusinessMessages;
        }

        //public easyuiGridDto<BusinessProgress> GetLaterMsg(string page,string rows, BusinessSearchDto searchDto)
        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetLaterMsg(string page, string rows, BusinessSearchDto searchDto)
        {
            int _page = int.Parse(page);
            int _rows = int.Parse(rows);
            DateTime Today = DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
            IQueryable<BusinessProgress> list = this.db.BusinessProgresses.Where(b => (!string.IsNullOrEmpty(searchDto.OrderNo) ? b.PS == searchDto.OrderNo : true)
                  && (!string.IsNullOrEmpty(searchDto.StyleNo) ? b.DT == searchDto.StyleNo : true) && (!string.IsNullOrEmpty(searchDto.ModelNo) ? b.Model == searchDto.ModelNo : true)
                  && (!string.IsNullOrEmpty(searchDto.BrandNo) ? b.Brand.Contains(searchDto.BrandNo) : true)
                  && (!string.IsNullOrEmpty(searchDto.OrderTypeNo) ? b.OrderType == searchDto.OrderTypeNo : true)
                  && b.packShipState == 0 && b.PeopleDelivery < Today && b.PS != null && b.Quantity > 0).OrderBy(b=>b.Id);
            int? _total = list.Count();
            var foot = new ProgressFootDto { PS = "数量合计：", DT = list.Sum(b => b.Quantity).ToString() };
            var data = list.Skip((_page - 1) * _rows).Take(_rows).ToList();
            return new easyuiFootGridDto<BusinessProgress, ProgressFootDto> { total=_total,rows=data,footer= new List<ProgressFootDto> { foot } };
        }
        
        public List<BusinessProgressDto> GetLaterMsg(BusinessSearchDto searchDto)
        {
            DateTime Today = DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
            IQueryable<BusinessProgressDto> list = this.db.BusinessProgresses.Where(b => (!string.IsNullOrEmpty(searchDto.OrderNo) ? b.PS == searchDto.OrderNo : true)
                  && (!string.IsNullOrEmpty(searchDto.StyleNo) ? b.DT == searchDto.StyleNo : true) && (!string.IsNullOrEmpty(searchDto.ModelNo) ? b.Model == searchDto.ModelNo : true)
                  && (!string.IsNullOrEmpty(searchDto.BrandNo) ? b.Brand.Contains(searchDto.BrandNo) : true)
                  && (!string.IsNullOrEmpty(searchDto.OrderTypeNo) ? b.OrderType == searchDto.OrderTypeNo : true)
                  && b.packShipState == 0 && b.PeopleDelivery < Today && b.PS != null && b.Quantity > 0).OrderBy(b => b.Id)
                  .Select(b=>new BusinessProgressDto
                  {
                      PS = b.PS,
                      DT = b.DT,
                      Model = b.Model,
                      Material = b.Material,
                      ProductName = b.ProductName,
                      Lengths = b.Lengths,
                      Quantity = b.Quantity,
                      Delivery = b.Delivery,
                      PersonGuest = b.PersonGuest,
                      OrderType = b.OrderType,
                      Brand = b.Brand,
                      PeopleDelivery = b.PeopleDelivery,
                      PeopleStyle = b.PeopleStyle,
                      ShipType = b.ShipType,
                      ononeIcomeState =  b.ononeIcomeState == 1 ? "完成" : "未完成",
                      packIcomeState = b.packIcomeState == 1 ? "完成" : "未完成",
                      packShipState = b.packShipState == 1 ? "完成" : "未完成",
                  });
            return list.ToList();
        }

        //public easyuiGridDto<BusinessProgress> GetMsgByDate(string page, string rows,DateTime beginDate,DateTime endDate, BusinessSearchDto searchDto)
        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetMsgByDate(string page, string rows, DateTime beginDate, DateTime endDate, BusinessSearchDto searchDto)
        {
            int _page = int.Parse(page);
            int _rows = int.Parse(rows);
            IQueryable<BusinessProgress> list = this.db.BusinessProgresses.Where(b => (!string.IsNullOrEmpty(searchDto.OrderNo) ? b.PS == searchDto.OrderNo : true)
                  && (!string.IsNullOrEmpty(searchDto.StyleNo) ? b.DT == searchDto.StyleNo : true) && (!string.IsNullOrEmpty(searchDto.ModelNo) ? b.Model == searchDto.ModelNo : true)
                  && (!string.IsNullOrEmpty(searchDto.BrandNo) ? b.Brand.Contains(searchDto.BrandNo) : true)
                  && (!string.IsNullOrEmpty(searchDto.OrderTypeNo) ? b.OrderType == searchDto.OrderTypeNo : true)
                  && b.packShipState == 0
                  //&& (new int?[] { 0,-1}).Contains(System.Data.Objects.SqlClient.SqlFunctions.DateDiff("ww", (DateTime)b.PeopleDelivery, Today))
                  && b.PeopleDelivery >= beginDate && b.PeopleDelivery <= endDate
                  && b.PS != null && b.Quantity > 0
                  ).OrderBy(b => b.Id);
            int? _total = list.Count();
            var foot = new ProgressFootDto { PS = "数量合计：", DT = list.Sum(b => b.Quantity).ToString() };
            var data = list.Skip((_page - 1) * _rows).Take(_rows).ToList();
            return new easyuiFootGridDto<BusinessProgress, ProgressFootDto> { total = _total, rows = data, footer = new List<ProgressFootDto> { foot } };
        }

        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetTwoWeeksMsg(string page, string rows, BusinessSearchDto searchDto)
        {
            DateTime Today = DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
            DateTime startWeek = Today.AddDays(1 - Convert.ToInt32(Today.DayOfWeek.ToString("d")));  //本周周一
            DateTime endWeek = startWeek.AddDays(13);//下周周日            
            return GetMsgByDate(page, rows, startWeek, endWeek, searchDto);
        }

        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetMonthMsg(string page, string rows, BusinessSearchDto searchDto)
        {
            DateTime Today = DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
            DateTime startMonth = Today.AddDays(1 - Today.Day);  //本月第一天
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);//本月月末
            return GetMsgByDate(page, rows, startMonth, endMonth, searchDto);
        }

        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetNextMonthMsg(string page, string rows, BusinessSearchDto searchDto)
        {
            DateTime Today = DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
            DateTime startMonth = Today.AddDays(1 - Today.Day).AddMonths(1);  //下月第一天
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);//下月月末
            return GetMsgByDate(page, rows, startMonth, endMonth, searchDto);
        }

        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetTwoMonthsMsg(string page, string rows, BusinessSearchDto searchDto)
        {
            DateTime Today = DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
            DateTime startMonth = Today.AddDays(1 - Today.Day).AddMonths(2);  //下二月第一天
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);//下二月月末
            return GetMsgByDate(page, rows, startMonth, endMonth, searchDto);
        }
    }
}