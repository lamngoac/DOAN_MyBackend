using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.Veloca
{
    public class Ser_RO
    {
        public object RONoSys { get; set; } // Số Seq báo giá hệ thống sinh.

        public object OrgID { get; set; } // Mã Org.

        public object RONo { get; set; } // Số báo giá.

        public object NNTFullName { get; set; } // Tên Người NNT

        public object NNTAddress { get; set; } // Địa chỉ người nộp thuế

        public object NNTPhone { get; set; } // Điện thoại người nộp thuế

        public object Hotline { get; set; } // Hotline

        public object Website { get; set; } // Webisite

        public object NetworkID { get; set; } // Mã Network.

        public object CustomerCodeSys  { get; set; } // Mã khách hàng hệ thống sinh.

        public object CustomerCode { get; set; } // Mã khách hàng.

        public object CustomerName { get; set; } // Tên khách hàng.

        public object CustomerNameEN { get; set; } // Tên khách hàng tiếng anh

        public object CustomerAddress { get; set; } //  Địa chỉ khách hàng.

        public object CustomerMobilePhone { get; set; } // Số điện thoại moblie.

        public object CustomerPhoneNo { get; set; } // Số điện thoại.

        public object CustomerEmail { get; set; } // Email khách hàng.

        public object CustomerContactName { get; set; } // Tên người liên hệ.

        public object CustomerContactPhone { get; set; } // Số điện thoại người liên hệ

        public object CustomerContactEmail { get; set; } // Email người liên hệ.

        public object RequestCustomer { get; set; } // Yêu cầu khách hàng.

        public object PlateNoSys { get; set; } // Biển số hệ thống sinh.

        public object PlateNo { get; set; } // Biển số

        public object VIN { get; set; } // Số khung

        public object ColorCode { get; set; } // Màu

        public object BrandCode { get; set; } // Brand

        public object ProductGrpCode { get; set; } // Model

        public object ProductGrpName { get; set; } // Model

        public object ProductCode { get; set; } // Spec

        public object WarrantyDateUTC { get; set; } // Ngày bào hành 

        public object EngineNo { get; set; } // Số máy.

        public object FlagPlateNo { get; set; } // Cờ biển số

        public object AppDTimeUTC { get; set; } // Thời gian

        public object InsCodeSys { get; set; } // Mã hãng bảo hiểm

        public object InsCode { get; set; } // Mã người dùng hãng bảo hiểm

        public object InsName { get; set; } // Tên hãng bảo hiểm 

        public object InsNameEN { get; set; } // Tên tiếng anh hãng bảo hiểm.

        public object InsAddress { get; set; } // Địa chỉ hãng bảo hiểm.

        public object InsMobilePhone { get; set; } // Mobile hãng bảo hiểm.

        public object InsPhoneNo { get; set; } // Số điện thoại hãng bảo hiểm.

        public object InsEmail { get; set; } // Email hãng bảo hiểm.

        public object InsContractName { get; set; } // Tên người liên hệ hãng bảo hiểm 

        public object InsContractPhone { get; set; } // Số điện thoại người liên hệ hãng bảo hiểm 

        public object InsContractEmail { get; set; } // Email người liên hệ hãng bảo hiểm.

        public object InsContractNo { get; set; } // Số hợp đồng

        public object InsStartDateUTC { get; set; } // Ngày bắt đầu bảo hiểm 

        public object InsEndDateUTC { get; set; } // ngày kết thúc bảo hiểm

        public object SerPackCodeSys { get; set; } // Mã gói dịch vụ

        public object PlanedDeliveryDTimeUTC { get; set; } // Thời gian giao xe dự kiến

        public object ActualDeliveryDTimeUTC { get; set; } // Thời gian giao xe thực tế

        public object Km { get; set; } // Km

        public object ReminderMaintanceDateUTC { get; set; } // Ngày nhắc bảo dưỡng

        public object ReminderMaintanceKm { get; set; } // Km nhắc bảo dưỡng

        public object WorkDoneSoon { get; set; } // Nội dung công việc

        public object TermsOfRepair { get; set; } // Lưu ý báo giá

        public object AppNoSys { get; set; } // Số lịch hẹn

        public object TotalValService { get; set; } // Tổng giá trị công việc trước chiết khấu và VAT

        public object TotalValBeforeVATService { get; set; } // Tổng giá trị công việc trước VAT // C7

        public object TotalValVATService { get; set; } // Tổng giá trị VAT của coogn việc // C8

        public object TotalValAfterVATService { get; set; } // Tổng giá trị sau VAT của Công việc // C6

        public object TotalValDisAfterVATService { get; set; } // Tổng giá trị giảm giá sau VAT của công việc

        public object TotalValPart { get; set; } // Tổng giá trị Part trước chiết khấu và trước VAT

        public object TotalValBeforeVATPart { get; set; } // Tổng giá trị part trước VAT // P8

        public object TotalValVATPart { get; set; } // Tổng tiền VAT Part // P9

        public object TotalValAfterVATPart { get; set; } // Tổng tiền sau VAT Part // P10

        public object TotalValDisAfterVATPart { get; set; } // Tổng giảm giá sau VAT của Part

        public object TotalValRO { get; set; } // Tổng giá trị báo giá trước VAT và trước chiết khấu

        public object TotalValBeforeVATRO { get; set; } // Tổng giá trị báo giá trước VAT // T1

        public object TotalValVATRO { get; set; } // Tổng giá trị VAT của báo giá // T2

        public object TotalValAfterVATRO { get; set; } // Tổng giá trị sau VAT của Báo giá // T4

        public object TotalValDisAfterVATRO { get; set; } // Tổng giảm giá của báo giá // T3

        public object ValVourcher { get; set; } // giá trị vourcher // T7

        public object ValPmtFromCard { get; set; } // Thanh toán từ thẻ // T8

        public object TotalValEnd { get; set; } // Tổng giá trị cuối cùng // T6

        public object TotalValPmt { get; set; } // Giá trị thanh toán // T9

        public object TotalValCusROAfterVAT { get; set; } // Tổng tiền khách hàng // F1

        public object TotalValInsROAfterVAT { get; set; } // Tổng bảo hiểm sau thuế // F2

        public object TotalValWarrantyROAfterVAT { get; set; } // Tổng bảo hành sau thuế // F3

        public object TotalValLocalROAfterVAT { get; set; } // Tổng nội bô sau thuế // F4

        public object TotalValDebit { get; set; } // Khách hàng nợ // T10

        public object TotalValCusPmt { get; set; } // Khách hàng thanh toán // T11

        public object LUDTimeUTC { get; set; } 

        public object LUBy { get; set; }

        public object CreateDTimeUTC { get; set; } // Thời gian tạo báo giá

        public object CreateBy { get; set; }

        public object WaitDTimeUTC { get; set; } // Thời gian tạo lệnh

        public object WaitBy { get; set; }

        public object InGarageDTimeUTC { get; set; } // Thời gian vào xưởng

        public object InGarageBy { get; set; }

        public object RepairedDTimeUTC { get; set; } // Thời gian sửa chữa xong

        public object RepairedBy { get; set; }

        public object CheckInDTimeUTC { get; set; } // Thời gian vào xưởng

        public object CheckInBy { get; set; }

        public object StartDTimeUTC { get; set; }

        public object StartBy { get; set; }

        public object CheckEndDTimeUTC { get; set; } // Thời điểm kiểm tra cuối

        public object CheckEndBy { get; set; }

        public object PaidDTimeUTC { get; set; } // Thời điểm thanh toán

        public object FinishDTimeUTC { get; set; } // Thời điểm kết thúc

        public object FinishBy { get; set; }

        public object CancelDTimeUTC { get; set; } // Thời điểm hủy lệnh

        public object CancelBy { get; set; }

        public object ROStatus { get; set; } // Trạng thái báo giá

        public object RepairStatus { get; set; } // Trạng thái lệnh sửa chữa

        public object FlagStop { get; set; } // Trạng thái dừng

        public object FlagService { get; set; }

        public object FlagServiceDtl { get; set; }

        public object FlagRORepair { get; set; } // đã tạo lệnh hay chưa ?? 1 đã tạo, 0 chưa tạo

        public object Remark { get; set; }

        public object LogLUDTime { get; set; }

        public object LogLUBy { get; set; }

        public object PaymentMethodCode { get; set; }
        //////

        public object CreateName { get; set; } // Tên CVDV

        public object BrandName { get; set; } // Tên Brand

        public object IdxReasonStop { get; set; } // IdxReasonStop

        public object cus_TotalValRemainDebit { get; set; } // còn nợ khách hàng

        public object ins_TotalValRemainDebit { get; set; } // còn nợ bảo hiểm

        public object warranty_TotalValRemainDebit { get; set; } // Còn nợ bảo hành

        public object TotalValPaymentedRO { get; set; } // Tổng đã thanh toán

        public object mcus_CustomerCode { get; set; }

        public object mcus_CustomerAvatarPath { get; set; }

        public object mcus_CustomerAvatarName { get; set; }
    }
}
