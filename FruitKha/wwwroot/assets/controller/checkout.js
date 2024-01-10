var checkOut = {
    init: function () {
        $('#btnOrder').click(function () {
            $.ajax({
                url: '/CheckOut/PaymentCheckOut',
                type: 'post',
                dataType: "json",
                data: {
                    phone: $('#ipPhone').val(),
                    address: $('#ipAddress').val()
                },
                beforSend: function () {
                    $('#btnOrder').prop('disabled', true);
                },
                success: function (res) {
                    $('#btnOrder').prop('disabled', false);
                    if (res.status) {
                        base.success(res.message);
                        setTimeout(function () {
                            window.location.href = "/";
                        }, 2000);
                    }
                    else {
                        base.error(res.message);
                    }
                },
                error: function () {
                    base.error('Có lỗi xảy ra vui lòng thử lại');
                }
            })
        });
    }
}
$(document).ready(function () {
    checkOut.init();
});