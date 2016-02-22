function doSearch(gridid, containerid, toolid, strurl) {
    var afinish = "no";
    if ($("#Finished").is(":checked")) {
        afinish = "yes";
    }
    $(containerid).show();
    $(gridid).datagrid({
        method: 'GET',
        url: strurl,
        pageSize: 15,
        pageList: [5, 10, 15],
        idField: 'Id',
        loadMsg: '数据加载中，请稍后……',
        rownumbers: false,
        animate: true,
        collapsible: true,
        nowrap: true,
        pagination: true,
        singleSelect: true,
        showFooter:true,
        toolbar: toolid,
        queryParams: { doSearch: 'do', AfterDates: $('#AfterDates').val(), OrderNo: $('#OrderNo').val(), StyleNo: $('#StyleNo').val(), ModelNo: $('#ModelNo').val(), BrandNo: $('#BrandNo').val(), OrderTypeNo: $('#OrderTypeNo').find("option:selected").text(), Finished: afinish },
        columns: [
            [{ field: 'Id', hidden: true, rowspan: 2 },
            { title: '订单号', field: 'PS', rowspan: 2, width: 63 },
            { title: '款号', field: 'DT', rowspan: 2, width: 75 },
            { title: '型号', field: 'Model', rowspan: 2, width: 40 },
            { title: '名称', field: 'ProductName', rowspan: 2, width: 70 },
            { title: '材料', field: 'Material', rowspan: 2, width: 70 },
            { title: '长度', field: 'Lenghts', rowspan: 2, width: 45 },
            { title: '数量', field: 'Quantity', rowspan: 2, width: 50 },
            { title: '交货期', field: 'Delivery', rowspan: 2, formatter: function (value) { return formatterDate(value); }, width: 73 },
            { title: '人客', field: 'PersonGuest', rowspan: 2, width: 70 },
            //'单据类型'
            { title: '单据',colspan:1 },
            { title: '品牌', field: 'Brand', rowspan: 2, width: 120 },
            { title: '人客货期', field: 'PeopleDelivery', rowspan: 2, formatter: function (value) { return formatterDate(value); }, width: 70 },
            { title: '人客款号', field: 'PeopleStyle', rowspan: 2, width: 80 },
            //出货类型
            { title: '出货', colspan: 1 },
            { title: '完', colspan: 1 },            
            { title: '包装', colspan: 2 }],
            [{ title: '类型', field: 'OrderType', rowspan: 1, width: 40, align: 'center' },
            { title: '类型', field: 'ShipType', rowspan: 1, width: 40,align:'center' },
            { title: '生产', field: 'ononeIcomeState', formatter: function (value, row) { return formatterhave1(value, row); }, rowspan: 1, width: 45, align: 'center' },
            { title: '收货', field: 'packIcomeState', formatter: function (value, row) { return formatterhave2(value, row); }, rowspan: 1, width: 45, align: 'center' },
            { title: '出货', field: 'packShipState', formatter: function (value, row) { return formatterhave3(value, row); }, rowspan: 1, width: 45, align: 'center' }]
        ],
        //子表格
        view: detailview,
        detailFormatter: function (index, row) {
            return '<div style="padding:2px"><table id="ddv-' + index + '"></table></div>';
        },
        onExpandRow: function (index, row) {
            $('#ddv-' + index).datagrid({
                method:'GET',
                url: '../API/apiBusiness/GetHouseList?OrderNo=' + row.PS + "&&?temp=" + (new Date()).getTime(),
                //url: '../API/apiBusiness/GetSlaveList?OrderNo=' + row.PS + '&&StyleNo=' + row.DT + '&&ModelNo=' + row.Model + "&&?temp=" + (new Date()).getTime(),
                fitColumns: false,
                singleSelect: true,
                rownumbers: false,
                //显示页脚
                //showFooter: true,
                loadMsg: '',
                height: 'auto',
                rowStyler: function (index, row) {
                    if (row.incomeQuantity < row.Requirement) {
                        return 'background-color:#6293BB;color:#fff;';
                    }
                },
                frozenColumns: [[
                    { title: '配件', colspan: 1 },
                    { title: '配件名称', field: 'AccessoriesName', rowspan: 2, width: 180 },
                    { title: '每套', colspan: 1 },
                    { title: '订单数', field: 'Requirement', rowspan: 2, width: 50 },
                    { title: '发货数', field: 'DeliveryNumber', rowspan: 2, width: 50 }],          
                    [{ title: '编号', field: 'AccessoriesNo', rowspan: 1, width: 35, align: 'center' },
                    { title: '用量', field: 'EachDosage', rowspan: 1, width: 35, align: 'center' }
                ]],
                columns: [[
                    { title: '大啤', field: 'DPCC', formatter: function (value) { return formatterZero(value); }, rowspan: 2, width: 50 },
                    { title: 'WG', field: 'WGCC', formatter: function (value) { return formatterZero(value); }, rowspan: 2, width: 50 },
                    { title: 'DM', field: 'DMCC', formatter: function (value) { return formatterZero(value); }, rowspan: 2, width: 50 },
                    { title: '加工部', field: 'JGBCC', formatter: function (value) { return formatterZero(value); }, rowspan: 2, width: 50 },
                    { title: '机加工', colspan: 3 },
                    { title: '磨光', field: 'MGCC', formatter: function (value) { return formatterZero(value); }, rowspan: 2, width: 50 },
                    { title: '装配一', colspan: 2 },
                    { title: '装配二', colspan: 1 },
                    { title: '装配三', colspan: 2 },
                    { title: '包装', colspan: 2 },
                    { title: '订单', colspan: 1 }],
                    [{ title: '车锣雕', field: 'JJGACC', formatter: function (value) { return formatterZero(value); }, rowspan: 1, width: 48 },
                    { title: '钻床', field: 'JJGBCC', formatter: function (value) { return formatterZero(value); }, rowspan: 1, width: 48 },
                    { title: '手啤烧焊', field: 'JJGCCC', formatter: function (value) { return formatterZero(value); }, rowspan: 1, width: 50 },
                    { title: '电镀', field: 'ZP1ACC', formatter: function (value) { return formatterZero(value); }, rowspan: 1, width: 48 },
                    { title: '镭射房', field: 'ZP1BCC', formatter: function (value) { return formatterZero(value); }, rowspan: 1, width: 48 },
                    { title: '装配', field: 'ZP2CC', formatter: function (value) { return formatterZero(value); }, rowspan: 1, width: 48 },
                    { title: '科宝装配', field: 'ZP3ACC', formatter: function (value) { return formatterZero(value); }, rowspan: 1, width: 60 },
                    { title: '装配3B', field: 'ZP3BCC', formatter: function (value) { return formatterZero(value); }, rowspan: 1, width: 60 },
                    { title: '中科', field: 'BZACC', formatter: function (value) { return formatterZero(value); }, rowspan: 1, width: 55 },
                    { title: '科宝', field: 'BZBCC', formatter: function (value) { return formatterZero(value); }, rowspan: 1, width: 55 },
                    { title: '欠数', field: 'DDQS', formatter: function (value) { return formatterZero(value); }, rowspan: 1, width: 50 }

                 //{title:'配件',colspan:1},
                 //{ title: '配件名称', field: 'AccessoriesName', rowspan: 2, width: 180 },
                 //{ title: '规格', field: 'Specifications', rowspan: 2, width: 260 },
                 //{ title: '每套', colspan: 1 },
                 //{ title: '需要量', field: 'Requirement', rowspan: 2, width: 60 },
                 //{title:'交货',colspan:3},
                 //{ title: '收货', colspan: 2 }],
                 //[{ title: '编号', field: 'AccessoriesNo', rowspan: 1, width: 35,align: 'center' },
                 //{ title: '用量', field: 'EachDosage', rowspan: 1, width: 40, align: 'center' },
                 //{ title: '交货部门', field: 'Department', rowspan: 1, width: 65 },
                 //{ title: '预交期', field: 'NextPreliminaryDelivery', rowspan: 1, formatter: function (value) { return formatterDate(value); }, width: 70 },
                 //{ title: '交货总数', field: 'shipQuantity', rowspan: 1, width: 65 },
                 //{ title: '收货部门', field: 'NextDepartment', rowspan: 1, width: 65 },
                 //{ title: '收货总数', field: 'incomeQuantity', rowspan: 1, width: 65 }
                ]],
                onResize: function () {
                    $(gridid).datagrid('fixDetailRowHeight', index);
                },
                onLoadSuccess: function () {
                    setTimeout(function () {
                        $(gridid).datagrid('fixDetailRowHeight', index);
                    }, 0);
                }
            });
            $(gridid).datagrid('fixDetailRowHeight', index);
        }
    });
    $(gridid).datagrid('clearSelections');
    //$(gridid).datagrid({
    //    filterBtnIconCls: 'icon-filter'
    //});
    //$(gridid).datagrid().datagrid('enableFilter', [{
    //    field: 'Quantity',
    //    type: 'numberbox',
    //    options: { precision: 1 },
    //    op: ['equal', 'notequal', 'less', 'greater']
    //}
    //, {
    //    field: 'unitcost',
    //    type: 'numberbox',
    //    options: { precision: 1 },
    //    op: ['equal', 'notequal', 'less', 'greater']
    //}
    //, {
    //    field: 'status',
    //    type: 'combobox',
    //    options: {
    //        panelHeight: 'auto',
    //        data: [{ value: '', text: 'All' }, { value: 'P', text: 'P' }, { value: 'N', text: 'N' }],
    //        onChange: function (value) {
    //            if (value == '') {
    //                dg.datagrid('removeFilterRule', 'status');
    //            } else {
    //                dg.datagrid('addFilterRule', {
    //                    field: 'status',
    //                    op: 'equal',
    //                    value: value
    //                });
    //            }
    //            dg.datagrid('doFilter');
    //        }
    //    }
    //}
   //]);
}

function doClear(gridid, containerid, strurl) {
    $('#AfterDates').val("");
    $('#OrderNo').val("");
    $('#StyleNo').val("");
    $('#ModelNo').val("");
    $('#BrandNo').val("");
    $("#OrderTypeNo").val("");
    $('#Finished').attr("checked", false);
    $(gridid).datagrid({
        queryParams: { doSearch: 'no', AfterDates: "", OrderNo: "", StyleNo: "", ModelNo: "",BrandNo:"",OrderTypeNo:"", Finished: "no" }
    });
    $(gridid).datagrid('loadData', { total: 0, rows: [] });
    $(gridid).datagrid('clearSelections');
    $(containerid).hide();
}

function formatterZero(value) {
    if (value != null) {
        if (value == 0) { return ""; }
        return value;
    }
}

function formatterDate(value) {
    if (value != null) {
        //var date = (new Date(parseInt(value.substring(value.indexOf('(') + 1, value.indexOf(')')))));
        var date = (new Date(value));
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        return "<lable class='text1'>" + date.getFullYear() + "-" + month + "-" + currentDate + "</lable>";
    }
}

function formatterhave1(value, row) {
    if(value!=null){
    if (value != 0 || row.packIcomeState != 0 || row.packShipState != 0)
    {
        return "<img src='../Content/themes/icons/ok.png' />";
    } else {
        return "<img src='../Content/themes/icons/no.png' />"
    }
    }
}
function formatterhave2(value, row) {
    if (value != null) {
        if (value != 0 || row.packShipState != 0) {
            return "<img src='../Content/themes/icons/ok.png' />";
        } else {
            return "<img src='../Content/themes/icons/no.png' />"
        }
    }
}
function formatterhave3(value, row) {
    if (value != null) {
        if (value != 0) {
            return "<img src='../Content/themes/icons/ok.png' />";
        } else {
            return "<img src='../Content/themes/icons/no.png' />"
        }
    }
}