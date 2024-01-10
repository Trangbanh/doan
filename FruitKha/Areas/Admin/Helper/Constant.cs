using System.ComponentModel;

namespace FruitKha.Areas.Admin.Helper
{
    public class Constant
    {
        public const string MESSAGE_UNAUTHORISED = "Yêu cầu trái phép <br> Bạn không có quyền truy cập tài nguyên được yêu cầu do hạn chế về bảo mật.<br>Để được truy cập, vui lòng liên hệ với quản trị viên hệ thống của bạn.";
        public const string OLD_PASS_FAILED = "Mật khẩu cũ không hợp lệ";
        public const string VERIFY_PASS_FAILED = "Mật khẩu nhắc lại không hợp lệ";
        public const string ALERT_SUCCESS = "{0} thành công.";
        public const string ALERT_FAIL = "{0} không thành công.";
        public const string ALERT_EXIST_DATA = "{0} đã tồn tại.";
        public const string NULLPASS = "Bạn chưa nhập mật khẩu cũ";
        public const string OLD_PASS_EQUAL_NEW_PASS = "Mật khẩu mới không được trùng với mật khẩu cũ";
    }
}
