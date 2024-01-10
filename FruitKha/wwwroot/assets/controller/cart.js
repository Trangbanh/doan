var cart = {
    init: function () {

    },
    updateCart: function (productId) {
        var quantityPro = $('#ipQuantitypro-' + productId).val();
        $.ajax({
            url: '/Cart/UpdateCart',
            type: 'post',
            dataType: "json",
            data: {
                productId: productId,
                quantity: quantityPro
            },
            success: function (res) {
                if (res.status) {
                    location.reload();
                }
                else {
                    base.error(res.message);
                }
            },
            error: function () {
                base.error('Có lỗi xảy ra vui lòng thử lại');
            }
        })
    },
    deleteCart: function (productId) {
        $.ajax({
            url: '/Cart/DeleteCart',
            type: 'post',
            dataType: "json",
            data: {
                productId: productId,
            },
            success: function (res) {
                if (res.status) {
                    location.reload();
                }
                else {
                    base.error(res.message);
                }
            },
            error: function () {
                base.error('Có lỗi xảy ra vui lòng thử lại');
            }
        })
    }
}
$(document).ready(function () {
    cart.init();
});