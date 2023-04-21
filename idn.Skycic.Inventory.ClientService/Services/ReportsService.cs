using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class ReportsService : ClientServiceBase<Rpt_InvFInventoryInFGSum>
    {
        public static ReportsService Instance
        {
            get
            {
                return GetInstance<ReportsService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Report";
            }
        }
        public RT_Rpt_InvFInventoryInFGSum WA_Rpt_InvFInventoryInFGSum(RQ_Rpt_InvFInventoryInFGSum objRQ_Rpt_InvFInventoryInFGSum, string addressAPIs)
        {
            var result = PostData<RT_Rpt_InvFInventoryInFGSum, RQ_Rpt_InvFInventoryInFGSum>("Report", "WA_Rpt_InvFInventoryInFGSum", new { }, objRQ_Rpt_InvFInventoryInFGSum, addressAPIs);
            return result;
        }
        public RT_Rpt_InvFInventoryOutFGSum WA_Rpt_InvFInventoryOutFGSum(RQ_Rpt_InvFInventoryOutFGSum objRQ_Rpt_InvFInventoryOutFGSum, string addressAPIs)
        {
            var result = PostData<RT_Rpt_InvFInventoryOutFGSum, RQ_Rpt_InvFInventoryOutFGSum>("Report", "WA_Rpt_InvFInventoryOutFGSum", new { }, objRQ_Rpt_InvFInventoryOutFGSum, addressAPIs);
            return result;
        }
        public RT_Rpt_InvInventoryBalanceMonth WA_Rpt_InvInventoryBalanceMonth(RQ_Rpt_InvInventoryBalanceMonth objRQ_Rpt_InvInventoryBalanceMonth, string addressAPIs)
        {
            var result = PostData<RT_Rpt_InvInventoryBalanceMonth, RQ_Rpt_InvInventoryBalanceMonth>("Report", "WA_Rpt_InvInventoryBalanceMonth", new { }, objRQ_Rpt_InvInventoryBalanceMonth, addressAPIs);
            return result;
        }

        public RT_Rpt_Inv_InvBalance_LastUpdInvByProduct WA_Rpt_Inv_InvBalance_LastUpdInvByProduct(RQ_Rpt_Inv_InvBalance_LastUpdInvByProduct objRQ_Rpt_Inv_InvBalance_LastUpdInvByProduct, string addressAPIs)
        {
            var result = PostData<RT_Rpt_Inv_InvBalance_LastUpdInvByProduct, RQ_Rpt_Inv_InvBalance_LastUpdInvByProduct>("Report", "WA_Rpt_Inv_InvBalance_LastUpdInvByProduct", new { }, objRQ_Rpt_Inv_InvBalance_LastUpdInvByProduct, addressAPIs);
            return result;
        }

        public RT_Rpt_Summary_In_Out WA_Rpt_Summary_In_Out(RQ_Rpt_Summary_In_Out objRQ_Rpt_Summary_In_Out, string addressAPIs)
        {
            var result = PostData<RT_Rpt_Summary_In_Out, RQ_Rpt_Summary_In_Out>("Report", "WA_Rpt_Summary_In_Out", new { }, objRQ_Rpt_Summary_In_Out, addressAPIs);
            return result;
        }

        public RT_Rpt_Summary_QtyInvByPeriod WA_Rpt_Summary_QtyInvByPeriod(RQ_Rpt_Summary_QtyInvByPeriod objRQ_Rpt_Summary_QtyInvByPeriod, string addressAPIs)
        {
            var result = PostData<RT_Rpt_Summary_QtyInvByPeriod, RQ_Rpt_Summary_QtyInvByPeriod>("Report", "WA_Rpt_Summary_QtyInvByPeriod", new { }, objRQ_Rpt_Summary_QtyInvByPeriod, addressAPIs);
            return result;
        }

        public RT_Rpt_Summary_InAndReturnSup WA_Rpt_Summary_InAndReturnSup(RQ_Rpt_Summary_InAndReturnSup objRQ_Rpt_Summary_InAndReturnSup, string addressAPIs)
        {
            var result = PostData<RT_Rpt_Summary_InAndReturnSup, RQ_Rpt_Summary_InAndReturnSup>("Report", "WA_Rpt_Summary_InAndReturnSup", new { }, objRQ_Rpt_Summary_InAndReturnSup, addressAPIs);
            return result;
        }

        public RT_Rpt_Summary_In_Out_Pivot WA_Rpt_Summary_In_Out_Pivot(RQ_Rpt_Summary_In_Out_Pivot objRQ_Rpt_Summary_In_Out_Pivot, string addressAPIs)
        {
            var result = PostData<RT_Rpt_Summary_In_Out_Pivot, RQ_Rpt_Summary_In_Out_Pivot>(ApiControllerName, "WA_Rpt_Summary_In_Out_Pivot", new { }, objRQ_Rpt_Summary_In_Out_Pivot, addressAPIs);
            return result;
        }

        public RT_Rpt_Summary_In_Out_Sup_Pivot WA_Rpt_Summary_In_Out_Sup_Pivot(RQ_Rpt_Summary_In_Out_Sup_Pivot objRQ_Rpt_Summary_In_Out_Sup_Pivot, string addressAPIs)
        {
            var result = PostData<RT_Rpt_Summary_In_Out_Sup_Pivot, RQ_Rpt_Summary_In_Out_Sup_Pivot>(ApiControllerName, "WA_Rpt_Summary_In_Out_Sup_Pivot", new { }, objRQ_Rpt_Summary_In_Out_Sup_Pivot, addressAPIs);
            return result;
        }

        public RT_Rpt_InvBalLot_MaxExpiredDateByInv WA_Rpt_InvBalLot_MaxExpiredDateByInv(RQ_Rpt_InvBalLot_MaxExpiredDateByInv objRQ_Rpt_InvBalLot_MaxExpiredDateByInv, string addressAPIs)
        {
            var result = PostData<RT_Rpt_InvBalLot_MaxExpiredDateByInv, RQ_Rpt_InvBalLot_MaxExpiredDateByInv>(ApiControllerName, "WA_Rpt_InvBalLot_MaxExpiredDateByInv", new { }, objRQ_Rpt_InvBalLot_MaxExpiredDateByInv, addressAPIs);
            return result;
        }
        //2021-05-22: Bản đồ lệnh giao hàng

        public RT_Rpt_MapDeliveryOrder_ByInvFIOut WA_Rpt_MapDeliveryOrder_ByInvFIOut(RQ_Rpt_MapDeliveryOrder_ByInvFIOut objRQ_Rpt_MapDeliveryOrder_ByInvFIOut, string addressAPIs)
        {
            var result = PostData<RT_Rpt_MapDeliveryOrder_ByInvFIOut, RQ_Rpt_MapDeliveryOrder_ByInvFIOut>(ApiControllerName, "WA_Rpt_MapDeliveryOrder_ByInvFIOut", new { }, objRQ_Rpt_MapDeliveryOrder_ByInvFIOut, addressAPIs);
            return result;
        }

        public RT_Rpt_Inv_InventoryBalance_StorageTime WA_Rpt_Inv_InventoryBalance_StorageTime(RQ_Rpt_Inv_InventoryBalance_StorageTime objRQ_Rpt_Inv_InventoryBalance_StorageTime, string addressAPIs)
        {
            var result = PostData<RT_Rpt_Inv_InventoryBalance_StorageTime, RQ_Rpt_Inv_InventoryBalance_StorageTime>(ApiControllerName, "WA_Rpt_Inv_InventoryBalance_StorageTime", new { }, objRQ_Rpt_Inv_InventoryBalance_StorageTime, addressAPIs);
            return result;
        }

        public RT_Rpt_Inv_InventoryBalance_Minimum WA_Rpt_Inv_InventoryBalance_Minimum(RQ_Rpt_Inv_InventoryBalance_Minimum objRQ_Rpt_Inv_InventoryBalance_Minimum, string addressAPIs)
        {
            var result = PostData<RT_Rpt_Inv_InventoryBalance_Minimum, RQ_Rpt_Inv_InventoryBalance_Minimum>(ApiControllerName, "WA_Rpt_Inv_InventoryBalance_Minimum", new { }, objRQ_Rpt_Inv_InventoryBalance_Minimum, addressAPIs);
            return result;
        }
    }
}
