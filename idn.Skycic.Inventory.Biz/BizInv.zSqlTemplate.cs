using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Xml;
using System.Linq;
using System.Threading;
//using System.Xml.Linq;

using CmUtils = CommonUtils;
using TDAL = EzDAL.MyDB;
using TDALUtils = EzDAL.Utils;
using TConst = idn.Skycic.Inventory.Constants;
using TUtils = idn.Skycic.Inventory.Utils;
using TError = idn.Skycic.Inventory.Errors;
using TJson = Newtonsoft.Json;

using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.BizService.Services;
using inos.common.Constants;
using inos.common.Model;
using inos.common.Service;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // Report:
        public class SqlTemplate_Mst_Product
        {
            public static string zzB_tbl_Mst_Product_Filter_ProductCode_zzE(
                object objProductCode
                ////
                )
            {
                string strSql = "-- Nothing";
                string strProductCode = TUtils.CUtils.StdParam(objProductCode);
                /////
                if (!string.IsNullOrEmpty(strProductCode))
                {
                    strSql = CmUtils.StringUtils.Replace(@"
                            --- #tbl_Mst_Product_Filter:
                            select distinct
	                            mp.OrgID
	                            , mp.ProductCode 
	                            , mp.ProductCodeBase
                            into #tbl_Mst_Product_Filter
                            from Mst_Product mp --//[mylock]
                                inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                    on mp.OrgID = t_MstNNT_View.OrgID
                            where(1=1)
	                            and (mp.ProductCode = '@strProductCode')
                            ;
					    "
                        , "@strProductCode", strProductCode
                        ////
                        );
                }
                else
                {
                    strSql = CmUtils.StringUtils.Replace(@"
                            ----- #tbl_Inv_InventoryBalance_Filter:
                            select distinct
	                            t.OrgID
	                            , t.ProductCode
	                            , t.ProductCode ProductCodeBase
                            into #tbl_Mst_Product_Filter
                            from Inv_InventoryBalance t --//[mylock]
	                            inner join #tbl_Mst_Inventory_Filter f --//[mylock]
		                            on t.OrgID = f.OrgID
			                            and t.InvCode = f.InvCode 
                            where(1=1)
	                            and (t.ProductCode = '@strProductCode' or '@strProductCode' = '')
                            ;
					    "
                        , "@strProductCode", objProductCode
                        ////
                        );

                }
                return strSql;
            }
        }
        #endregion 
    }
}
