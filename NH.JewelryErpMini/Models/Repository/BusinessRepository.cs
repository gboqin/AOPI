using AutoMapper;
using NH.JewelryErpMini.Extension;
using NH.JewelryErpMini.Models.Domain;
using NH.JewelryErpMini.Models.Initial;
using NH.JewelryErpMini.Models.viewDto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NH.JewelryErpMini.Models.Repository
{
    public class BusinessRepository
    {
        private JewelryDbContext db;
        public BusinessRepository()
        {
            this.db = new JewelryDbContext();
        }

        public IQueryable<BusinessProgress> GetAll()
        {
            return this.db.BusinessProgresses;
        }

        public easyuiGridDto<BusinessProgress> GetBySearch(string rows, string page, string doSearch, BusinessSearchDto searchDto)
        {
            if (doSearch == "do")
            {
                int _page = int.Parse(page);
                int _rows = int.Parse(rows);
                DateTime beginDate = DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
                DateTime? endDate = null;
                if (!string.IsNullOrEmpty(searchDto.AfterDates)) {
                    endDate = Convert.ToDateTime(DateTime.Now.AddDays(double.Parse(searchDto.AfterDates)).ToShortDateString().ToString());
                }

                IQueryable<BusinessProgress> list = this.db.BusinessProgresses.Where(b=>(!string.IsNullOrEmpty(searchDto.OrderNo)? b.PS==searchDto.OrderNo:true)
                  &&(!string.IsNullOrEmpty(searchDto.StyleNo)?b.DT==searchDto.StyleNo:true)&&(!string.IsNullOrEmpty(searchDto.ModelNo)?b.Model==searchDto.ModelNo:true)
                  && (!string.IsNullOrEmpty(searchDto.BrandNo) ? b.Brand.Contains(searchDto.BrandNo) : true)
                  && (!string.IsNullOrEmpty(searchDto.OrderTypeNo) ? b.OrderType==searchDto.OrderTypeNo : true) && (!string.IsNullOrEmpty(searchDto.AfterDates) ? b.PeopleDelivery >= beginDate : true)
                  && (!string.IsNullOrEmpty(searchDto.AfterDates)?b.PeopleDelivery<= endDate : true)
                  //加入System.Data.Entity
                  //&& (System.Data.Objects.SqlClient.SqlFunctions.DateDiff())
                  &&(searchDto.Finished=="yes"? true : b.packShipState == 0)
                  &&b.PS !=null
                  &&b.Quantity>0
                ).OrderBy(g => g.Id);
                int? _total = list.Count();
                var data = list
                    .Skip((_page - 1) * _rows)
                    .Take(_rows)
                    .ToList();
                return new easyuiGridDto<BusinessProgress> { total = _total, rows = data };
            }
            else
            {
                return new easyuiGridDto<BusinessProgress> { total = 0, rows = this.GetAll().Where(b => b.Id == -1).OrderBy(b => b.PS).ToList() };
            }
        }

        public easyuiFootGridDto<BusinessProgress, ProgressFootDto> GetListBySearch(string rows, string page, string doSearch, BusinessSearchDto searchDto)
        {
            if (doSearch == "do")
            {
                int _page = int.Parse(page);
                int _rows = int.Parse(rows);
                DateTime beginDate = DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
                DateTime? endDate = null;
                if (!string.IsNullOrEmpty(searchDto.AfterDates))
                {
                    endDate = Convert.ToDateTime(DateTime.Now.AddDays(double.Parse(searchDto.AfterDates)).ToShortDateString().ToString());
                }

                IQueryable<BusinessProgress> list = this.db.BusinessProgresses.Where(b => (!string.IsNullOrEmpty(searchDto.OrderNo) ? b.PS == searchDto.OrderNo : true)
                  && (!string.IsNullOrEmpty(searchDto.StyleNo) ? b.DT == searchDto.StyleNo : true) && (!string.IsNullOrEmpty(searchDto.ModelNo) ? b.Model == searchDto.ModelNo : true)
                  && (!string.IsNullOrEmpty(searchDto.BrandNo) ? b.Brand.Contains(searchDto.BrandNo) : true)
                  && (!string.IsNullOrEmpty(searchDto.OrderTypeNo) ? b.OrderType == searchDto.OrderTypeNo : true) && (!string.IsNullOrEmpty(searchDto.AfterDates) ? b.PeopleDelivery >= beginDate : true)
                  && (!string.IsNullOrEmpty(searchDto.AfterDates) ? b.PeopleDelivery <= endDate : true)
                  //加入System.Data.Entity 
                  //&& (System.Data.Objects.SqlClient.SqlFunctions.DateDiff())
                  && (searchDto.Finished == "yes" ? true : b.packShipState == 0)
                  && b.PS != null
                  && b.Quantity > 0
                ).OrderBy(g => g.Id);
                int? _total = list.Count();
                var foot = new ProgressFootDto { PS = "数量合计：", DT = list.Sum(b => b.Quantity).ToString() };
                var data = list
                    .Skip((_page - 1) * _rows)
                    .Take(_rows)
                    .ToList();
                //footer与formater之间的格式会冲突，即js中formater，不但格式化单元格内容，还会把footer中的数据格式化
                return new easyuiFootGridDto<BusinessProgress, ProgressFootDto> { total = _total, rows = data,footer= new List<ProgressFootDto> { foot } };
            }
            else
            {
                return new easyuiFootGridDto<BusinessProgress, ProgressFootDto> { total = 0, rows = this.GetAll().Where(b => b.Id == -1).OrderBy(b => b.PS).ToList(),footer =null };
            }
        }

        public easyuiGridDto<BusinessDartProgress> GetSlaveList(string PS)
        {
            IQueryable<BusinessDartProgress> list = this.db.BusinessDartProgresses.Where(b => b.PS == PS).OrderBy(b=>b.Department);
            int? _total = list.Count();
            var data = list.ToList();
            return new easyuiGridDto<BusinessDartProgress> { total = _total, rows = data };
        }

        public easyuiGridDto<DeptWarehouse> GetHouseList(string PS)
        {
            //,string DT,string Model
            //IQueryable<BusinessDartProgress> list = this.db.BusinessDartProgresses.Where(b => b.PS == PS &&  b.DT == DT && b.Model == Model).OrderBy(g => g.PS);
            //IQueryable<BusinessDartProgress> list = this.db.BusinessDartProgresses.Where(b => b.PS == PS).OrderBy(b=>b.Department);
            IQueryable<DeptWarehouse> list = this.db.DeptWarehousese.Where(b => b.PS == PS);
            int? _total = list.Count();
            var data = list.ToList();
            return new easyuiGridDto<DeptWarehouse> { total = _total, rows =data };
        }

        //导入excel
        public void Import(List<BusinessPartMasterFilesDto> list)
        {
            //BusinessPartMasterFiles,Id未设为标记
            List <BusinessPartMasterFiles> dlist = AutoMapperExtension.MapToList< BusinessPartMasterFilesDto, BusinessPartMasterFiles>(list);
            foreach(var item in dlist)
            {
                db.BusinessPartMasterFileses.Add(item);
            }
            db.SaveChanges();
            //try1通过
            //foreach (var item in list)
            //{
            //    Mapper.CreateMap<BusinessPartMasterFilesDto, BusinessPartMasterFiles>();
            //    BusinessPartMasterFiles model = Mapper.Map<BusinessPartMasterFilesDto, BusinessPartMasterFiles>(item);
            //    db.BusinessPartMasterFileses.Add(model);
            //}
            //db.SaveChanges();
        }

        public IQueryable<BusinessPartMasterFiles> GetMarterFiles()
        {
            return this.db.BusinessPartMasterFileses;
        }

        //获取月份作为列,easyui datagrid
        public List<ColumnDto> GetMonthColumns(IQueryable<BusinessPartMasterFiles> list)
        {
            List<ColumnDto> monthColumns = new List<ColumnDto>();
            List<TitleDto> MonthTitles = list.Select(d => new TitleDto { Title = d.Month }).Distinct().ToList();
            var monthtitles = MonthTitles.OrderBy(m => m.Title).ToList();
            int count = monthtitles.Count;
            for(var i=0;i< count; i++)
            {
                monthColumns.Add(new ColumnDto { Field="Month"+(i+1).ToString(),Title= monthtitles[i].Title.ToString()});
            }
            return monthColumns;
        }
        //获取月份作为列,Saveas
        public List<ColumnDto> GetMonthTitleColumns(IQueryable<BusinessPartMasterFiles> list)
        {
            List<ColumnDto> monthTitleColumns = new List<ColumnDto>();
            monthTitleColumns.Add(new ColumnDto { Field = "Brand", Title = "Brand" });
            List<TitleDto> MonthTitles = list.Select(d => new TitleDto { Title = d.Month }).Distinct().ToList();
            var monthTitles = MonthTitles.OrderBy(m => m.Title).ToList();
            int count = monthTitles.Count;
            for (var i = 0; i < count; i++)
            {
                monthTitleColumns.Add(new ColumnDto { Field = "Month" + (i + 1).ToString(), Title = monthTitles[i].Title.ToString() });
            }
            monthTitleColumns.Add(new ColumnDto { Field = "Total", Title = "Total" });
            return monthTitleColumns;
        }

        //获取新、返、影等ordertype,作为列 easyui datagrid
        public List<ColumnDto> GetOrderTypeColumns(IQueryable<BusinessPartMasterFiles> list)
        {
            List<ColumnDto> orderTypeColumns = new List<ColumnDto>();
            List<TitleDto> OrderTypeTitles = list.Select(d => new TitleDto { Title = d.OrderType }).Distinct().ToList();
            var orderTypeTitles = OrderTypeTitles.OrderBy(m => m.Title).ToList();
            int count = orderTypeTitles.Count;
            for (var i = 0; i < count; i++)
            {
                orderTypeColumns.Add(new ColumnDto { Field = "OrderType" + (i + 1).ToString(), Title = orderTypeTitles[i].Title.ToString() });
            }
            return orderTypeColumns;
        }

        //获取新、返、影等ordertype,作为列 Saveas
        public List<ColumnDto> GetOrderTypeTitleColumns(IQueryable<BusinessPartMasterFiles> list)
        {
            List<ColumnDto> orderTypeTitleColumns = new List<ColumnDto>();
            orderTypeTitleColumns.Add(new ColumnDto { Field = "Month", Title= "Month" });
            List<TitleDto> OrderTypeTitles = list.Select(d => new TitleDto { Title = d.OrderType }).Distinct().ToList();
            var orderTypeTitles = OrderTypeTitles.OrderBy(m => m.Title).ToList();
            int count = orderTypeTitles.Count;
            for (var i = 0; i < count; i++)
            {
                orderTypeTitleColumns.Add(new ColumnDto { Field = "OrderType" + (i + 1).ToString(), Title = orderTypeTitles[i].Title.ToString() });
            }
            orderTypeTitleColumns.Add(new ColumnDto { Field = "Total", Title = "Total" });
            return orderTypeTitleColumns;
        }

        public easyuiReportFootGridDto<InspectedOutstandingReportDto, InspectedoutstandingReportFootDto> GetInspectedOutstandingReportData(string LinkOrderTypes, string OrderTypeStr)
        {
            //IList<InspectedOutstandingReportDto> data = db.Database.SqlQuery<InspectedOutstandingReportDto>("EXEC procInspectedOutstanding @OrderTypes",new SqlParameter("OrderTypes", "'TST','PHOTO(PR SAMPLE)'")).ToList();
            IList<InspectedOutstandingReportDto> data = db.Database.SqlQuery<InspectedOutstandingReportDto>("EXEC procInspectedOutstanding @LinkOrderTypes,@OrderTypeStr", new SqlParameter("LinkOrderTypes", LinkOrderTypes), new SqlParameter("OrderTypeStr", OrderTypeStr)).ToList();
            var foot = new InspectedoutstandingReportFootDto {
                Brand = "总计：",
                Month1 = data.Sum(d => d.Month1).ToString(),
                Month2 = data.Sum(d => d.Month2).ToString(),
                Month3 = data.Sum(d => d.Month3).ToString(),
                Month4 = data.Sum(d => d.Month4).ToString(),
                Month5 = data.Sum(d => d.Month5).ToString(),
                Month6 = data.Sum(d => d.Month6).ToString(),
                Month7 = data.Sum(d => d.Month7).ToString(),
                Month8 = data.Sum(d => d.Month8).ToString(),
                Month9 = data.Sum(d => d.Month9).ToString(),
                Month10 = data.Sum(d => d.Month10).ToString(),
                Month11 = data.Sum(d => d.Month11).ToString(),
                Month12 = data.Sum(d => d.Month12).ToString(),
                Month13 = data.Sum(d => d.Month13).ToString(),
                Month14 = data.Sum(d => d.Month14).ToString(),
                Month15 = data.Sum(d => d.Month15).ToString(),
                Total = data.Sum(d => d.Total).ToString()
            };
            return new easyuiReportFootGridDto<InspectedOutstandingReportDto, InspectedoutstandingReportFootDto> { rows = data, footer = new List<InspectedoutstandingReportFootDto> { foot } };
        }

        public List<InspectedOutstandingReportDto> GetInspectedOutstandingReportDataForExecl(string LinkOrderTypes, string OrderTypeStr)
        {
            List<InspectedOutstandingReportDto> data = db.Database.SqlQuery<InspectedOutstandingReportDto>("EXEC procInspectedOutstanding @LinkOrderTypes,@OrderTypeStr", new SqlParameter("LinkOrderTypes", LinkOrderTypes), new SqlParameter("OrderTypeStr", OrderTypeStr)).ToList();
            data.Add(new InspectedOutstandingReportDto
            {
                Brand = "总计：",
                Month1 = data.Sum(d => d.Month1)??null,
                Month2 = data.Sum(d => d.Month2) ?? null,
                Month3 = data.Sum(d => d.Month3) ?? null,
                Month4 = data.Sum(d => d.Month4) ?? null,
                Month5 = data.Sum(d => d.Month5) ?? null,
                Month6 = data.Sum(d => d.Month6) ?? null,
                Month7 = data.Sum(d => d.Month7) ?? null,
                Month8 = data.Sum(d => d.Month8) ?? null,
                Month9 = data.Sum(d => d.Month9) ?? null,
                Month10 = data.Sum(d => d.Month10) ?? null,
                Month11 = data.Sum(d => d.Month11) ?? null,
                Month12 = data.Sum(d => d.Month12) ?? null,
                Month13 = data.Sum(d => d.Month13) ?? null,
                Month14 = data.Sum(d => d.Month14) ?? null,
                Month15 = data.Sum(d => d.Month15) ?? null,
                Total = data.Sum(d => d.Total) ?? null
            });
            return data;
        }

        public easyuiReportFootGridDto<OrderTypeReportDto,OrderTypeReportFootDto> GetAllBrandOutstandingReportData(string linkBrands,string strBrands)
        {
            IList<OrderTypeReportDto> data = db.Database.SqlQuery<OrderTypeReportDto>("EXEC procAllBrandOutstanding @linkBrands,@strBrands", new SqlParameter("linkBrands", linkBrands), new SqlParameter("strBrands", strBrands)).ToList();
            var foot = new OrderTypeReportFootDto
            {
                Month = "总计：",
                OrderType1 = data.Sum(d => d.OrderType1).ToString(),
                OrderType2 = data.Sum(d => d.OrderType2).ToString(),
                OrderType3 = data.Sum(d => d.OrderType3).ToString(),
                OrderType4 = data.Sum(d => d.OrderType4).ToString(),
                OrderType5 = data.Sum(d => d.OrderType5).ToString(),
                OrderType6 = data.Sum(d => d.OrderType6).ToString(),
                OrderType7 = data.Sum(d => d.OrderType7).ToString(),
                OrderType8 = data.Sum(d => d.OrderType8).ToString(),
                OrderType9 = data.Sum(d => d.OrderType9).ToString(),
                OrderType10 = data.Sum(d => d.OrderType10).ToString(),
                OrderType11 = data.Sum(d => d.OrderType11).ToString(),
                OrderType12 = data.Sum(d => d.OrderType12).ToString(),
                Total = data.Sum(d => d.Total).ToString()
            };
            return new easyuiReportFootGridDto<OrderTypeReportDto, OrderTypeReportFootDto> { rows = data, footer = new List<OrderTypeReportFootDto> { foot } };
        }

        public List<OrderTypeReportDto> GetAllBrandOutstandingReportDataForExecl(string linkBrands, string strBrands)
        {
            List<OrderTypeReportDto> data = db.Database.SqlQuery<OrderTypeReportDto>("EXEC procAllBrandOutstanding @linkBrands,@strBrands", new SqlParameter("linkBrands", linkBrands), new SqlParameter("strBrands", strBrands)).ToList();
            data.Add(new OrderTypeReportDto
            {
                Month = "总计：",
                OrderType1 = data.Sum(d => d.OrderType1)??null,
                OrderType2 = data.Sum(d => d.OrderType2) ?? null,
                OrderType3 = data.Sum(d => d.OrderType3) ?? null,
                OrderType4 = data.Sum(d => d.OrderType4) ?? null,
                OrderType5 = data.Sum(d => d.OrderType5) ?? null,
                OrderType6 = data.Sum(d => d.OrderType6) ?? null,
                OrderType7 = data.Sum(d => d.OrderType7) ?? null,
                OrderType8 = data.Sum(d => d.OrderType8) ?? null,
                OrderType9 = data.Sum(d => d.OrderType9) ?? null,
                OrderType10 = data.Sum(d => d.OrderType10) ?? null,
                OrderType11 = data.Sum(d => d.OrderType11) ?? null,
                OrderType12 = data.Sum(d => d.OrderType12) ?? null,
                Total = data.Sum(d => d.Total) ?? null
            });
            return data;
        }

        public easyuiReportFootGridDto<OrderTypeReportDto, OrderTypeReportFootDto> GetTypeOutstandingReportData(string linkTypes, string strTypes)
        {
            IList<OrderTypeReportDto> data = db.Database.SqlQuery<OrderTypeReportDto>("EXEC procTypeOutstanding @linkTypes,@strTypes", new SqlParameter("linkTypes", linkTypes), new SqlParameter("strTypes", strTypes)).ToList();
            var foot = new OrderTypeReportFootDto
            {
                Month = "总计：",
                OrderType1 = data.Sum(d => d.OrderType1).ToString(),
                OrderType2 = data.Sum(d => d.OrderType2).ToString(),
                OrderType3 = data.Sum(d => d.OrderType3).ToString(),
                OrderType4 = data.Sum(d => d.OrderType4).ToString(),
                OrderType5 = data.Sum(d => d.OrderType5).ToString(),
                OrderType6 = data.Sum(d => d.OrderType6).ToString(),
                OrderType7 = data.Sum(d => d.OrderType7).ToString(),
                OrderType8 = data.Sum(d => d.OrderType8).ToString(),
                OrderType9 = data.Sum(d => d.OrderType9).ToString(),
                OrderType10 = data.Sum(d => d.OrderType10).ToString(),
                OrderType11 = data.Sum(d => d.OrderType11).ToString(),
                OrderType12 = data.Sum(d => d.OrderType12).ToString(),
                Total = data.Sum(d => d.Total).ToString()
            };
            return new easyuiReportFootGridDto<OrderTypeReportDto, OrderTypeReportFootDto> { rows = data, footer = new List<OrderTypeReportFootDto> { foot } };
        }

        public List<OrderTypeReportDto> GetTypeOutstandingReportDataForExecl(string linkTypes, string strTypes)
        {
            List<OrderTypeReportDto> data = db.Database.SqlQuery<OrderTypeReportDto>("EXEC procTypeOutstanding @linkTypes,@strTypes", new SqlParameter("linkTypes", linkTypes), new SqlParameter("strTypes", strTypes)).ToList();
            data.Add(new OrderTypeReportDto
            {
                Month = "总计：",
                OrderType1 = data.Sum(d => d.OrderType1) ?? null,
                OrderType2 = data.Sum(d => d.OrderType2) ?? null,
                OrderType3 = data.Sum(d => d.OrderType3) ?? null,
                OrderType4 = data.Sum(d => d.OrderType4) ?? null,
                OrderType5 = data.Sum(d => d.OrderType5) ?? null,
                OrderType6 = data.Sum(d => d.OrderType6) ?? null,
                OrderType7 = data.Sum(d => d.OrderType7) ?? null,
                OrderType8 = data.Sum(d => d.OrderType8) ?? null,
                OrderType9 = data.Sum(d => d.OrderType9) ?? null,
                OrderType10 = data.Sum(d => d.OrderType10) ?? null,
                OrderType11 = data.Sum(d => d.OrderType11) ?? null,
                OrderType12 = data.Sum(d => d.OrderType12) ?? null,
                Total = data.Sum(d => d.Total) ?? null
            });
            return data;
        }

        public List<TitleDto> GetOrdderTypes()
        {
            return db.BusinessPartMasterFileses.Select(b => new TitleDto { Title=b.OrderType}).Distinct().ToList();
        }

        public List<TitleDto> GetBrands()
        {
            var list = db.BusinessPartMasterFileses.Select(b => new TitleDto { Id=b.Brand.ToUpper(),Title = b.Brand.ToUpper() }).Distinct().ToList();
            List<TitleDto> data = new List<TitleDto>();
            data.Add(new TitleDto { Id="0",Title = "全部" });
            foreach(var item in list.OrderBy(l=>l.Title))
            {
                data.Add(item);
            }
            return data;
        }

        public List<TitleDto> GetTypes()
        {
            var list = db.BusinessPartMasterFileses.Select(b => new TitleDto { Id = b.Type.ToLower(), Title = b.Type.ToLower() }).Distinct().ToList();
            List<TitleDto> data = new List<TitleDto>();
            data.Add(new TitleDto { Id = "0", Title = "全部" });
            foreach (var item in list.OrderBy(l => l.Title))
            {
                data.Add(item);
            }
            return data;
        }
    }
}