using NH.JewelryErpMini.Models.Domain;
using NH.JewelryErpMini.Models.Repository;
using NH.JewelryErpMini.Models.viewDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NH.JewelryErpMini.Controllers.API
{
    [Authorize]
    public class apiBusinessController : ApiController
    {
        private BusinessRepository repository;
        public apiBusinessController()
        {
            this.repository = new BusinessRepository();
        }

        public easyuiGridDto<BusinessProgress> GetList(string rows, string page,string doSearch, string AfterDates, string OrderNo, string StyleNo, string ModelNo,string BrandNo,string OrderTypeNo, string Finished)
        {
            var searchDto = new BusinessSearchDto { AfterDates=AfterDates, OrderNo=OrderNo, StyleNo=StyleNo , ModelNo=ModelNo,BrandNo= BrandNo, OrderTypeNo = OrderTypeNo, Finished=Finished};
            return this.repository.GetBySearch(rows, page, doSearch, searchDto);
        }

        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetListBySearch(string rows, string page, string doSearch, string AfterDates, string OrderNo, string StyleNo, string ModelNo, string BrandNo, string OrderTypeNo, string Finished)
        {
            var searchDto = new BusinessSearchDto { AfterDates = AfterDates, OrderNo = OrderNo, StyleNo = StyleNo, ModelNo = ModelNo, BrandNo = BrandNo, OrderTypeNo = OrderTypeNo, Finished = Finished };
            return this.repository.GetListBySearch(rows, page, doSearch, searchDto);
        }

        public easyuiGridDto<BusinessDartProgress> GetSlaveList(string OrderNo, string StyleNo, string ModelNo)
        {
            return this.repository.GetSlaveList(OrderNo);
        }

        public easyuiGridDto<DeptWarehouse> GetHouseList(string OrderNo)
        {
            return this.repository.GetHouseList(OrderNo);
        }

        public easyuiReportFootGridDto<InspectedOutstandingReportDto, InspectedoutstandingReportFootDto> GetInspectedOutstandingReportData(string orderType)
        {
            if ((orderType == null) || (orderType == "")) {
                return this.repository.GetInspectedOutstandingReportData("","");
            }
            string str = orderType.Replace(",", "','");
            str = "'" + str + "'";
            return this.repository.GetInspectedOutstandingReportData(str, orderType);
        }

        public easyuiReportFootGridDto<OrderTypeReportDto, OrderTypeReportFootDto> GetAllBrandOutstandingReportData(string brands)
        {
            if ((brands == null) || (brands == "")||(brands=="0"))
            {
                return this.repository.GetAllBrandOutstandingReportData("", "");
            }
            string str = brands.Replace(",", "','");
            str = "'" + str + "'";
            return this.repository.GetAllBrandOutstandingReportData(str, brands);
        }

        public easyuiReportFootGridDto<OrderTypeReportDto, OrderTypeReportFootDto> GetTypeOutstandingReportData(string types)
        {
            if ((types == null) || (types == "") || (types == "0"))
            {
                return this.repository.GetTypeOutstandingReportData("", "");
            }
            string str = types.Replace(",", "','");
            str = "'" + str + "'";
            return this.repository.GetTypeOutstandingReportData(str, types);
        }

        public IList<ColumnDto> GetMonthColumns()
        {
            return this.repository.GetMonthColumns(this.repository.GetMarterFiles());
        }

        public IList<TitleDto> GetOrderTypes()
        {
            return this.repository.GetOrdderTypes();
        }

        public IList<TitleDto> GetBrands()
        {
            return this.repository.GetBrands();
        }

        public IList<TitleDto> GetTypes()
        {
            return this.repository.GetTypes();
        }
    }
}
