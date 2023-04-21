using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Constants
{
    public class EditStatus
    {
        public const int IMG_IDX_STT_NONE = 0;
        public const int IMG_IDX_STT_EDIT = 1;
        public const int IMG_IDX_STT_DELETE = 2;
        public const int IMG_IDX_STT_NEW = 3;
        //
        public const string ITEM_VALUE_STT_NONE = "O";
        public const string ITEM_VALUE_STT_EDIT = "E";
        public const string ITEM_VALUE_STT_DELETE = "D";
        public const string ITEM_VALUE_STT_NEW = "N";
    }
    public class ErrorMessage
    {
        public const string MSG_WARNING_FLAGACTIVE_VALUE = "Chỉ nhập 0 hoặc 1";
        public const string MSG_WARNING_EMPTY_VALUE = "Không được trống";
        public const string MSG_WARNING_REMARK = "Ghi chú không được > 400 ký tự";
        public const string MSG_WARNING_NOT_NUMERIC = "Không phải là số";
        public const string MSG_WARNING_NUMERIC = "Phải lớn hơn 0";
        public const string MSG_WARNING_NOT_INTNUMERIC = "Không phải là số nguyên";
        public const string MSG_WARNING_NOT_DATETIME = "Không đúng định dạng 'MM/dd/yyyy'";
        public const string MSG_WARNING_INVALID_EXCEL_FORMAT = "Số cột không hợp lệ";
        public const string MSG_WARNING_INVALID_AMOUNT = "Số tiền thanh toán không hợp lệ";
        public const string MSG_WARNING_NOEMPTYVALUE = "Không được để trống";
        public const string MSG_WARNING_SHORT_PASSWORD = "Mật khẩu có ít nhất 6 kí tự";
        public const string MSG_FIND_NOT_FOUND = "Không tìm thấy kết quả nào";
        public const string MSG_CONNECT_WS = "Lỗi kết nối dịch vụ";
        public const string MSG_WARNING_DUPLICATE = "Lỗi trùng khóa chính";
        public const string MSG_WARNING_DUPLICATE_DATA = "Lỗi trùng dữ liệu";
        public const string MSG_WARNING_INVALID_DATA_TYPE = "Kiểu dữ liệu không phù hợp";
        public const string MSG_WARNING_DUPLICATE_GROUP_CODE = "Mã nhóm đã tồn tại";
        public const string MSG_WARNING_DUPLICATE_USER_CODE = "Mã người dùng đã tồn tại";
        public const string MSG_WARNING_DUPLICATE_SERIAL_NO = "Số Serial đã tồn tại";
        public const string MSG_WARNING_ACC_CODE_SUB = "Mã tài khoản con phải bao hàm mã tài khoản cha";
        public const string MSG_WARNING_DUPLICATE_LOTNO = "Số LOT này đã tồn tại!";
        public const string MSG_WARNING_DUPLICATE_SERIALNO = "Số Serial này đã tồn tại!";
        public const string MSG_WARNING_IMPORT_EXCEL = "Chưa nhập dữ liệu excel";

    }

    public class Nonsense
    {
        public const string HTC_DEALER_CODE = "htc";
        public const string FILTER_EXCEL_DIALOG = "xls files (*.xls)|*.xls|All files (*.*)|*.*";
        public const string NUMERIC_DB_FORMAT = "{0:0,0.00}";
        public const string INTEGER_DB_FORMAT = "{0:0,0}";
        public const string DATE_TIME_FORMAT = "yyyy-MM-dd";
        public const string DATE_TIME_FORMAT_TVAN = "dd/MM/yyyy";
        public const string DATE_TIME_FORMAT_TVAN_VN = "dd-MM-yyyy";
        public const string DATE_TIME_FULL_FORMAT_TVAN_VN = "dd-MM-yyyy HH:mm:ss";
        public const string MONTH_YEAR_FORMAT = "yyyy-MM";
        public const string DATE_TIME_DB_FORMAT = "yyyy-MM-dd";
        public const string DATE_TIME_FULL_DB_FORMAT = "yyyy-MM-dd HH:mm:ss";
        //public const string DATE_TIME_FULL_DB_FORMAT_VN = "yyyy-dd-MM HH:mm:ss";
        public const string MESS_SUBMIT_DELETE_DATA = "Bạn chắc chắn xóa dữ liệu đã chọn";
        public const string MESS_NOROWS_ISSELECTED = "Chưa chọn dữ liệu làm việc";
        public const string MESS_NOTFOUND_RESULT = "Không có kết quả nào phù hợp điều kiện tìm kiếm";
        public const string MESS_NO_SEARCH_CONDITION = "Hãy nhập điều kiện tìm kiếm";
        public const string MESS_CREATE_SUCCESS = "Dữ liệu đã được tạo thành công!";
        public const string MESS_CREATE_UNSUCCESS = "Dữ liệu tạo không thành công!";
        public const string MESS_UPDATE_SUCCESS = "Dữ liệu đã được cập nhật thành công!";
        public const string MESS_UPDATE_UNSUCCESS = "Dữ liệu cập nhật không thành công!";
        public const string MESS_APPROVE_SUCCESS = "Dữ liệu đã được duyệt thành công!";
        public const string MESS_APPROVE_UNSUCCESS = "Dữ liệu duyệt không thành công!";
        public const string MESS_DELETE_SUCCESS = "Dữ liệu đã được xóa!";
        public const string MESS_DELETE_UNSUCCESS = "Dữ liệu không được phép được xóa!";
        public const string MESS_BORROW_SUCCESS = "Mượn TSBĐ thành công!";
        public const string MESS_TRATSBD_SUCCESS = "Trả TSBĐ thành công!";
        public const string MESS_EXPORT_EXCEL_NOT_CHECK = "Chưa chọn dữ liệu xuất excel";
        public const string MESS_EXPORT_EXCEL_SUCCESS = "Đã xuất excel thành công";
        public const string MESS_EXPORT_PDF_SUCCESS = "Đã xuất pdf thành công";
        public const string MESS_IMPORT_EXCEL_SUCCESS = "Đã nhập dữ liệu excel thành công";
        public const string MESS_GET_T24 = "Lấy dữ liệu T24 thành công!";
        public const string MESS_WARNING_CONDITION_4SEARCH = "Hãy nhập ít nhất một điều kiện tìm kiếm";
        public const string MESS_WARNING_EXITS_DATA = " đã tồn tại trong hệ thống!";
        public const string MESS_DO_IT = "Thao tác thực hiện thành công";
        public const string MESS_NO_DATA = "Không có dữ liệu";
        public const string MESS_DATA_ERROR = "Dữ liệu không hợp lệ!";
        public const string MESS_NO_CHECK = "Chưa chọn hàm để gán!";
        public const string MESS_RESET_PASS_SUCCESS = "Thay đổi mật khẩu thành công!";
        public const string MESS_CHECK_FILEIMPORT = "File dữ liệu import không hợp lệ!";
        public const string MESS_CHECK_FILE = "File Excel không hợp lệ!";
        public const string MESS_CHECK_FILE_NULL = "File Excel nhập không có dữ liệu!";
        public const string MESS_WARNING_EXITS_DATA1 = " Đặc tả xe đã có trong hợp đồng ngoại, vui lòng chọn đặc tả xe khác!";
        public const string MESS_WARNING_EXITS_DATA2 = " Chi tiết TSBĐ đã tồn tại trong lưới mô tả chi tiết TSBĐ, vui lòng chọn lại!";
        public const string MESS_CHECK_UPDATE_CONTRACTOVERSEA = "Cập nhật không thành công, phải có ít nhất 1 spec trong hợp đồng ngoại!";
        public const string MESS_NO_CHECK_Serial = "Chưa chọn Serial!";
        public const string MESS_NO_CHECK_WORKORDERNO = "Chưa chọn lệnh sản xuất!";
        public const string MESS_DESIGN_BY = "idocnet.com";

        public const string MESS_NO_SELECTROW = "Chưa chọn trạm sản xuất!";
        public const string MESS_LTV = "Cột LTV không được để trống!";
        public const string MESS_NOMINALVALUE = "Cột ĐGĐV không được để trống!";
        public const string MESS_GTDG = "Cột GTĐG không được để trống!";
        public const string MESS_KHS = "Cột KHS không được để trống!";
        public const string ZERO_NUMERIC_FORMAT = "0.00";
        public const string ZERO_INTEGER_FORMAT = "0";
        public const string ONE_NUMERIC_FORMAT = "1.00";
        public const string ONE_NUMERIC_FORMAT4 = "1.0000";
        public const string ONE_INTEGER_FORMAT = "1";

        public const int RemarkLength = 400;
        public const int digits2 = 2;

    }

    public class FILEPATH
    {
        public const string RESOURCES = "Resources";
    }
}
