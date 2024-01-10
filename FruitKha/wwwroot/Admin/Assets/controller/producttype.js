var productType = {
    init: function () {
        productType.tblProductType();
        $('#btnSeach').click(function () {
            productType.tblProductType();
        });
        $('#btnCreate').click(function () {
            $('#titleModal').text("Thêm mới danh mục sản phẩm");
            $('#ipHiddenId').val(0);
            $('#ipTypeName').val('');
            $('#ipFileUpload').val('');
            $('#modalsAddEdit').modal('show');
        });
        $('#btnSubmit').click(function () {
            var formData = new FormData();
            formData.append("Idtype", $('#ipHiddenId').val());
            formData.append("TypeName", $('#ipTypeName').val());
            var fileUpload = $("#ipFileUpload").get(0);
            var files = fileUpload.files;
            formData.append("fileUpload", files[0]);
            $.ajax({
                url: '/Admin/ProductType/CreateOrEdit',
                type: 'post',
                contentType: false,
                processData: false,
                data: formData,
                success: function (res) {
                    if (res.status) {
                        base.success(res.message);
                        $("#tblProductType").bootstrapTable('refresh', { silent: true });
                        $('#modalsAddEdit').modal('hide');
                    } else {
                        base.error(res.message);
                    }
                }
            });
        });
    },
    tblProductType: function () {
        $("#tblProductType").bootstrapTable('destroy');
        $("#tblProductType").bootstrapTable({
            method: 'get',
            url: "/Admin/ProductType/GetProductTypeData",
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
                    field: "typeName",
                    title: "Tên danh mục",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "images",
                    title: "Ảnh ",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value != null && value != undefined && value != '') {
                            html += '<a href="/Uploads/Cates/' + value + '" target="_blank"><img style="width:50px;height=50px;object-fit:cover" src="/Uploads/Cates/' + value + '" /></a>'
                        }
                        return html;
                    }
                },
                {
                    field: "isHome",
                    title: "Hiên thị trang chủ",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value == true) {
                            html += '<button class="btn btn-success btn-sm btnIsHome">Hiển thị</button>';
                        } else {
                            html += '<button class="btn btn-danger btn-sm btnIsHome">Không</button>';
                        }
                        return html;
                    },
                    events: {
                        'click .btnIsHome': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn thay đổi trạng thái hiển thị này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/ProductType/ChangeIsHome',
                                                type: 'post',
                                                data: {
                                                    Id: row.idtype,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblProductType").bootstrapTable('refresh', { silent: true });
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
                {
                    title: "Chức năng",
                    valign: 'middle',
                    align: 'center',
                    class: 'CssAction',
                    formatter: function (value, row, index) {
                        var action = "";
                        action += '<a class=" btn btn-primary btn-sm btnEdit" title="Sửa"><i class="bx bx-pencil"></i></a>\
                        <a class=" btn btn-danger btn-sm btnDelete" title="Xóa"><i class="bx bx-trash"></i></a>';
                        return action;
                    },
                    events: {
                        'click .btnEdit': function (e, value, row, index) {
                            $('#titleModal').text("Chỉnh sửa danh mục sản phẩm");
                            $('#ipHiddenId').val(row.idtype);
                            $('#ipTypeName').val(row.typeName);
                            $('#ipFileUpload').val('');
                            $('#modalsAddEdit').modal('show');
                        },
                        'click .btnDelete': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn xóa danh mục này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/ProductType/Delete',
                                                type: 'post',
                                                data: {
                                                    Id: row.idtype,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblProductType").bootstrapTable('refresh', { silent: true });
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
    productType.init();
});