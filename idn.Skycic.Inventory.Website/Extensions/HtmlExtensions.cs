using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI;

namespace idn.Skycic.Inventory.Website.Extensions
{
    public static class HtmlExtensions
    {
        public static string BuildBreadcrumbNavigation(this HtmlHelper helper)
        {
            // optional condition: I didn't wanted it to show on home and account controller
            if (helper.ViewContext.RouteData.Values["controller"].ToString() == "Home" ||
                helper.ViewContext.RouteData.Values["controller"].ToString() == "Account")
            {
                return string.Empty;
            }

            StringBuilder breadcrumb = new StringBuilder("<ol class='breadcrumb col-12'>");
            if (!CUtils.IsNullOrEmpty(helper.ViewContext.RouteData.Values["action"]))
            {
                var action = CUtils.StrValue(helper.ViewContext.RouteData.Values["action"]);
                var controller = CUtils.StrValue(helper.ViewContext.RouteData.Values["controller"]);
                if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Dashboard.ToLower().Trim()) && action.ToLower().Trim().Equals("index"))
                {
                    breadcrumb.Append("<li>").Append(Breadcrumb_Name.Dashboard.HtmlItemString("menu")).Append("</li>");
                }
                else
                {
                    breadcrumb.Append("<li>").Append(helper.ActionLink(Breadcrumb_Name.Dashboard.HtmlItemString("menu"), "Index", "Dashboard").ToHtmlString()).Append("</li>");
                    var genBreadcrumbItem = GenBreadcrumbItem(helper);
                    breadcrumb.Append(genBreadcrumbItem);
                }

            }

            var strBreadcrumb = breadcrumb.Append("</ol>").ToString();
            return strBreadcrumb;
        }

        public static string GenBreadcrumbItem(HtmlHelper helper)
        {
            string menuCationsMng = "";
            string menuCations = "";
            var action = CUtils.StrValue(helper.ViewContext.RouteData.Values["action"]).ToLower().Trim();
            var controller = CUtils.StrValue(helper.ViewContext.RouteData.Values["controller"]).ToLower().Trim();
            StringBuilder breadcrumb = new StringBuilder();
            // Trả hàng NCC
            if (controller.ToLower().Trim().Equals(Breadcrumb_Code.InvFInventoryReturnSup.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.InvFInventoryReturnSup.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if(action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
            }
            // KH trả hàng
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.InvF_InventoryCusReturn.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.InvF_InventoryCusReturn.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
            }
            // Nhập kho
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.InvF_InventoryIn.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.InvF_InventoryIn.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
            }
            // Xuất kho
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.InvF_InventoryOut.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.InvF_InventoryOut.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
                else if (action.Equals("ExportCross".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.ExportCross.HtmlItemString("menu");
                }                
            }
            // Điều chuyển
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.InvF_MoveOrd.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.InvF_MoveOrd.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
            }
            // Kiểm kê
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.InvFInvAudit.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.InvFInvAudit.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
                else if (action.Equals("Action".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Action.HtmlItemString("menu");
                }
            }
            // Điều chuyển
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.InvF_MoveOrd.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.InvF_MoveOrd.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
            }
            // Điều chuyển
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.InvF_MoveOrd.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.InvF_MoveOrd.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
            }
            //vùng thị trường
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_Area.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_Area.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Mst_Area_Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Mst_Area_Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Mst_Area_Detail.HtmlItemString("menu");
                }
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_Customer.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_Customer.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_Product.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_Product.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Report.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Report.HtmlItemString("menu");
                if (action.Equals(Breadcrumb_Code.Rpt_Inv_InventoryBalance.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_Inv_InventoryBalance.HtmlItemString("menu");
                }
                if (action.Equals(Breadcrumb_Code.Rpt_Inv_InventoryBalance_Extend.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_Inv_InventoryBalance_Extend.HtmlItemString("menu");
                }
                if (action.Equals(Breadcrumb_Code.Rpt_InvF_WarehouseCard.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_InvF_WarehouseCard.HtmlItemString("menu");
                }
                if (action.Equals(Breadcrumb_Code.Rpt_Inventory_In_Out_Inv.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_Inventory_In_Out_Inv.HtmlItemString("menu");
                }
                if (action.Equals(Breadcrumb_Code.Rpt_Summary_In_Out.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_Summary_In_Out.HtmlItemString("menu");
                }
                if (action.Equals(Breadcrumb_Code.Rpt_Summary_QtyInvByPeriod.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_Summary_QtyInvByPeriod.HtmlItemString("menu");
                }
                if (action.Equals(Breadcrumb_Code.Rpt_Summary_InAndReturnSup.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_Summary_InAndReturnSup.HtmlItemString("menu");
                }
                if (action.Equals(Breadcrumb_Code.Rpt_Summary_In_Pivot.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_Summary_In_Pivot.HtmlItemString("menu");
                }
                if (action.Equals(Breadcrumb_Code.Rpt_Summary_In_Out_Sup_Pivot.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_Summary_In_Out_Sup_Pivot.HtmlItemString("menu");
                }
                if (action.Equals(Breadcrumb_Code.Rpt_InvBalLot_MaxExpiredDateByInv.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_InvBalLot_MaxExpiredDateByInv.HtmlItemString("menu");
                }
                if (action.Equals(Breadcrumb_Code.Rpt_MapDeliveryOrder_ByInvFIOut.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_MapDeliveryOrder_ByInvFIOut.HtmlItemString("menu");
                }
                if (action.Equals(Breadcrumb_Code.Rpt_Inv_InventoryBalance_StorageTime.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_Inv_InventoryBalance_StorageTime.HtmlItemString("menu");
                }
                if (action.Equals(Breadcrumb_Code.Rpt_Inv_InventoryBalance_Minimum.ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Rpt_Inv_InventoryBalance_Minimum.HtmlItemString("menu");
                }
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Map_UserInNotifyType.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Map_UserInNotifyType.HtmlItemString("menu");
            }
            //master quản trị
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_Inventory.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_Inventory.HtmlItemString("menu");
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_Sys_Config.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_Sys_Config.HtmlItemString("menu");
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_InventoryLevelType.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_InventoryLevelType.HtmlItemString("menu");
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_InventoryType.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_InventoryType.HtmlItemString("menu");
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_InvInType.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_InvInType.HtmlItemString("menu");
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_InvOutType.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_InvOutType.HtmlItemString("menu");
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_UserMapInventory.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_UserMapInventory.HtmlItemString("menu");
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.InvFTempPrint.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.InvFTempPrint.HtmlItemString("menu");
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Product_CustomField.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Product_CustomField.HtmlItemString("menu");
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_ColumnConfigGroup.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_ColumnConfigGroup.HtmlItemString("menu");
                if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Mst_ColumnConfigGroup_Update.HtmlItemString("menu");
                }
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Sys_User.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Sys_User.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_Organization.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_Organization.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Sys_Group.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Sys_Group.HtmlItemString("menu");
                if (action.Equals("Create".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Create.HtmlItemString("menu");
                }
                else if (action.Equals("Update".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Update.HtmlItemString("menu");
                }
                else if (action.Equals("Detail".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Detail.HtmlItemString("menu");
                }
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Notification.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Notification.HtmlItemString("menu");
                if (action.Equals("Setting".ToLower().Trim()))
                {
                    menuCations = Breadcrumb_Name.Notification_Setting.HtmlItemString("menu");
                }
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_Productgroupsub.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_Productgroupsub.HtmlItemString("menu");
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_Customergroup.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_Customergroup.HtmlItemString("menu");
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.Mst_Customersource.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.Mst_Customersource.HtmlItemString("menu");
            }
            else if (controller.ToLower().Trim().Equals(Breadcrumb_Code.MstBrand.ToLower().Trim()))
            {
                menuCationsMng = Breadcrumb_Name.MstBrand.HtmlItemString("menu");
            }
            else
            {
                menuCationsMng = helper.ViewContext.RouteData.Values["controller"].ToString().Titleize();
            }

            if (controller.Equals("Report".ToLower().Trim()))
            {
                breadcrumb.Append("<li>");
                breadcrumb.Append(menuCations);
                breadcrumb.Append("</li>");
                return breadcrumb.ToString();
            }

            if (action.Equals("Index".ToLower().Trim()))
            {
                breadcrumb.Append("<li>");
                breadcrumb.Append(menuCationsMng);
                breadcrumb.Append("</li>");
            }
            else
            {
                breadcrumb.Append("<li>");
                breadcrumb.Append(helper.ActionLink(
                    menuCationsMng,
                    "Index",
                    controller
                    )
                );
                breadcrumb.Append("</li>");
                if (CUtils.IsNullOrEmpty(menuCations))
                {
                    menuCations = action;
                }
                breadcrumb.Append("<li>");
                breadcrumb.Append(menuCations);
                breadcrumb.Append("</li>");
            }
            return breadcrumb.ToString();
        }

        public static string BreadcrumbName(string breadcrumbcode)
        {
            var breadcrumbName = "";
            switch (breadcrumbcode)
            {
                case Breadcrumb_Code.Dashboard:
                    {
                        breadcrumbName = Breadcrumb_Name.Dashboard.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.InvFInventoryReturnSup:
                    {
                        breadcrumbName = Breadcrumb_Name.InvFInventoryReturnSup.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.InvF_InventoryCusReturn:
                    {
                        breadcrumbName = Breadcrumb_Name.InvF_InventoryCusReturn.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.InvF_InventoryIn:
                    {
                        breadcrumbName = Breadcrumb_Name.InvF_InventoryIn.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.InvF_InventoryOut:
                    {
                        breadcrumbName = Breadcrumb_Name.InvF_InventoryOut.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.InvF_MoveOrd:
                    {
                        breadcrumbName = Breadcrumb_Name.InvF_MoveOrd.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.InvFInvAudit:
                    {
                        breadcrumbName = Breadcrumb_Name.InvFInvAudit.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.Mst_Customer:
                    {
                        breadcrumbName = Breadcrumb_Name.Mst_Customer.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.Mst_Product:
                    {
                        breadcrumbName = Breadcrumb_Name.Mst_Product.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.Rpt_Inv_InventoryBalance:
                    {
                        breadcrumbName = Breadcrumb_Name.Rpt_Inv_InventoryBalance.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.Rpt_InvF_WarehouseCard:
                    {
                        breadcrumbName = Breadcrumb_Name.Rpt_InvF_WarehouseCard.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.Rpt_Inventory_In_Out_Inv:
                    {
                        breadcrumbName = Breadcrumb_Name.Rpt_Inventory_In_Out_Inv.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.Rpt_Summary_In_Out:
                    {
                        breadcrumbName = Breadcrumb_Name.Rpt_Summary_In_Out.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.Rpt_Summary_QtyInvByPeriod:
                    {
                        breadcrumbName = Breadcrumb_Name.Rpt_Summary_QtyInvByPeriod.HtmlItemString("menu");
                        break;
                    }
                case Breadcrumb_Code.Rpt_Summary_InAndReturnSup:
                    {
                        breadcrumbName = Breadcrumb_Name.Rpt_Summary_InAndReturnSup.HtmlItemString("menu");
                        break;
                    }
            }
            return breadcrumbName;
        }

        public static string ResourceVersion = "";

        public static string GetModelAttemptedValue(this HtmlHelper htmlHelper, string key)
        {
            ModelState state;
            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out state))
            {
                return state.Value.AttemptedValue;
            }
            return null;
        }

        public static string EvalString(this HtmlHelper htmlHelper, string key)
        {
            return Convert.ToString(htmlHelper.ViewData.Eval(key), CultureInfo.CurrentCulture);
        }

        public static MvcHtmlString MetaDescription(this HtmlHelper htmlHelper, string content)
        {
            if (content != null)
            {
                TagBuilder builder = new TagBuilder("meta");
                builder.Attributes.Add("name", "description");
                content = Regex.Replace(content, "\r|\n", "");
                content = Regex.Replace(content, "(&nbsp;)+|(\t+)", " ");
                builder.Attributes.Add("content", SecurityElement.Escape(content));
                return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Hop-PC
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="selectList"></param>
        /// <param name="defaultValue"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = (int)Enum.Parse(typeof(TEnum), e.ToString()), Name = e.ToString() };

            return new SelectList(values, "Id", "Name", (int)Enum.Parse(typeof(TEnum), enumObj.ToString()));
        }

        public static MvcHtmlString DropDownListDefault(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object defaultValue, string defaultText)
        {
            List<SelectListItem> list = new List<SelectListItem>(selectList);
            SelectListItem item = new SelectListItem();
            item.Text = defaultText;
            item.Value = defaultValue.ToString();
            list.Insert(0, item);
            return htmlHelper.DropDownList(name, list);
        }
        public static MvcHtmlString DropDownListDefault(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object defaultValue, string defaultText, object htmAttribute = null)
        {
            List<SelectListItem> list = new List<SelectListItem>(selectList);
            SelectListItem item = new SelectListItem();
            item.Text = defaultText;
            item.Value = defaultValue.ToString();
            list.Insert(0, item);
            return htmlHelper.DropDownList(name, list, htmAttribute);
        }
        public static T GetStateValue<T>(this HtmlHelper htmlHelper, string key)
        {
            T value = default(T);
            ModelState state;
            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out state))
            {
                value = (T)state.Value.ConvertTo(typeof(T), null);
            }
            else
            {
                value = (T)htmlHelper.ViewData.Eval(key);
            }
            return value;
        }

        public static string ToTitleOnTable(this string value, int length)
        {
            string temp = "";
            if (value.Length > length)
            {
                temp = value.Substring(0, length) + "...";
            }
            else
            {
                temp = value;
            }
            return temp;
        }

        #region Link
        public static MvcHtmlString Link(this HtmlHelper htmlHelper, string url)
        {
            return htmlHelper.Link(url, null);
        }

        public static MvcHtmlString Link(this HtmlHelper htmlHelper, string url, object htmlAttributes)
        {
            return htmlHelper.Link(null, url, htmlAttributes);
        }

        public static MvcHtmlString Link(this HtmlHelper htmlHelper, string innerHtml, string url)
        {
            return htmlHelper.Link(innerHtml, url, null);
        }

        public static MvcHtmlString Link(this HtmlHelper htmlHelper, string innerHtml, string url, object htmlAttributes)
        {
            IDictionary<string, object> htmlAttributesDictionay = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            TagBuilder builder = new TagBuilder("a");
            builder.MergeAttributes<string, object>(htmlAttributesDictionay);
            if (innerHtml == null)
            {
                builder.InnerHtml = url.Replace("http://", "");
            }
            else
            {
                builder.InnerHtml = innerHtml;
            }
            builder.Attributes.Add("href", url);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }


        #endregion

        public static MvcHtmlString CheckBoxBit<T>(this HtmlHelper htmlHelper, string name, T value, T expectedBit) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return htmlHelper.CheckBox(name, (Convert.ToInt32(value) & Convert.ToInt32(expectedBit)) > 0, new { value = Convert.ToInt32(expectedBit) });
        }




        #region GetUrl
        /// <summary>
        /// Generates a url based on the routeValues
        /// </summary>
        public static string GetUrl(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
        {
            return UrlHelper.GenerateUrl(null, actionName, controllerName, new RouteValueDictionary(routeValues), htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, true);
        }
        #endregion


        #region Script, style sheets and images
        /// <summary>
        /// Script tag, enforces to be app relative
        /// </summary>
        public static MvcHtmlString Script(this HtmlHelper htmlHelper, string url)
        {
            ValidateApplicationUrl(url);
            if (url[0] == '~')
            {
                url = url.ToLower();
            }

            if (!string.IsNullOrEmpty(ResourceVersion))
            {
                if (url.Contains("?"))
                {
                    url = string.Format("{0}&rv={1}", url, ResourceVersion);
                }
                else
                {
                    url = string.Format("{0}?rv={1}", url, ResourceVersion);
                }
            }

            TagBuilder builder = new TagBuilder("script");
            builder.Attributes.Add("type", "text/javascript");
            builder.Attributes.Add("src", UrlHelper.GenerateContentUrl(url, htmlHelper.ViewContext.HttpContext));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }


        /// <summary>
        /// Add a link tag referencing the stylesheet, enforcing the url to be app relative.
        /// </summary>
        public static MvcHtmlString Stylesheet(this HtmlHelper htmlHelper, string url)
        {

            ValidateApplicationUrl(url);
            if (url[0] == '~')
            {
                url = url.ToLower();
            }

            if (!string.IsNullOrEmpty(ResourceVersion))
            {
                if (url.Contains("?"))
                {
                    url = string.Format("{0}&rv={1}", url, ResourceVersion);
                }
                else
                {
                    url = string.Format("{0}?rv={1}", url, ResourceVersion);
                }
            }

            TagBuilder builder = new TagBuilder("link");
            builder.Attributes.Add("type", "text/css");
            builder.Attributes.Add("rel", "stylesheet");
            builder.Attributes.Add("href", UrlHelper.GenerateContentUrl(url, htmlHelper.ViewContext.HttpContext));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Add a link tag referencing the stylesheet, enforcing the url to be app relative.
        /// </summary>
        public static MvcHtmlString Img(this HtmlHelper htmlHelper, string url, string alt)
        {
            ValidateApplicationUrl(url);
            if (url[0] == '~')
            {
                url = url.ToLower();
            }

            TagBuilder builder = new TagBuilder("img");
            builder.Attributes.Add("alt", alt);
            builder.Attributes.Add("src", UrlHelper.GenerateContentUrl(url, htmlHelper.ViewContext.HttpContext));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Validates that the url is absolute or starts with tilde (~) char.
        /// </summary>
        /// <param name="url"></param>
        private static void ValidateApplicationUrl(string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if ((!url.StartsWith("http://")) && (!url.StartsWith("https://")) && url[0] != '~')
            {
                throw new ArgumentException("Url must start tilde character '~' or be absolute.", "url");
            }
        }
        #endregion

        public static MvcHtmlString Nl2Br(this HtmlHelper htmlHelper, string text)
        {
            if (string.IsNullOrEmpty(text))
                return MvcHtmlString.Create(text);
            else
            {
                StringBuilder builder = new StringBuilder();
                string[] lines = text.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i > 0)
                        builder.Append("<br/>");
                    builder.Append(System.Web.HttpUtility.HtmlEncode(lines[i]));
                }
                return MvcHtmlString.Create(builder.ToString());
            }
        }


        #region VIews
        public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        {

            controller.ViewData.Model = model;
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static string RenderPartialViewToString(ControllerContext context, string viewName, object model, ViewDataDictionary viewdata)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                    ViewContext viewContext = new ViewContext(context, viewResult.View, viewdata, context.Controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    var strRender = sw.GetStringBuilder().ToString();
                    return strRender;
                }
            }
            catch (Exception ex)
            {
                var strEx = ex.ToString();
                return strEx;
            }
        }

        #endregion


        #region Maps
        public static ExampleConfigurator Configurator(this HtmlHelper instance, string title)
        {
            return new ExampleConfigurator(instance).Title(title);
        }
        #endregion

    }


    public class ExampleConfigurator : IDisposable
    {
        public const string CssClass = "configurator";

        private HtmlTextWriter writer;
        private HtmlHelper htmlHelper;
        private string title;
        private MvcForm form;

        public ExampleConfigurator(HtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
            this.writer = new HtmlTextWriter(htmlHelper.ViewContext.Writer);
        }

        public ExampleConfigurator Title(string title)
        {
            this.title = title;

            return this;
        }

        public ExampleConfigurator Begin()
        {
            this.writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);
            this.writer.RenderBeginTag(HtmlTextWriterTag.Div);

            this.writer.AddAttribute(HtmlTextWriterAttribute.Class, CssClass + "-legend");
            this.writer.RenderBeginTag(HtmlTextWriterTag.H3);
            this.writer.Write(this.title);
            this.writer.RenderEndTag();

            return this;
        }

        public ExampleConfigurator End()
        {
            this.writer.RenderEndTag();

            if (this.form != null)
            {
                this.form.EndForm();
            }

            return this;
        }

        public void Dispose()
        {
            this.End();
        }

        public ExampleConfigurator PostTo(string action, string controller)
        {
            form = htmlHelper.BeginForm(action, controller);
            return this;
        }


    }
}