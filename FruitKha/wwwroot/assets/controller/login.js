var login = {
    init: function () {
        $('#btnLogin').click(function () {
            login.login();
        });
        $('#btnRegister').click(function () {
            login.register();
        });
        $('#btnChangePassword').click(function () {
            login.changePassword();
        });
        $('#btnEditProfile').click(function () {
            login.editProfile();
        });
        $('#btnResetPassword').click(function () {
            login.resetPassword();
        });
    },
    login: function () {
        $.ajax({
            url: '/Login/Login',
            type: 'post',
            data: {
                username: $('#username').val(),
                password: $('#password').val(),
            },
            beforSend: function () {
                $('#btnLogin').prop('disabled', true);
            },
            success: function (res) {
                $('#btnLogin').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                    window.location.href = '/';
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
    register: function () {
        $.ajax({
            url: '/Login/Register',
            type: 'post',
            data: $('#frmRegister').serialize(),
            beforSend: function () {
                $('#btnRegister').prop('disabled', true);
            },
            success: function (res) {
                $('#btnRegister').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                    setTimeout(function () {
                        window.location.href = '/Login/Index';
                    }, 1500);
                }
                else {
                    base.error(res.message);
                }
            },
            error: function () {
                base.error('Có lỗi xảy ra vui lòng thử lại');
            }
        });
    },
    changePassword: function () {
        $.ajax({
            url: '/Login/ChangePassword',
            type: 'post',
            dataType: "json",
            data: $('#frmChangePassword').serialize(),
            beforSend: function () {
                $('#btnChangePassword').prop('disabled', true);
            },
            success: function (res) {
                $('#btnChangePassword').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                    setTimeout(function () {
                        window.location.href = '/Login/Logout';
                    }, 1500);
                }
                else {
                    base.error(res.message);
                }
            },
            error: function () {
                base.error('Có lỗi xảy ra vui lòng thử lại');
            }
        });
    },
    editProfile: function () {
        $.ajax({
            url: '/Login/Profile',
            type: 'post',
            dataType: "json",
            data: $('#frmUser').serialize(),
            beforSend: function () {
                $('#btnEditProfile').prop('disabled', true);
            },
            success: function (res) {
                $('#btnEditProfile').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
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
    resetPassword: function () {
        $.ajax({
            url: '/Login/ResetPassword',
            type: 'post',
            dataType: "json",
            data: {
                phone: $('#ipPhoneNumber').val(),
                newPassword: $('#ipPasswordNew').val(),
                confirmPassword: $('#ipConfirmPassword').val()
            },
            beforSend: function () {
                $('#btnResetPassword').prop('disabled', true);
            },
            success: function (res) {
                $('#btnResetPassword').prop('disabled', false);
                if (res.status) {
                    base.success(res.message);
                    setTimeout(function () {
                        window.location.href = '/Login/Index';
                    }, 1500);
                }
                else {
                    base.error(res.message);
                }
            },
            error: function () {
                base.error('Có lỗi xảy ra vui lòng thử lại');
            }
        });
    },
}
$(document).on('keypress', function (e) {
    if (e.which == 13) {
        $('#btnLogin').trigger('click');
    }
});
$(document).ready(function () {
    login.init();
});