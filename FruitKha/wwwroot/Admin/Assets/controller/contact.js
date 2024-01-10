var contact = {
    init: function () {
        contact.tblContact();
        $('#btnSeach').click(function () {
            contact.tblContact();
        });
        $('#btnSend').click(function () {
            $.ajax({
                type: 'post',
                url: '/Admin/Contact/RepContact',
                dataType: 'json',
                data: $('#frmContacts').serialize(),
                success: function (res) {
                    if (res.status) {
                        $('#modalsSendContact').modal('hide');
                        $("#tblContacts").bootstrapTable('refresh', { silent: true });
                        base.success(res.message);                     
                    } else {
                        base.error(res.message);
                    }
                }
            })
        });
    },
    tblContact: function () {
        $("#tblContacts").bootstrapTable('destroy');
        $("#tblContacts").bootstrapTable({
            method: 'get',
            url: "/Admin/Contact/GetData",
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
                    title: "Họ tên",
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
                    field: "email",
                    title: "Email",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "message",
                    title: "Nội dung",
                    align: 'left',
                    valign: 'left',
                },
                {
                    field: "isRead",
                    title: "Trạng thái",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value == 1) {
                            html += '<span class="badge bg-success">Đã đọc và phản hồi</span>';
                        } else {
                            html += '<span class="badge bg-warning">Chưa đọc</span>';
                        }
                        return html;
                    }
                },
                {
                    field: "createdDate",
                    title: "Ngày tạo",
                    align: 'left',
                    valign: 'left',
                    formatter: function (value, row, index) {
                        return moment(value).format('DD/MM/YYYY HH:mm');
                    }
                },
                {
                    title: "Chức năng",
                    valign: 'middle',
                    align: 'center',
                    class: 'CssAction',
                    formatter: function (value, row, index) {
                        var action = "";
                        action += '<a class=" btn btn-primary btn-sm btnSendMail" title="Trả lời"><i class="bx bx-mail-send"></i></a>';
                        return action;
                    },
                    events: {
                        'click .btnSendMail': function (e, value, row, index) {
                            $('#ipHiddenId').val(row.contactId);
                            $('#ipName').val(row.name);
                            $('#ipEmail').val(row.email);
                            $('#ipPhone').val(row.phone);
                            $('#txtMessage').val(row.message);
                            $('#modalsSendContact').modal('show');
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
    contact.init();
});