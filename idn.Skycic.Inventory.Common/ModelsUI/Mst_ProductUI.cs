using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Mst_ProductUI: Mst_Product
    {
        // Bổ sung thêm các thông tin tồn kho
        public object TotalQtyTotalOK { get; set; } // Tổng tồn kho ở các vị trí _Dùng trong kiểm kê
        public object lstInvCode { get; set; } // Danh sách kho _Dùng trong kiểm kê

        public object QtyTotalOK { get; set; }// Số lượng tồn kho
        public object InvCodeSuggest { get; set; } // Vị trí tồn lớn nhất hoặc tồn gần nhất
        public object InvCode { get; set; } // Vị trí xuất
        public object SellPrice { get; set; } // Giá bán từ master sản phẩm
        public object SellOrder { get; set; } // Giá bán từ đơn hàng
        public object DiscountPrice { get; set; } // Giảm giá

        public object UPInv { get; set; } // Giá vốn
        public object UPIn { get; set; }
        public object UPReturnSup { get; set; }
        public object ValReturnSup { get; set; }

        public object UPOut { get; set; }
        public object UPCusReturn { get; set; }
        public object ValUPReturn { get; set; }
        public object UPOutDesc { get; set; }
        public object ValOutAfterDesc { get; set; }


        public object InvCodeInActual { get; set; }
        public object Qty { get; set; }
        public object ValOUTAfterDesc { get; set; }
        public object TotalValInv { get; set; }

        public object FlagLo { get; set; }
        //public object FlagSerial { get; set; } // Serial dùng theo Product
        public object FlagCombo { get; set; }

        public object CountPrdBase { get; set; }


        public object InvBUPattern { get; set; }

        public List<Mst_Product> LstPrdBase { get; set; }
        public List<Mst_ProductUI> LstPrdBaseUI { get; set; }

        public List<Mst_ProductUI> lstUnitCodeUIByProduct { get; set; }

        public List<Mst_Product> LstPrdLeve2 { get; set; }
        public List<Prd_Attribute> LstAttributeBase { get; set; }
        public List<Prd_Attribute> LstAttributeLevel2 { get; set; }

        public List<Mst_Product> Lst_Mst_ProductBase { get; set; }

        public Mst_ProductUI()
        {
            FlagLo = "0";
            FlagSerial = "0";
            FlagCombo = "0";
            
        }
    }

    public class Prd_AttributeUI : Prd_Attribute
    {
        public object AttributeName { get; set; }
    }

    public class Mst_Product_Import : Mst_Product
    {
        public List<Prd_AttributeUI> Lst_Prd_AttributeUI { get; set; }
        public List<Mst_ProductImages> Lst_Mst_ProductImages { get; set; }
        public List<Mst_ProductFiles> Lst_Mst_ProductFiles { get; set; }
        public List<Prd_BOM> Lst_Prd_BOM { get; set; }
    }

    public class Mst_ProductUI_Create : Mst_Product
    {
        public List<Prd_Attribute> Lst_Prd_Attribute { get; set; }
    }

    public class RQ_Mst_ProductUI : RQ_Mst_Product
    {
        public Mst_Product Mst_Product { get; set; }
        public List<Mst_ProductUI_Create> Lst_Mst_ProductUI_Create { get; set; } // ở màn hình update sẽ là list sản phẩm con (trường hợp thêm đơn vị khác đơn vị cơ bản)
        public List<Mst_Product> Lst_Unit_Delete { get; set; }
        public List<Mst_Product> Lst_Unit { get; set; }
        public List<Prd_Attribute> Lst_Prd_Attribute_Delete { get; set; } // danh sách thuộc tính bị xóa
        public List<Prd_Attribute> Lst_Prd_Attribute_New { get; set; } // danh sách thuộc tính mới

        public string FlagRedirect { get; set; } // 1: Reload page; 0: Redirect page
    }

    public class Mst_Product_ImportExcel
    {
        public Mst_Product Mst_Product { get; set; } // lưu Root hoặc Base
        public List<Mst_Product_Import> Lst_Mst_Product { get; set; }

    }
}
