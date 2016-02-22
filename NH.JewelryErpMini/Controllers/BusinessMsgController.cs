using NH.JewelryErpMini.Helpers;
using NH.JewelryErpMini.Models.Initial;
using NH.JewelryErpMini.Models.Repository;
using NH.JewelryErpMini.Models.viewDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NH.JewelryErpMini.Controllers
{
    [Authorize]
    public class BusinessMsgController : Controller
    {
        private BusinessMsgRepository repository;

        public BusinessMsgController()
        {
            this.repository = new BusinessMsgRepository();
        }
        // GET: BusinessMsg
        public ActionResult Index()
        {
            var result = repository.GetList().ToList();
            return View(result);
        }

        public PartialViewResult ManagerMsg()
        {
            var result = repository.GetList().FirstOrDefault();
            return PartialView(result);
        }

        public FileResult GetExportExcelUrl(int ExportTypeIndex)
        {
            //构造导出的集合
            List<BusinessProgressDto> bpList = new List<BusinessProgressDto>()
            {
                new BusinessProgressDto()
                {
                    PS="12345", DT="23456",Model="12",Material="34",ProductName="56",Lengths="78",Quantity=(decimal)10,
                    Delivery=DateTime.Now,PersonGuest ="测试数据",OrderType="adfa",Brand="1231",PeopleDelivery=DateTime.Now,
                    PeopleStyle ="sasda",ShipType=1,ononeIcomeState="完成",packIcomeState="完成",packShipState="完成"
                },
                new BusinessProgressDto()
                {
                    PS="12345", DT="23456",Model="12",Material="34",ProductName="56",Lengths="78",Quantity=(decimal)10,
                    Delivery=DateTime.Now,PersonGuest ="测试数据",OrderType="adfa",Brand="1231",PeopleDelivery=DateTime.Now,
                    PeopleStyle ="sasda",ShipType=1,ononeIcomeState="未完成",packIcomeState="未完成",packShipState="未完成"
                }
            };
            //var searchDto = new BusinessSearchDto { OrderNo = "", StyleNo = "", ModelNo = "", BrandNo = "FOSSIL", OrderTypeNo = "" };
            //List<BusinessProgressDto> bpList = repository.GetLaterMsg(searchDto);

            string[] oldColumn = ExcelHelper.GetPropertyNameArray<BusinessProgressDto>();
            string[] newColumn = new string[] {"订单号","款号","型号","名称","材料","长度","数量","交货期","人客", "单据类型", "品牌", "人客货期", "人客款号", "出货类型","完生产","包装收货","包装出货"};

            //类型转换（将List转化为DataTable）
            DataTable ExportDt = ExcelHelper.ListToDataTable<BusinessProgressDto>(bpList);
            //可以考虑读取配置文件
            string path = "/SaveFile/";
            string fileName = "";
            if (ExportTypeIndex == 1)
            {
                fileName = "导出CSV.csv";
            }
            else if (ExportTypeIndex == 2)
            {
                fileName = "导出XLS.xls";
            }
            else
            {
                fileName = "导出XLSX.xlsx";
            }

            string streamFileName = Path.Combine(Request.MapPath(path), Path.GetFileName(fileName));

            //调用改写的NPOI方法            
            if (ExportTypeIndex == 1)
            {
                return File(path + fileName, "application/zip-x-compressed", "物流订单模板导出.csv");
            }
            else if (ExportTypeIndex == 2)
            {
                ExcelHelper.MyExport(ExportDt, "大家好我是表头", streamFileName, "1", oldColumn, newColumn);
                return new FilePathResult(path + fileName, "application/vnd.ms-excel");
            }
            else
            {
                ExcelHelper.MyExport(ExportDt, "大家好我是表头", streamFileName, "1", oldColumn, newColumn);
                return new FilePathResult(path + fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}