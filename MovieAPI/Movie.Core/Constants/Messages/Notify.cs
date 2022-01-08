namespace Movie.Core.Constants
{
    public static class Notify
    {
        public class Account
        {
            public const string NOTIFY_ACCOUT_CHANGEPASSWORD_SUCCESS = "Thay đổi mật khẩu thành công!";
            public const string NOTIFY_ACCOUT_CHANGEPASSWORD_FAILTURE = "Thay đổi mật khẩu thất bại!";
            public const string NOTIFY_ACCOUT_FAIL = "Sai tài khoản hoặc mật khẩu";
            public const string NOTIFY_ACCOUT_VERIED = "Email chưa xác nhận!";
            public const string NOTIFY_ACCOUT_REGISTER_FAILURE = "Đăng ký thất bại";
            public const string NOTIFY_ACCOUT_REGISTER_SUCCESS = "Đăng ký thành công";

            public const string NOTIFY_ACCOUNT_INVALID_PASSWORD = "Bạn nhập sai mật khẩu rồi!";
            public const string NOTIFY_ACCOUT_INVALID_EMAIL = "Email không hợp lệ";
            public const string NOTIFY_ACCOUNT_EMAIL_NOT_FOUND = "Oh No! Email không tồn tại! Hãy đăng ký!";
            public const string NOTIFY_ACCOUNT_TOO_MANY_ATTEMPTS_TRY_LATER = "Quyền truy cập vào tài khoản này đã tạm thời bị vô hiệu hóa do nhiều lần đăng nhập không thành công. Bạn có thể khôi phục nó ngay lập tức bằng cách đặt lại mật khẩu của mình hoặc bạn có thể thử lại sau.";
        }
        public const string NOTIFY_SUCCESS = "Thêm mới bản ghi thành công";
        public const string NOTIFY_UPDATE = "Cập nhật bản ghi thành công";
        public const string NOTIFY_ERROR = "Có lỗi xảy ra khi thực hiện";
        public const string NOTIFY_DELETE = "Xóa bản ghi thành công";
        public const string NOTIFY_ISVALID_MOVIE = "Phim đã tồn tại! Thay đổi tiêu đề phim!";
        public const string NOTIFY_ISVALID_GENRES = "Thể loại đã tồn tại!";
        public const string NOTIFY_VALID_GENRES = "Thể loại không tồn tại!";
        public const string NOTIFY_ISVALID_COMPANY = "Công ty đã tồn tại!";
        public const string NOTIFY_VALID_COMPANY = "Công ty không tồn tại!";
    }
}
