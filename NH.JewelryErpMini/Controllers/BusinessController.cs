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
    public class BusinessController : Controller
    {
        private BusinessRepository _repository;
        public BusinessController()
        {
            this._repository = new BusinessRepository();
        }
        // GET: Business
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MasterFiles()
        {
            //string str = "TST,PHOTO(PR SAMPLE)";
            //var s= str.Replace(",", "','");
            //s = "'" + s + "'";
            //var list = _repository.GetInspectedOutstandingReportData(s);
            //string[] s = str.Split(new char[] { ',' });
            //var monthAlias = _repository.GetMonthColumns(_repository.GetMarterFiles().Where(d => s.Contains(d.OrderType)));
            //var monthAlias = _repository.GetMonthColumns(_repository.GetMarterFiles());
            //var orderTypeAlias = _repository.GetOrderTypeColumns(_repository.GetMarterFiles());
            //_repository.trysql();
            //var OrderTypes = _repository.GetOrdderTypes();
            return View();
        }

        public string ImportExcel(HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null) { return "文件为空"; }
            try
            {
                //将硬盘路径转化为服务器路径的文件流
                string fileName = Path.Combine(Request.MapPath("~/SaveFile"), Path.GetFileName(fileUpload.FileName));
                //NPOI得到EXCEL的第一种方法              
                fileUpload.SaveAs(fileName);
                DataTable dtData = ExcelHelper.ImportExcelFile(fileName);

                Dictionary<string, string> toListColumns = new Dictionary<string, string>() {
                    { "FtyReference", "FtyReference" },{ "FactoryStyleNumber", "FactoryStyleNumber" }, { "PlatingMethod", "PlatingMethod" },
                    { "Brand", "Brand" }, { "Material", "Material" } , { "QRSStyles","QRSStyles"}, { "Type", "Type類型" },
                    {"PONO","PONO" }, {"CustSONO","CustSONO"}, {"FossilStyle","FossilStyle" }, { "JWLSize","JWLSize"},
                    {"ShipTo","ShipTo" }, { "POTotalQty","PO總訂單量"}, { "OrderType","OrderType"}, { "PODate","PODate"},
                    {"SapReqtDate","SAPREQTDATE" }, { "OrgDelDate","OrgDelDate"}, { "Month","Month"}, { "POQty","POQty"},
                    {"AccumulatedShippedQty","AccumulatedShippedQty" }, { "OpenQty","OpenQty"}, { "InspectedQty","InspectedQ"},
                    { "RealQty","實際欠數"},{ "MakingDept","做货部门"}, { "POLine","POLine"}
                };
                List<BusinessPartMasterFilesDto> list = DataTableHelper.ToListByContains<BusinessPartMasterFilesDto>(dtData, toListColumns);
                this._repository.Import(list);
                //得到EXCEL的第二种方法(第一个参数是文件流,第二个是excel标签名,第三个是第几行开始读0算第一行)
                //DataTable dtData2 = ExcelHelper.RenderDataTableFromExcel(fileName, "Sheet", 0);
                return "导入成功";
            }
            catch
            {
                return "导入失败";
            }
        }

        public ActionResult InspectedOutstandingReport(string ordertype)
        {
            ViewBag.orderType = ordertype;
            if (ordertype == null || (ordertype == ""))
            {
                ViewBag.Columns = _repository.GetMonthColumns(_repository.GetMarterFiles());
                return View();
            }
            string[] s = ordertype.Split(new char[] { ',' });
            ViewBag.Columns = _repository.GetMonthColumns(_repository.GetMarterFiles().Where(d => s.Contains(d.OrderType)));
            return View();
        }

        public FileResult InspectedOutstandingSaveas(int ExportTypeIndex, string ordertype)
        {
            var data = new List<InspectedOutstandingReportDto>();
            var title = new List<ColumnDto>();

            if ((ordertype == null) || (ordertype == "") || (ordertype == "0"))
            {
                data = _repository.GetInspectedOutstandingReportDataForExecl("", "");
                title = _repository.GetMonthTitleColumns(_repository.GetMarterFiles());
            }
            else {
                string str = ordertype.Replace(",", "','");
                str = "'" + str + "'";
                data = this._repository.GetInspectedOutstandingReportDataForExecl(str, ordertype);
                title = _repository.GetMonthTitleColumns(_repository.GetMarterFiles().Where(d => str.Contains(d.OrderType)));
            }

            string[] oldColumn = title.Select(t => t.Field).ToArray();
            string[] newColumn = title.Select(t => t.Title).ToArray();

            //类型转换（将List转化为DataTable）
            DataTable ExportDt = ExcelHelper.ListToDataTable<InspectedOutstandingReportDto>(data);
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

        public ActionResult AllBrandOutstandingReport(string brands)
        {
            ViewBag.Brand = brands;
            if (brands == null || (brands == "")|| (brands == "0"))
            {
                ViewBag.Columns = _repository.GetOrderTypeColumns(_repository.GetMarterFiles());
                return View();
            }
            string[] s = brands.Split(new char[] { ',' });
            ViewBag.Columns = _repository.GetOrderTypeColumns(_repository.GetMarterFiles().Where(d => s.Contains(d.Brand.ToUpper())));
            return View();
        }

        public FileResult AllBrandOutstandingSaveas(int ExportTypeIndex, string brands)
        {

            var data = new List<OrderTypeReportDto>();
            var title = new List<ColumnDto>();

            if ((brands == null) || (brands == "") || (brands == "0"))
            {
                data = _repository.GetAllBrandOutstandingReportDataForExecl("", "");
                title = _repository.GetOrderTypeTitleColumns(_repository.GetMarterFiles());
            }
            else {
                string str = brands.Replace(",", "','");
                str = "'" + str + "'";
                data = this._repository.GetAllBrandOutstandingReportDataForExecl(str, brands);
                title = _repository.GetOrderTypeTitleColumns(_repository.GetMarterFiles().Where(d => str.Contains(d.Brand.ToLower())));
            }          

            string[] oldColumn = title.Select(t => t.Field).ToArray();
            string[] newColumn = title.Select(t => t.Title).ToArray();

            //类型转换（将List转化为DataTable）
            DataTable ExportDt = ExcelHelper.ListToDataTable<OrderTypeReportDto>(data);
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

        public ActionResult TypeOutstandingReport(string types)
        {
            ViewBag.Type = types;
            if (types == null || (types == "") || (types == "0"))
            {
                ViewBag.Columns = _repository.GetOrderTypeColumns(_repository.GetMarterFiles());
                return View();
            }
            string[] s = types.Split(new char[] { ',' });
            ViewBag.Columns = _repository.GetOrderTypeColumns(_repository.GetMarterFiles().Where(d => s.Contains(d.Type.ToLower())));
            return View();
        }

        public FileResult TypeOutstandingSaveas(int ExportTypeIndex,string types)
        {
            var data = new List<OrderTypeReportDto>();
            var title = new List<ColumnDto>();

            if ((types == null) || (types == "") || (types == "0"))
            {
                data= _repository.GetTypeOutstandingReportDataForExecl("", "");
                title = _repository.GetOrderTypeTitleColumns(_repository.GetMarterFiles());
            }
            else {
                string str = types.Replace(",", "','");
                str = "'" + str + "'";
                data = this._repository.GetTypeOutstandingReportDataForExecl(str, types);
                title = _repository.GetOrderTypeTitleColumns(_repository.GetMarterFiles().Where(d => str.Contains(d.Type.ToLower())));
            }
       
            string[] oldColumn = title.Select(t => t.Field).ToArray();
            string[] newColumn = title.Select(t => t.Title).ToArray();

            //类型转换（将List转化为DataTable）
            DataTable ExportDt = ExcelHelper.ListToDataTable<OrderTypeReportDto>(data);
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