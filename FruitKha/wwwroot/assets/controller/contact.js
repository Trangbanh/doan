var contact = {
    init: function () {
        $('#btnSendContact').click(function () {
            $.ajax({
                type: 'post',
                url: '/Contact/SendContact',
                dataType: 'json',
                data: $('#fruitkha-contact').serialize(),
                success: function (res) {
                    if (res.status) {
                        base.success(res.message);
                        $('#name').val('');
                        $('#email').val('');
                        $('#phone').val('');
                        $('#message').val('');
                    } else {
                        base.error(res.message);
                    }
                }
            })
        });
    }
}
$(document).ready(function () {
    contact.init();
});