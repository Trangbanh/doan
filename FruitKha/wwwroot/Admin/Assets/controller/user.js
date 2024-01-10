var user = {
    init: function () {
        user.tblUser();
        $('#btnSeach').click(function () {
            user.tblUser();
        });
    },
    tblUser: function () {
        $("#tblUser").bootstrapTable('destroy');
        $("#tblUser").bootstrapTable({
            method: 'get',
            url: "/Admin/User/GetUserData",
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
                    field: "name",
                    title: "Tên",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "sdt",
                    title: "Số điện thoại",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "useName",
                    title: "Tài khoản",
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
                    field: "isActive",
                    title: "Trạng thái",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value) {
                            html += '<button class="btn btn-success btn-sm btnActive">Kích hoạt</button>';
                        } else {
                            html += '<button class="btn btn-danger btn-sm btnActive">Ngừng hoạt động</button>';
                        }
                        return html;
                    },
                    events: {
                        'click .btnActive': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn thay đổi trạng thái khách hàng này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/User/ChangeIsActive',
                                                type: 'post',
                                                data: {
                                                    Id: row.useId,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblUser").bootstrapTable('refresh', { silent: true });
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
}
$(document).ready(function () {
    user.init();
});