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
    public class apiBusinessMsgController : ApiController
    {
        private BusinessMsgRepository repository;
        public apiBusinessMsgController()
        {
            this.repository = new BusinessMsgRepository();
        }
        //设置apiconfig路由解决多个get
        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetLater(string rows , string page,string OrderNo, string StyleNo, string ModelNo, string BrandNo, string OrderTypeNo)
        {
            var searchDto = new BusinessSearchDto { OrderNo = OrderNo, StyleNo = StyleNo, ModelNo = ModelNo, BrandNo = BrandNo, OrderTypeNo = OrderTypeNo};
            return this.repository.GetLaterMsg(page, rows,searchDto);
        }

        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetTwoweeks(string rows, string page, string OrderNo, string StyleNo, string ModelNo, string BrandNo, string OrderTypeNo)
        {
            var searchDto = new BusinessSearchDto { OrderNo = OrderNo, StyleNo = StyleNo, ModelNo = ModelNo, BrandNo = BrandNo, OrderTypeNo = OrderTypeNo };
            return this.repository.GetTwoWeeksMsg(page, rows, searchDto);
        }

        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetMonth(string rows, string page, string OrderNo, string StyleNo, string ModelNo, string BrandNo, string OrderTypeNo)
        {
            var searchDto = new BusinessSearchDto { OrderNo = OrderNo, StyleNo = StyleNo, ModelNo = ModelNo, BrandNo = BrandNo, OrderTypeNo = OrderTypeNo };
            return this.repository.GetMonthMsg(page, rows, searchDto);
        }

        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetNextMonth(string rows, string page, string OrderNo, string StyleNo, string ModelNo, string BrandNo, string OrderTypeNo)
        {
            var searchDto = new BusinessSearchDto { OrderNo = OrderNo, StyleNo = StyleNo, ModelNo = ModelNo, BrandNo = BrandNo, OrderTypeNo = OrderTypeNo };
            return this.repository.GetNextMonthMsg(page, rows, searchDto);
        }

        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetTwoMonths(string rows, string page, string OrderNo, string StyleNo, string ModelNo, string BrandNo, string OrderTypeNo)
        {
            var searchDto = new BusinessSearchDto { OrderNo = OrderNo, StyleNo = StyleNo, ModelNo = ModelNo, BrandNo = BrandNo, OrderTypeNo = OrderTypeNo };
            return this.repository.GetTwoMonthsMsg(page, rows, searchDto);
        }

    }
}
