var bill = {
    init: function () {
        bill.tblBill();
        bill.tblBillDetail();
        $('#btnSeach').click(function () {
            bill.tblBill();
        });
    },
    tblBill: function () {
        $("#tblBill").bootstrapTable('destroy');
        $("#tblBill").bootstrapTable({
            method: 'get',
            url: "/Admin/Bill/GetBillData",
            queryParams: function (p) {
                var param = $.extend(true, {
                    search: $('#txtSearch').val(),
                    offset: p.offset,
                    limit: p.limit,
                }, p);
                return param;
            },
            formatLoadingMessage: function () {
                return '<span>Đang tải dữ liệu...</span>';
            },
            formatNoMatches: function () {
                return '<span>Không có dữ liệu</span>';
            },
            striped: true,
            sidePagination: 'server',
            pagination: true,
            paginationVAlign: 'bottom',
            search: false,
            pageSize: 10,
            pageList: [10, 50, 500],
            columns: [

                {
                    field: "orderCode",
                    title: "Mã đơn hàng",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        return '<a title="Chi tiết đơn hàng" href="/Admin/Bill/BillDetail?Id=' + row.idbill + '">' + value + '</a>';
                    }
                },
                {
                    field: "fullName",
                    title: "Tên khách hàng",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "phone",
                    title: "Số điện thoại",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "address",
                    title: "Địa chỉ",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "orderDate",
                    title: "Ngày đặt hàng",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        return moment(value).format('DD/MM/YYYY');
                    }
                },
                {
                    field: "status",
                    title: "Trạng thái",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value == true) {
                            html += '<button class="btn btn-success btn-sm">Đã duyệt</button>';
                        } else {
                            html += '<button class="btn btn-danger btn-sm btnStatus">Chờ xử lý</button>';
                        }
                        return html;
                    },
                    events: {
                        'click .btnStatus': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn duyệt đơn hàng này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/Bill/ChangeStatus',
                                                type: 'post',
                                                data: {
                                                    Id: row.idbill,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblBill").bootstrapTable('refresh', { silent: true });
                                                    }
                                                    else {
                                                        base.error(res.message);
                                                    }
                                                }
                                            });
                                        }
                                    },
                                    cancel: {
                                        text: 'Đóng',
                                        btnClass: 'btn btn-danger'
                                    },
                                }
                            });
                        }
                    }
                },

            ],
            onLoadSuccess: function (data) {

            },
        })
    },
    tblBillDetail: function () {
        $("#tblBillDetail").bootstrapTable('destroy');
        $("#tblBillDetail").bootstrapTable({
            method: 'get',
            url: "/Admin/Bill/GetBillDetail",
            queryParams: function (p) {
                var param = $.extend(true, {
                    billId: $('#ipIdBillHidden').val(),
                    offset: p.offset,
                    limit: p.limit,
                }, p);
                return param;
            },
            formatLoadingMessage: function () {
                return '<span>Đang tải dữ liệu...</span>';
            },
            formatNoMatches: function () {
                return '<span>Không có dữ liệu</span>';
            },
            striped: true,
            sidePagination: 'server',
            pagination: true,
            paginationVAlign: 'bottom',
            search: false,
            pageSize: 10,
            pageList: [10, 50, 500],
            columns: [

                {
                    field: "productName",
                    title: "Tên sản phẩm",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "quantity",
                    title: "Số lượng",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "totalPrice",
                    title: "Tổng tiền",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        return accounting.formatMoney(value, "", 0, ",", ".", "%v%s");
                    }
                },            

            ],
            onLoadSuccess: function (data) {
                $('#totalPriceOrder').html(accounting.formatMoney(data.totalPriceAll, "", 0, ",", ".", "%v%s"));
            },
        })
    },
}
$(document).ready(function () {
    bill.init();
});