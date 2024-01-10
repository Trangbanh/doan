var blogs = {
    init: function () {
        blogs.tblBlog();
        $('#btnSeach').click(function () {
            blogs.tblBlog();
        });
        $('#btnSubmitBlog').click(function () {
            var formData = new FormData();
            formData.append("Idblog", $('#ipIdHidden').val());
            formData.append("Title", $('#ipTitle').val());
            formData.append("ShortContent", $('#ipShortContent').val());
            var fileUpload = $("#ipFileUpload").get(0);
            var files = fileUpload.files;
            formData.append("fileUpload", files[0]);
            var desc = CKEDITOR.instances['TxtDescription'].getData();
            formData.append('Contents', desc);
            $.ajax({
                url: '/Admin/Blog/CreateOrEdit',
                type: 'post',
                processData: false,
                contentType: false,
                data: formData,
                success: function (res) {
                    if (res.status) {
                        base.success(res.message);
                        setTimeout(function () {
                            window.location.href = '/Admin/Blog/Index';
                        }, 1500);
                    } else {
                        base.error(res.message);
                    }
                }
            });
        });
    },
    tblBlog: function () {
        $("#tblBlogs").bootstrapTable('destroy');
        $("#tblBlogs").bootstrapTable({
            method: 'get',
            url: "/Admin/Blog/GetBlogData",
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
                    title: "Tiêu đề",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "image",
                    title: "Ảnh đại diện",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value != null && value != undefined && value != '') {
                            html += '<a href="/Uploads/Blogs/' + value + '" ><img style="width:50px;height=50px;object-fit:cover" src="/Uploads/Blogs/' + value + '" /></a>';
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
                        action += '<a class=" btn btn-primary btn-sm btnEdit" href="/Admin/Blog/CreateOrEdit?Id=' + row.idblog + '" title="Sửa"><i class="bx bx-pencil"></i></a>\
                        <a class=" btn btn-danger btn-sm btnDelete" title="Xóa"><i class="bx bx-trash"></i></a>';
                        return action;
                    },
                    events: {
                        'click .btnDelete': function (e, value, row, index) {
                            $.confirm({
                                title: 'Cảnh báo!',
                                content: 'Bạn chắc chắn muốn xóa bài viết này?',
                                buttons: {
                                    formSubmit: {
                                        text: 'Xác nhận',
                                        btnClass: 'btn btn-primary',
                                        action: function () {
                                            $.ajax({
                                                url: '/Admin/Blog/Delete',
                                                type: 'post',
                                                data: {
                                                    Id: row.idblog,
                                                },
                                                success: function (res) {
                                                    if (res.status) {
                                                        base.success(res.message);
                                                        $("#tblBlogs").bootstrapTable('refresh', { silent: true });
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
                        },
                    }
                },

            ],
            onLoadSuccess: function (data) {

            },
        })
    },
}
$(document).ready(function () {
    blogs.init();
});