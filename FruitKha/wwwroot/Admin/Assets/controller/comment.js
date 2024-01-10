var comment = {
    init: function () {
        comment.tblComment();
        $('#btnSeach').click(function () {
            comment.tblComment();
        });
    },
    tblComment: function () {
        $("#tblComment").bootstrapTable('destroy');
        $("#tblComment").bootstrapTable({
            method: 'get',
            url: "/Admin/Comment/GetCommentData",
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
                    field: "title",
                    title: "Tiêu đề bài viết",
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
                    field: "commentContent",
                    title: "Nội dung bình luận",
                    align: 'left',
                    valign: 'left',
                },
                {
                    title: "Chức năng",
                    valign: 'middle',
                    align: 'center',
                    class: 'CssAction',
                    formatter: function (value, row, index) {
                        var action = "";
                        action += '<a class=" btn btn-danger btn-sm btnDelete" title="Xóa"><i class="bx bx-trash"></i></a>';
                        return action;
                    },
                    events: {
                        'click .btnDelete': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn xóa bình luận này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/Comment/Delete',
                                                type: 'post',
                                                data: {
                                                    Id: row.idcomment,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblComment").bootstrapTable('refresh', { silent: true });
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
    comment.init();
});