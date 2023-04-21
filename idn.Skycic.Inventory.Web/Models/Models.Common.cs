using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Web.Models
{
    public class Mst_DealerUI : Mst_Dealer
    {
        public string CreateDTimeFrom { get; set; }
        public string CreateDTimeTo { get; set; }
    }

    public class Invoice_InvoiceInputUI : Invoice_InvoiceInput
    {
        public string InvoiceDateUTCFrom { get; set; }
        public string InvoiceDateUTCTo { get; set; }

        public string CreateDTimeUTCFrom { get; set; }
        public string CreateDTimeUTCTo { get; set; }
    }

    public class ObjectInput
    {
        public string Id { get; set; } // id 
        public string Name { get; set; } // name
        public string Class { get; set; } // class css
        public string Style { get; set; } // style nếu có
        public string Events { get; set; } // sự kiện của input
        public string Attributes { get; set; } // Attributes nếu có
        public string Type { get; set; } //Type của input: text, hidden, select, textarea,...
        public string Value { get; set; } // giá trị gán cho input
        public string DataType { get; set; } // kiểu dữ liệu hiển thị: string, int, double, bool, ... (hiện giờ để format text, number hoặc datetime)
    }

    public static class Predicates
    {
        public static bool IsNull<T>(T value) where T : class
        {
            return value == null;
        }

        public static bool IsNotNull<T>(T value) where T : class
        {
            return value != null;
        }

        public static bool IsNull<T>(T? nullableValue) where T : struct
        {
            return !nullableValue.HasValue;
        }

        public static bool IsNotNull<T>(T? nullableValue) where T : struct
        {
            return nullableValue.HasValue;
        }

        public static bool HasValue<T>(T? nullableValue) where T : struct
        {
            return nullableValue.HasValue;
        }

        public static bool HasNoValue<T>(T? nullableValue) where T : struct
        {
            return !nullableValue.HasValue;
        }
    }
}