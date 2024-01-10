var product = {
    init: function () {
        product.tblProduct();
        $('#btnSeach').click(function () {
            product.tblProduct();
        });
        $('#btnCreateSubmit').on('click', function () {
            var formData = new FormData();
            formData.append("ProductId", $('#ipIdHidden').val());
            formData.append("ProductName", $('#ipProductName').val());
            formData.append("Idtype", $('#sltProductType').val());
            formData.append("Price", $('#ipPrice').val());
            formData.append("PromotionalPrice", $('#ipPromotionalPrice').val());
            var fileUpload = $("#ipFileUpload").get(0);
            var files = fileUpload.files;
            formData.append("fileUpload", files[0]);
            var desc = CKEDITOR.instances['TxtDescription'].getData();
            formData.append('Description', desc);
            $.ajax({
                url: '/Admin/Product/CreateOrEdit',
                type: 'post',
                processData: false,
                contentType: false,
                data: formData,
                success: function (res) {
                    if (res.status) {
                        base.success(res.message);
                        setTimeout(function () {
                            window.location.href = '/Admin/Product';
                        }, 1500);
                    } else {
                        base.error(res.message);
                    }
                }
            });
        });
    },
    tblProduct: function () {
        $("#tblProducts").bootstrapTable('destroy');
        $("#tblProducts").bootstrapTable({
            method: 'get',
            url: "/Admin/Product/GetProductData",
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
                    field: "productName",
                    title: "Tên sản phẩm",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "image",
                    title: "Ảnh",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value != null && value != undefined && value != '') {
                            html += '<a href="/Uploads/Product/' + value + '"><img style="width:50px;height=50px;object-fit:cover" src="/Uploads/Product/' + value + '" /></a>';
                        }
                        return html;
                    }
                },
                {
                    field: "typeName",
                    title: "Danh mục",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "price",
                    title: "Giá tiền",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value != null && value != undefined && value != '') {
                            html += '<p>Giá tiền: '+ accounting.formatMoney(value, "", 0, ",", ".", "%v%s") +'</p>';
                        }
                        if (row.promotionalPrice != null && row.promotionalPrice > 0) {
                            html += '<p>Giá khuyến mãi: ' + accounting.formatMoney(row.promotionalPrice, "", 0, ",", ".", "%v%s") + '</p>';
                        }
                        return html;
                    }
                },
                {
                    title: "Chức năng",
                    valign: 'middle',
                    align: 'center',
                    class: 'CssAction',
                    formatter: function (value, row, index) {
                        var action = "";
                        action += '<a href="/Admin/Product/CreateOrEdit?Id=' + row.productId + '" class="btn btn-primary btn-sm btnEdit" title="Sửa"><i class="bx bx-pencil"></i></a>\
                        <a class=" btn btn-danger btn-sm btnDelete" title="Xóa"><i class="bx bx-trash"></i></a>';
                        return action;
                    },
                    events: {
                        'click .btnDelete': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn xóa sản phẩm này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/Product/Delete',
                                                type: 'post',
                                                data: {
                                                    Id: row.productId,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblProducts").bootstrapTable('refresh', { silent: true });
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
    product.init();
});