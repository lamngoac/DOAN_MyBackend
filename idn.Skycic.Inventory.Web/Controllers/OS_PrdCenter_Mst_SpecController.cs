using idn.Skycic.Inventory.Common.Models;
using ProductCentrer = idn.Skycic.Inventory.Common.Models.ProductCentrer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class OS_PrdCenter_Mst_SpecController : AdminBaseController
    {
        // Chú ý: Xử lý không sử dụng OS_PrdCenter_Mst_Spec.SpecCode mà sử dụng: Mst_Spec.SpecCode
        // GET: OS_PrdCenter_Mst_Spec
        public ActionResult Index(string orgid = "", string brandcode = "", string modelcode = "", string speccode = "", string specname = "", string spectype1 = "", string spectype2 = "", string flagactive = "", string init = "init", int recordcount = 10, int page = 0)
        {
            init = "search"; // Không làm tìm kiếm
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            ViewBag.WaOrgID = waOrgID;
            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            ViewBag.IsParent = isparent;

            var pageInfo = new PageInfo<ProductCentrer.Mst_Spec>(0, recordcount)
            {
                DataList = new List<ProductCentrer.Mst_Spec>()
            };
            var itemCount = 0;
            var pageCount = 0;
            #region["Mst_NNT"]
            ////var objMst_NNT = new Mst_NNT()
            ////{
            ////    //OrgID = waOrgID,
            ////    FlagActive = FlagActive,
            ////};

            ////var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);
            ////var listMst_NNT = List_Mst_NNT(strWhereClauseMst_NNT);

            //var listMst_NNT = new List<Mst_NNT>();
            //var rqNNT = new RQ_Mst_NNT()
            //{
            //    Tid = GetNextTId(),
            //    WAUserCode = waUserCode,
            //    WAUserPassword = waUserPassword,
            //    GwUserCode = GwUserCode,
            //    GwPassword = GwPassword,
            //    Rt_Cols_Mst_NNT = "*",
            //    Ft_RecordStart = Ft_RecordStart,
            //    Ft_RecordCount = Ft_RecordCount,
            //    Ft_WhereClause = "Mst_NNT.FlagActive = 1 AND Mst_NNT.OrgID != 0"
            //};
            //var rtNNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(rqNNT, userState.AddressAPIs);
            //if (rtNNT != null) if (rtNNT.Lst_Mst_NNT != null) listMst_NNT = rtNNT.Lst_Mst_NNT;

            //ViewBag.List_Mst_NNT = listMst_NNT;
            #endregion

            #region["Model"]
            var objMst_Model = new ProductCentrer.Mst_Model()
            {
                //OrgID = waOrgID,
                FlagActive = FlagActive,
            };

            var strWhereClauseMst_Model = strWhereClause_Mst_Model(objMst_Model);
            var listMst_Model = List_Mst_Model(strWhereClauseMst_Model);
            ViewBag.ListOS_PrdCenter_Mst_Model = listMst_Model;
            #endregion
            #region["Mst_SpecType1"]
            var objMst_SpecType1 = new ProductCentrer.Mst_SpecType1()
            {
                //OrgID = waOrgID,
                FlagActive = FlagActive,
            };

            var strWhereClauseMst_SpecType1 = strWhereClause_Mst_SpecType1(objMst_SpecType1);

            var listOS_PrdCenter_Mst_SpecType1 = List_Mst_SpecType1(strWhereClauseMst_SpecType1);
            ViewBag.ListOS_PrdCenter_Mst_SpecType1 = listOS_PrdCenter_Mst_SpecType1;
            #endregion
            #region["Mst_SpecType2"]
            var objMst_SpecType2 = new ProductCentrer.Mst_SpecType2()
            {
                //OrgID = waOrgID,
                FlagActive = FlagActive,
            };

            var strWhereClauseMst_SpecType2 = strWhereClause_Mst_SpecType2(objMst_SpecType2);

            var listMst_SpecType2 = List_Mst_SpecType2(strWhereClauseMst_SpecType2);
            ViewBag.ListOS_PrdCenter_Mst_SpecType2 = listMst_SpecType2;
            #endregion
            #region["Brand"]
            var objMst_Brand = new ProductCentrer.Mst_Brand()
            {
                //OrgID = waOrgID,
                FlagActive = FlagActive,
            };

            var strWhereClauseMst_Brand = strWhereClause_Mst_Brand(objMst_Brand);

            var listMst_Brand = List_Mst_Brand(strWhereClauseMst_Brand);
            ViewBag.ListOS_PrdCenter_Mst_Brand = listMst_Brand;
            #endregion
            #region["Mst_SpecCustomField"]
            var objMst_SpecCustomField = new ProductCentrer.Mst_SpecCustomField()
            {
                FlagActive = FlagActive,
            };

            var strWhereClauseMst_SpecCustomField = strWhereClause_Mst_SpecCustomField(objMst_SpecCustomField);

            var list_Mst_SpecCustomField = List_Mst_SpecCustomField(strWhereClauseMst_SpecCustomField);
            ViewBag.List_OS_PrdCenter_Mst_SpecCustomField = list_Mst_SpecCustomField;
            #endregion

            if (init != "init")
            {
                #region["Search"]
                var objMst_SpecUI = new Mst_SpecUI()
                {
                    BrandCode = brandcode,
                    ModelCode = modelcode,
                    SpecCode = speccode,
                    SpecName = specname,
                    SpecType1 = spectype1,
                    SpecType2 = spectype2,
                    FlagActive = flagactive,
                    OrgID = orgid,
                };

                var strWhereClauseMst_Spec = strWhereClause_Mst_Spec(objMst_SpecUI);

                var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_WhereClause = strWhereClauseMst_Spec,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Spec
                    Rt_Cols_Mst_Spec = "*",
                    Rt_Cols_Mst_SpecFiles = "*",
                    Rt_Cols_Mst_SpecImage = "*",
                    Mst_Spec = null,
                    Lst_Mst_SpecFiles = null,
                    Lst_Mst_SpecImage = null,
                };

                var objRT_Mst_SpecCur = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                if (objRT_Mst_SpecCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_SpecCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_SpecCur != null && objRT_Mst_SpecCur.Lst_Mst_Spec != null && objRT_Mst_SpecCur.Lst_Mst_Spec.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_SpecCur.Lst_Mst_Spec);
                    pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            ViewBag.BrandCode = brandcode;
            ViewBag.ModelCode = modelcode;
            ViewBag.SpecCode = speccode;
            ViewBag.SpecName = specname;
            ViewBag.SpecType1 = spectype1;
            ViewBag.SpecType2 = spectype2;
            ViewBag.FlagActive = flagactive;
            ViewBag.OrgID = orgid;

            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();

            return View(pageInfo);
        }

        #region["Tạo mới Spec"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            ViewBag.IsParent = isparent;

            #region["Model"]
            //var objOS_PrdCenter_Mst_Model = new OS_PrdCenter_Mst_Model()
            //{
            //    FlagActive = FlagActive,
            //    OrgID = waOrgID,
            //};

            //var strWhereClauseMst_Model = strWhereClause_Mst_Model(objOS_PrdCenter_Mst_Model);
            //var listOS_PrdCenter_Mst_Model = List_Mst_Model(strWhereClauseMst_Model);
            //ViewBag.ListOS_PrdCenter_Mst_Model = listOS_PrdCenter_Mst_Model;
            #endregion
            #region["Mst_SpecType1"]
            var objMst_SpecType1 = new ProductCentrer.Mst_SpecType1()
            {
                OrgID = waOrgID,
                FlagActive = FlagActive,
            };

            var strWhereClauseMst_SpecType1 = strWhereClause_Mst_SpecType1(objMst_SpecType1);

            var listOS_PrdCenter_Mst_SpecType1 = List_Mst_SpecType1(strWhereClauseMst_SpecType1);
            ViewBag.ListOS_PrdCenter_Mst_SpecType1 = listOS_PrdCenter_Mst_SpecType1;
            #endregion
            #region["Mst_SpecType2"]
            //var objOS_PrdCenter_Mst_SpecType2 = new OS_PrdCenter_Mst_SpecType2()
            //{
            //    OrgID = waOrgID,
            //    FlagActive = FlagActive,
            //};

            //var strWhereClauseMst_SpecType2 = strWhereClause_Mst_SpecType2(objOS_PrdCenter_Mst_SpecType2);

            //var listOS_PrdCenter_Mst_SpecType2 = List_Mst_SpecType2(strWhereClauseMst_SpecType2);
            //ViewBag.ListOS_PrdCenter_Mst_SpecType2 = listOS_PrdCenter_Mst_SpecType2;
            #endregion
            #region["Mst_Unit"]
            var objMst_Unit = new ProductCentrer.Mst_Unit()
            {
                FlagActive = FlagActive,
            };
            var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);
            var list_Mst_Unit = List_Mst_Unit(strWhereClauseMst_Unit);
            ViewBag.ListOS_PrdCenter_Mst_Unit = list_Mst_Unit;
            #endregion
            #region["Mst_SpecCustomField"]
            var objMst_SpecCustomField = new ProductCentrer.Mst_SpecCustomField()
            {
                FlagActive = FlagActive,
            };
            
            var strWhereClauseMst_SpecCustomField = strWhereClause_Mst_SpecCustomField(objMst_SpecCustomField);

            var list_Mst_SpecCustomField = List_Mst_SpecCustomField(strWhereClauseMst_SpecCustomField);
            ViewBag.List_OS_PrdCenter_Mst_SpecCustomField = list_Mst_SpecCustomField;
            #endregion

            ViewBag.OrgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objRQ_Mst_SpecUIInput = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_Mst_SpecUI>(model);

                if (isparent)
                {
                    //objRQ_Mst_SpecUIInput.NetworkSpecCode = objRQ_Mst_SpecUIInput.SpecCode;
                }

                var listMst_SpecImage = new List<ProductCentrer.Mst_SpecImage>();
                var listMst_SpecFiles = new List<ProductCentrer.Mst_SpecFiles>();

                var listMoveFiles_Images = new List<MoveFiles>();
                if (objRQ_Mst_SpecUIInput != null)
                {
                    #region["Upload Image"]

                    if (objRQ_Mst_SpecUIInput.ListImages != null && objRQ_Mst_SpecUIInput.ListImages.Count > 0)
                    {
                        foreach (var item in objRQ_Mst_SpecUIInput.ListImages)
                        {
                            string folderUpload = @"UploadedFiles\SpecImages_Temp\" + waUserCode;
                            var fileName = item.Name;
                            // Upload file lên server
                            var objRQ_File = new ProductCentrer.RQ_File()
                            {
                                FlagIsDelete = null,
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                FuncType = null,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = null,
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                UtcOffset = userState.UtcOffset,
                                // RQ_File
                                folderUpload = folderUpload,
                                fileName = fileName,
                                uploadFileAsBase64String = item.Base64,
                            };
                            var result = OS_PrdCenter_FileService.Instance.WA_OS_UploadFile(objRQ_File, addressAPIs_ProductCentrer);
                            if (!CUtils.IsNullOrEmpty(result.Status) &&
                                result.Status.ToString().ToLower().Trim().Equals("true"))
                            {
                                // Lưu file tem trên Server Biz thành công

                                // Lấy đường dẫn file
                                var filePathServerTem = result.AppPath.ToString().Trim();
                                var filePathServer = filePathServerTem.Replace(@"SpecImages_Temp", @"SpecImages"); // đường dẫn sau khi move file
                                var index = filePathServer.LastIndexOf(@"\", System.StringComparison.Ordinal);
                                var folderUploadServer = "";

                                folderUploadServer = filePathServer.Substring(0, index);
                                index = folderUploadServer.IndexOf(@"\UploadedFiles\", System.StringComparison.Ordinal);
                                if (index == 0)
                                {
                                    folderUploadServer = folderUploadServer.Replace(@"\UploadedFiles\", @"UploadedFiles\");
                                }

                                var objMoveFiles = new MoveFiles()
                                {
                                    FolderUpload = folderUploadServer,
                                    SourceFileName = filePathServerTem,
                                    DestFileName = filePathServer,
                                };

                                listMoveFiles_Images.Add(objMoveFiles);

                                var objMst_SpecImage = new ProductCentrer.Mst_SpecImage()
                                {
                                    SpecImagePath = filePathServer,
                                    SpecImageName = fileName,
                                    SpecImageDesc = null,
                                    FlagPrimaryImage = item.FlagPrimaryImage,
                                };
                                listMst_SpecImage.Add(objMst_SpecImage);
                            }

                        }
                    }
                    #endregion

                    #region["Upload File"]
                    if (objRQ_Mst_SpecUIInput.ListFiles != null && objRQ_Mst_SpecUIInput.ListFiles.Count > 0)
                    {
                        foreach (var item in objRQ_Mst_SpecUIInput.ListFiles)
                        {
                            string folderUpload = @"UploadedFiles\SpecFiles_Temp\" + waUserCode;
                            var fileName = item.Name;
                            // Upload file lên server
                            var objRQ_File = new ProductCentrer.RQ_File()
                            {
                                FlagIsDelete = null,
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                FuncType = null,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = null,
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                UtcOffset = userState.UtcOffset,
                                // RQ_File
                                folderUpload = folderUpload,
                                fileName = fileName,
                                uploadFileAsBase64String = item.Base64,
                            };
                            var result = OS_PrdCenter_FileService.Instance.WA_OS_UploadFile(objRQ_File, addressAPIs_ProductCentrer);
                            if (!CUtils.IsNullOrEmpty(result.Status) &&
                                result.Status.ToString().ToLower().Trim().Equals("true"))
                            {
                                // Lưu file tem trên Server Biz thành công

                                // Lấy đường dẫn file
                                var filePathServerTem = result.AppPath.ToString().Trim();
                                var filePathServer = filePathServerTem.Replace(@"SpecFiles_Temp", @"SpecFiles"); // đường dẫn sau khi move file
                                var index = filePathServer.LastIndexOf(@"\", System.StringComparison.Ordinal);
                                var folderUploadServer = "";

                                folderUploadServer = filePathServer.Substring(0, index);
                                index = folderUploadServer.IndexOf(@"\UploadedFiles\", System.StringComparison.Ordinal);
                                if (index == 0)
                                {
                                    folderUploadServer = folderUploadServer.Replace(@"\UploadedFiles\", @"UploadedFiles\");
                                }

                                var objMoveFiles = new MoveFiles()
                                {
                                    FolderUpload = folderUploadServer,
                                    SourceFileName = filePathServerTem,
                                    DestFileName = filePathServer,
                                };

                                listMoveFiles_Images.Add(objMoveFiles);

                                var objMst_SpecFiles = new ProductCentrer.Mst_SpecFiles()
                                {
                                    SpecFilePath = filePathServer,
                                    SpecFileName = fileName,
                                    SpecFileDesc = null,
                                };
                                listMst_SpecFiles.Add(objMst_SpecFiles);
                            }

                        }
                    }
                    #endregion
                }



                var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec
                {
                    FlagIsDelete = null,
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Spec
                    Rt_Cols_Mst_Spec = null,
                    Mst_Spec = objRQ_Mst_SpecUIInput.Mst_Spec,
                    Lst_Mst_SpecImage = listMst_SpecImage,
                    Lst_Mst_SpecFiles = listMst_SpecFiles,
                };
                OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Add(objRQ_Mst_Spec, addressAPIs_ProductCentrer);

                // tạo Spec thành công => move file
                if (listMoveFiles_Images != null && listMoveFiles_Images.Count > 0)
                {
                    foreach (var item in listMoveFiles_Images)
                    {
                        if (!CUtils.IsNullOrEmpty(item.FolderUpload))
                        {
                            var objRQ_File = new ProductCentrer.RQ_File()
                            {
                                FlagIsDelete = null,
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                FuncType = null,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = null,
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                UtcOffset = userState.UtcOffset,
                                // RQ_File
                                folderUpload = item.FolderUpload,
                                sourceFileName = item.SourceFileName,
                                destFileName = item.DestFileName,
                            };
                            var result = OS_PrdCenter_FileService.Instance.WA_OS_MoveFile(objRQ_File, addressAPIs_ProductCentrer);

                        }
                    }
                }


                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới Spec thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }
        #endregion

        #region["Sửa Spec"]
        [HttpGet]
        public ActionResult Update(string speccode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            ViewBag.WaOrgID = waOrgID;
            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            ViewBag.IsParent = isparent;

            var domainCur = WA_OS_GetFileFromPath(GetNextTId(), waUserCode, waUserPassword);// "http://103.35.65.129:12908/idocNet.Test.ProductCenter.V10.WA/api/File";
            if (!CUtils.IsNullOrEmpty(speccode))
            {
                var objMst_SpecUI = new Mst_SpecUI()
                {
                    SpecCode = speccode,
                };

                var strWhereClauseMst_Spec = strWhereClause_Mst_SpecBySpecCode(objMst_SpecUI);

                var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_Spec,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Spec
                    Rt_Cols_Mst_Spec = "*",
                    Rt_Cols_Mst_SpecFiles = "*",
                    Rt_Cols_Mst_SpecImage = "*",
                    Mst_Spec = null,
                    Lst_Mst_SpecFiles = null,
                    Lst_Mst_SpecImage = null,
                };

                var objRT_Mst_SpecCur = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                if (objRT_Mst_SpecCur.Lst_Mst_Spec != null && objRT_Mst_SpecCur.Lst_Mst_Spec.Count > 0)
                {
                    #region["Model"]
                    //var objOS_PrdCenter_Mst_Model = new OS_PrdCenter_Mst_Model()
                    //{
                    //    FlagActive = FlagActive,
                    //};
                    //if (!isparent)
                    //{
                    //    objOS_PrdCenter_Mst_Model.OrgID = waOrgID;
                    //}
                    //var strWhereClauseMst_Model = strWhereClause_Mst_Model(objOS_PrdCenter_Mst_Model);
                    //var listOS_PrdCenter_Mst_Model = List_Mst_Model(strWhereClauseMst_Model);
                    //ViewBag.ListOS_PrdCenter_Mst_Model = listOS_PrdCenter_Mst_Model;
                    #endregion
                    #region["Mst_SpecType1"]
                    var objMst_SpecType1 = new ProductCentrer.Mst_SpecType1()
                    {
                        FlagActive = FlagActive,
                    };
                    if (!isparent)
                    {
                        objMst_SpecType1.OrgID = waOrgID;
                    }
                    var strWhereClauseMst_SpecType1 = strWhereClause_Mst_SpecType1(objMst_SpecType1);

                    var listMst_SpecType1 = List_Mst_SpecType1(strWhereClauseMst_SpecType1);
                    ViewBag.ListOS_PrdCenter_Mst_SpecType1 = listMst_SpecType1;
                    #endregion
                    #region["Mst_SpecType2"]
                    //var objOS_PrdCenter_Mst_SpecType2 = new OS_PrdCenter_Mst_SpecType2()
                    //{
                    //    FlagActive = FlagActive,
                    //};
                    //if (!isparent)
                    //{
                    //    objOS_PrdCenter_Mst_SpecType2.OrgID = waOrgID;
                    //}
                    //var strWhereClauseMst_SpecType2 = strWhereClause_Mst_SpecType2(objOS_PrdCenter_Mst_SpecType2);

                    //var listOS_PrdCenter_Mst_SpecType2 = List_Mst_SpecType2(strWhereClauseMst_SpecType2);
                    //ViewBag.ListOS_PrdCenter_Mst_SpecType2 = listOS_PrdCenter_Mst_SpecType2;
                    #endregion
                    #region["Mst_Unit"]
                    var objMst_Unit = new ProductCentrer.Mst_Unit()
                    {
                        FlagActive = FlagActive,
                    };

                    var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);

                    var list_Mst_Unit = List_Mst_Unit(strWhereClauseMst_Unit);
                    ViewBag.ListOS_PrdCenter_Mst_Unit = list_Mst_Unit;
                    #endregion
                    #region["Mst_SpecCustomField"]
                    var objMst_SpecCustomField = new ProductCentrer.Mst_SpecCustomField()
                    {
                        FlagActive = FlagActive,
                    };

                    var strWhereClauseMst_SpecCustomField = strWhereClause_Mst_SpecCustomField(objMst_SpecCustomField);

                    var list_Mst_SpecCustomField = List_Mst_SpecCustomField(strWhereClauseMst_SpecCustomField);
                    ViewBag.List_OS_PrdCenter_Mst_SpecCustomField = list_Mst_SpecCustomField;
                    #endregion
                    //Mst_SpecImage_File
                    var listMst_SpecFiles = new List<Mst_SpecImage_File>();
                    if (objRT_Mst_SpecCur.Lst_Mst_SpecFiles != null &&
                        objRT_Mst_SpecCur.Lst_Mst_SpecFiles.Count > 0)
                    {
                        var i = 0;
                        foreach (var item in objRT_Mst_SpecCur.Lst_Mst_SpecFiles)
                        {
                            var specFilePath = "";
                            var objMst_SpecImage_File = new Mst_SpecImage_File()
                            {
                                Id = i.ToString().Trim(),
                                Name = item.SpecFileName.ToString().Trim() ?? "",
                                Status = "O",
                            };
                            if (!CUtils.IsNullOrEmpty(item.SpecFilePath))
                            {
                                //objMst_SpecImage_File.SrcLocal = item.SpecFilePath.ToString().Trim();

                                specFilePath = domainCur + @"/" + item.SpecFilePath.ToString().Trim().Replace(@"\", @"/");
                                objMst_SpecImage_File.Src = specFilePath;
                            }


                            listMst_SpecFiles.Add(objMst_SpecImage_File);
                            i++;
                        }
                    }
                    var listMst_SpecImage = new List<Mst_SpecImage_File>();
                    if (objRT_Mst_SpecCur.Lst_Mst_SpecImage != null &&
                        objRT_Mst_SpecCur.Lst_Mst_SpecImage.Count > 0)
                    {
                        var i = 0;
                        foreach (var item in objRT_Mst_SpecCur.Lst_Mst_SpecImage)
                        {
                            var specImagePath = "";
                            var objMst_SpecImage_File = new Mst_SpecImage_File()
                            {
                                Id = i.ToString().Trim(),
                                Name = item.SpecImageName.ToString().Trim() ?? "",
                                Src = specImagePath,
                                FlagPrimaryImage = item.FlagPrimaryImage.ToString().Trim() ?? "",
                                Status = "O",
                            };
                            if (!CUtils.IsNullOrEmpty(item.SpecImagePath))
                            {
                                //objMst_SpecImage_File.SrcLocal = item.SpecImagePath.ToString().Trim();
                                specImagePath = domainCur + @"/" + item.SpecImagePath.ToString().Trim().Replace(@"\", @"/");
                                objMst_SpecImage_File.Src = specImagePath;
                            }


                            listMst_SpecImage.Add(objMst_SpecImage_File);
                            i++;
                        }
                    }
                    ViewBag.ListMst_SpecFiles = listMst_SpecFiles;
                    ViewBag.ListMst_SpecImage = listMst_SpecImage;
                    return View(objRT_Mst_SpecCur.Lst_Mst_Spec[0]);
                }
            }
            return View(new ProductCentrer.Mst_Spec());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            var domainCur = WA_OS_GetFileFromPath(GetNextTId(), waUserCode, waUserPassword);//"http://103.35.65.129:12908/idocNet.Test.ProductCenter.V10.WA/api/File/UploadedFiles/";
            try
            {
                if (!CUtils.IsNullOrEmpty(model))
                {
                    var objRQ_Mst_SpecUIInput = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_Mst_SpecUI>(model);
                    var objMst_SpecUI = new Mst_SpecUI()
                    {
                        OrgID = objRQ_Mst_SpecUIInput.Mst_Spec.SpecCode,
                        SpecCode = objRQ_Mst_SpecUIInput.Mst_Spec.SpecCode,
                    };

                    var strWhereClauseMst_Spec = strWhereClause_Mst_SpecBySpecCode(objMst_SpecUI);

                    var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_ProductCentrer_GwUserCode,
                        GwPassword = OS_ProductCentrer_GwPassword,
                        OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                        NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_Spec,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Spec
                        Rt_Cols_Mst_Spec = "*",
                        Rt_Cols_Mst_SpecFiles = "*",
                        Rt_Cols_Mst_SpecImage = "*",
                        Mst_Spec = null,
                        Lst_Mst_SpecFiles = null,
                        Lst_Mst_SpecImage = null,
                    };

                    var objRT_Mst_SpecCur = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                    if (objRT_Mst_SpecCur.Lst_Mst_Spec != null && objRT_Mst_SpecCur.Lst_Mst_Spec.Count > 0)
                    {
                        var orgID = objRT_Mst_SpecCur.Lst_Mst_Spec[0].OrgID;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].SpecName = objRQ_Mst_SpecUIInput.Mst_Spec.SpecName;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].ModelCode = objRQ_Mst_SpecUIInput.Mst_Spec.ModelCode;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].SpecType1 = objRQ_Mst_SpecUIInput.Mst_Spec.SpecType1;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].SpecType2 = objRQ_Mst_SpecUIInput.Mst_Spec.SpecType2;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].Color = objRQ_Mst_SpecUIInput.Mst_Spec.Color;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].DefaultUnitCode = objRQ_Mst_SpecUIInput.Mst_Spec.DefaultUnitCode;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].StandardUnitCode = objRQ_Mst_SpecUIInput.Mst_Spec.StandardUnitCode;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].SpecDesc = objRQ_Mst_SpecUIInput.Mst_Spec.SpecDesc;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].FlagHasSerial = objRQ_Mst_SpecUIInput.Mst_Spec.FlagHasSerial;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].FlagHasLOT = objRQ_Mst_SpecUIInput.Mst_Spec.FlagHasLOT;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].CustomField1 = objRQ_Mst_SpecUIInput.Mst_Spec.CustomField1;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].CustomField2 = objRQ_Mst_SpecUIInput.Mst_Spec.CustomField2;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].CustomField3 = objRQ_Mst_SpecUIInput.Mst_Spec.CustomField3;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].CustomField4 = objRQ_Mst_SpecUIInput.Mst_Spec.CustomField4;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].CustomField5 = objRQ_Mst_SpecUIInput.Mst_Spec.CustomField5;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].CustomField6 = objRQ_Mst_SpecUIInput.Mst_Spec.CustomField6;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].CustomField7 = objRQ_Mst_SpecUIInput.Mst_Spec.CustomField7;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].CustomField8 = objRQ_Mst_SpecUIInput.Mst_Spec.CustomField8;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].CustomField9 = objRQ_Mst_SpecUIInput.Mst_Spec.CustomField9;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].CustomField10 = objRQ_Mst_SpecUIInput.Mst_Spec.CustomField10;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].FlagActive = objRQ_Mst_SpecUIInput.Mst_Spec.FlagActive;
                        objRT_Mst_SpecCur.Lst_Mst_Spec[0].NetworkSpecCode = objRQ_Mst_SpecUIInput.Mst_Spec.NetworkSpecCode;

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_Spec = TableName.Mst_Spec + ".";
                        
                        if (isparent && !waOrgID.Equals(orgID))
                        {
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.NetworkSpecCode);
                        }
                        else
                        {
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.SpecName);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.ModelCode);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.SpecType1);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.SpecType2);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.Color);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.DefaultUnitCode);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.StandardUnitCode);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.SpecDesc);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.FlagHasSerial);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.FlagHasLOT);

                            #region["Mst_SpecCustomField"]
                            var objMst_SpecCustomField = new ProductCentrer.Mst_SpecCustomField()
                            {
                                FlagActive = FlagActive,
                            };

                            var strWhereClauseMst_SpecCustomField = strWhereClause_Mst_SpecCustomField(objMst_SpecCustomField);

                            var list_Mst_SpecCustomField = List_Mst_SpecCustomField(strWhereClauseMst_SpecCustomField);
                            if (list_Mst_SpecCustomField != null &&
                                list_Mst_SpecCustomField.Count > 0)
                            {
                                foreach (var it in list_Mst_SpecCustomField)
                                {
                                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, it.SpecCustomFieldCode);
                                }

                            }
                            #endregion


                            //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.CustomField1);
                            //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.CustomField2);
                            //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.CustomField3);
                            //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.CustomField4);
                            //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.CustomField5);
                            //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.CustomField6);
                            //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.CustomField7);
                            //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.CustomField8);
                            //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.CustomField9);
                            //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.CustomField10);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.FlagActive);
                        }
                        objRQ_Mst_Spec.Ft_WhereClause = null;
                        objRQ_Mst_Spec.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_Spec.Rt_Cols_Mst_Spec = null;
                        objRQ_Mst_Spec.Rt_Cols_Mst_SpecFiles = null;
                        objRQ_Mst_Spec.Rt_Cols_Mst_SpecImage = null;
                        objRQ_Mst_Spec.Mst_Spec = objRT_Mst_SpecCur.Lst_Mst_Spec[0];

                        var listMst_SpecImage = new List<ProductCentrer.Mst_SpecImage>();
                        //var listOS_PrdCenter_Mst_SpecImageUI = new List<OS_PrdCenter_Mst_SpecImageUI>();
                        var listMst_SpecFiles = new List<ProductCentrer.Mst_SpecFiles>();
                        //var listOS_PrdCenter_Mst_SpecFilesUI = new List<OS_PrdCenter_Mst_SpecFilesUI>();

                        var listMoveFiles_Images = new List<MoveFiles>();
                        if (objRQ_Mst_SpecUIInput != null)
                        {
                            #region["Upload Image"]

                            if (objRQ_Mst_SpecUIInput.ListImages != null && objRQ_Mst_SpecUIInput.ListImages.Count > 0)
                            {
                                foreach (var item in objRQ_Mst_SpecUIInput.ListImages)
                                {
                                    if (!CUtils.IsNullOrEmpty(item.Status) &&
                                        item.Status.ToString().ToUpper().Trim().Equals("N"))
                                    {
                                        string folderUpload = @"UploadedFiles\SpecImages_Temp\" + waUserCode;
                                        var fileName = item.Name;
                                        // Upload file lên server
                                        var objRQ_File = new ProductCentrer.RQ_File()
                                        {
                                            FlagIsDelete = null,
                                            Tid = GetNextTId(),
                                            GwUserCode = OS_ProductCentrer_GwUserCode,
                                            GwPassword = OS_ProductCentrer_GwPassword,
                                            OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                            NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                            FuncType = null,
                                            Ft_RecordStart = Ft_RecordStart,
                                            Ft_RecordCount = Ft_RecordCount,
                                            Ft_WhereClause = null,
                                            Ft_Cols_Upd = null,
                                            WAUserCode = waUserCode,
                                            WAUserPassword = waUserPassword,
                                            UtcOffset = userState.UtcOffset,
                                            // RQ_File
                                            folderUpload = folderUpload,
                                            fileName = fileName,
                                            uploadFileAsBase64String = item.Base64,
                                        };
                                        var result = OS_PrdCenter_FileService.Instance.WA_OS_UploadFile(objRQ_File, addressAPIs_ProductCentrer);
                                        if (!CUtils.IsNullOrEmpty(result.Status) &&
                                            result.Status.ToString().ToLower().Trim().Equals("true"))
                                        {
                                            // Lưu file tem trên Server Biz thành công

                                            // Lấy đường dẫn file
                                            var filePathServerTem = result.AppPath.ToString().Trim();
                                            var filePathServer = filePathServerTem.Replace(@"SpecImages_Temp", @"SpecImages"); // đường dẫn sau khi move file
                                            var index = filePathServer.LastIndexOf(@"\", System.StringComparison.Ordinal);
                                            var folderUploadServer = "";

                                            folderUploadServer = filePathServer.Substring(0, index);
                                            index = folderUploadServer.IndexOf(@"\UploadedFiles\", System.StringComparison.Ordinal);
                                            if (index == 0)
                                            {
                                                folderUploadServer = folderUploadServer.Replace(@"\UploadedFiles\", @"UploadedFiles\");
                                            }

                                            var objMoveFiles = new MoveFiles()
                                            {
                                                FolderUpload = folderUploadServer,
                                                SourceFileName = filePathServerTem,
                                                DestFileName = filePathServer,
                                            };

                                            listMoveFiles_Images.Add(objMoveFiles);

                                            var objMst_SpecImage = new ProductCentrer.Mst_SpecImage()
                                            {
                                                SpecImagePath = filePathServer,
                                                SpecImageName = fileName,
                                                SpecImageDesc = null,
                                                FlagPrimaryImage = item.FlagPrimaryImage,
                                            };
                                            listMst_SpecImage.Add(objMst_SpecImage);
                                        }
                                    }
                                    else
                                    {
                                        //var domainCur = "http://103.35.65.129:12908/idocNet.Test.ProductCenter.V10.WA/api/File/UploadedFiles/";
                                        //var srcCur = item.Src.Replace(domainCur, @"UploadedFiles\"); 
                                        var srcCur = item.Src;

                                        // Cập nhật lại đường link sp bị sai
                                        while (srcCur.Contains("UploadedFiles/"))
                                        {
                                            srcCur = srcCur.Replace("UploadedFiles/", "");
                                        }

                                        //srcCur = srcCur.Replace("/UploadedFilesSpecImages/", "/SpecImages/");
                                        srcCur = srcCur.Replace(domainCur, @"UploadedFiles");
                                        srcCur = srcCur.Replace(@"/", @"\");
                                        var fileName = "";
                                        if (!CUtils.IsNullOrEmpty(item.Name))
                                        {
                                            fileName = CUtils.StrValueOrNull(item.Name);
                                        }

                                        var flagPrimaryImage = "0";
                                        if (!CUtils.IsNullOrEmpty(item.FlagPrimaryImage))
                                        {
                                            flagPrimaryImage = CUtils.StrValueOrNull(item.FlagPrimaryImage);
                                        }
                                        var objMst_SpecImage = new ProductCentrer.Mst_SpecImage()
                                        {

                                            SpecImagePath = srcCur,
                                            SpecImageName = fileName,
                                            SpecImageDesc = null,
                                            FlagPrimaryImage = flagPrimaryImage,
                                        };
                                        listMst_SpecImage.Add(objMst_SpecImage);
                                    }

                                }
                            }
                            #endregion

                            #region["Upload File"]
                            if (objRQ_Mst_SpecUIInput.ListFiles != null && objRQ_Mst_SpecUIInput.ListFiles.Count > 0)
                            {
                                foreach (var item in objRQ_Mst_SpecUIInput.ListFiles)
                                {
                                    if (!CUtils.IsNullOrEmpty(item.Status) &&
                                        item.Status.ToString().ToUpper().Trim().Equals("N"))
                                    {
                                        string folderUpload = @"UploadedFiles\SpecFiles_Temp\" + waUserCode;
                                        var fileName = item.Name;
                                        // Upload file lên server
                                        var objRQ_File = new ProductCentrer.RQ_File()
                                        {
                                            FlagIsDelete = null,
                                            Tid = GetNextTId(),
                                            GwUserCode = OS_ProductCentrer_GwUserCode,
                                            GwPassword = OS_ProductCentrer_GwPassword,
                                            OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                            NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                            FuncType = null,
                                            Ft_RecordStart = Ft_RecordStart,
                                            Ft_RecordCount = Ft_RecordCount,
                                            Ft_WhereClause = null,
                                            Ft_Cols_Upd = null,
                                            WAUserCode = waUserCode,
                                            WAUserPassword = waUserPassword,
                                            UtcOffset = userState.UtcOffset,
                                            // RQ_File
                                            folderUpload = folderUpload,
                                            fileName = fileName,
                                            uploadFileAsBase64String = item.Base64,
                                        };
                                        var result = OS_PrdCenter_FileService.Instance.WA_OS_UploadFile(objRQ_File, addressAPIs_ProductCentrer);
                                        if (!CUtils.IsNullOrEmpty(result.Status) &&
                                            result.Status.ToString().ToLower().Trim().Equals("true"))
                                        {
                                            // Lưu file tem trên Server Biz thành công

                                            // Lấy đường dẫn file
                                            var filePathServerTem = result.AppPath.ToString().Trim();
                                            var filePathServer = filePathServerTem.Replace(@"SpecFiles_Temp", @"SpecFiles"); // đường dẫn sau khi move file
                                            var index = filePathServer.LastIndexOf(@"\", System.StringComparison.Ordinal);
                                            var folderUploadServer = "";

                                            folderUploadServer = filePathServer.Substring(0, index);
                                            index = folderUploadServer.IndexOf(@"\UploadedFiles\", System.StringComparison.Ordinal);
                                            if (index == 0)
                                            {
                                                folderUploadServer = folderUploadServer.Replace(@"\UploadedFiles\", @"UploadedFiles\");
                                            }

                                            var objMoveFiles = new MoveFiles()
                                            {
                                                FolderUpload = folderUploadServer,
                                                SourceFileName = filePathServerTem,
                                                DestFileName = filePathServer,
                                            };

                                            listMoveFiles_Images.Add(objMoveFiles);

                                            var objMst_SpecFiles = new ProductCentrer.Mst_SpecFiles()
                                            {
                                                SpecFilePath = filePathServer,
                                                SpecFileName = fileName,
                                                SpecFileDesc = null,
                                            };
                                            listMst_SpecFiles.Add(objMst_SpecFiles);
                                        }
                                    }
                                    else
                                    {
                                        //var domainCur = "http://103.35.65.129:12908/idocNet.Test.ProductCenter.V10.WA/api/File/UploadedFiles/";
                                        var srcCur = item.Src.Replace(domainCur, @"UploadedFiles\");
                                        srcCur = srcCur.Replace(@"/", @"\");
                                        var fileName = "";
                                        if (!CUtils.IsNullOrEmpty(item.Name))
                                        {
                                            fileName = CUtils.StrValueOrNull(item.Name);
                                        }
                                        var objMst_SpecFiles = new ProductCentrer.Mst_SpecFiles()
                                        {

                                            SpecFilePath = srcCur,
                                            SpecFileName = fileName,
                                            SpecFileDesc = null,
                                        };
                                        listMst_SpecFiles.Add(objMst_SpecFiles);
                                    }

                                }
                            }
                            #endregion
                        }

                        objRQ_Mst_Spec.Lst_Mst_SpecFiles = listMst_SpecFiles;
                        objRQ_Mst_Spec.Lst_Mst_SpecImage = listMst_SpecImage;

                        OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Upd(objRQ_Mst_Spec, addressAPIs_ProductCentrer);

                        // Sửa Spec thành công => move file
                        if (listMoveFiles_Images != null && listMoveFiles_Images.Count > 0)
                        {
                            foreach (var item in listMoveFiles_Images)
                            {
                                if (!CUtils.IsNullOrEmpty(item.FolderUpload))
                                {
                                    var objRQ_File = new ProductCentrer.RQ_File()
                                    {
                                        FlagIsDelete = null,
                                        Tid = GetNextTId(),
                                        GwUserCode = OS_ProductCentrer_GwUserCode,
                                        GwPassword = OS_ProductCentrer_GwPassword,
                                        OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                        NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                        FuncType = null,
                                        Ft_RecordStart = Ft_RecordStart,
                                        Ft_RecordCount = Ft_RecordCount,
                                        Ft_WhereClause = null,
                                        Ft_Cols_Upd = null,
                                        WAUserCode = waUserCode,
                                        WAUserPassword = waUserPassword,
                                        UtcOffset = userState.UtcOffset,
                                        // RQ_File
                                        folderUpload = item.FolderUpload,
                                        sourceFileName = item.SourceFileName,
                                        destFileName = item.DestFileName,
                                    };
                                    var result = OS_PrdCenter_FileService.Instance.WA_OS_MoveFile(objRQ_File, addressAPIs_ProductCentrer);

                                }
                            }
                        }

                        // Xóa File - Image

                        if (objRQ_Mst_SpecUIInput.ListImagesDelete != null &&
                            objRQ_Mst_SpecUIInput.ListImagesDelete.Count > 0)
                        {
                            foreach (var item in objRQ_Mst_SpecUIInput.ListImagesDelete)
                            {
                                var srcCur = item.Src.Replace(domainCur, @"UploadedFiles\");
                                var objRQ_File = new ProductCentrer.RQ_File()
                                {
                                    FlagIsDelete = null,
                                    Tid = GetNextTId(),
                                    GwUserCode = OS_ProductCentrer_GwUserCode,
                                    GwPassword = OS_ProductCentrer_GwPassword,
                                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                    FuncType = null,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = null,
                                    Ft_Cols_Upd = null,
                                    WAUserCode = waUserCode,
                                    WAUserPassword = waUserPassword,
                                    UtcOffset = userState.UtcOffset,
                                    // RQ_File
                                    folderUpload = null,
                                    sourceFileName = srcCur,
                                    destFileName = null,
                                };
                                var result = OS_PrdCenter_FileService.Instance.WA_OS_DeleteFile(objRQ_File, addressAPIs_ProductCentrer);
                            }
                        }

                        if (objRQ_Mst_SpecUIInput.ListFilesDelete != null &&
                            objRQ_Mst_SpecUIInput.ListFilesDelete.Count > 0)
                        {
                            foreach (var item in objRQ_Mst_SpecUIInput.ListFilesDelete)
                            {
                                var srcCur = item.Src.Replace(domainCur, @"UploadedFiles\");
                                var objRQ_File = new ProductCentrer.RQ_File()
                                {
                                    FlagIsDelete = null,
                                    Tid = GetNextTId(),
                                    GwUserCode = OS_ProductCentrer_GwUserCode,
                                    GwPassword = OS_ProductCentrer_GwPassword,
                                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                    FuncType = null,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = null,
                                    Ft_Cols_Upd = null,
                                    WAUserCode = waUserCode,
                                    WAUserPassword = waUserPassword,
                                    UtcOffset = userState.UtcOffset,
                                    // RQ_File
                                    folderUpload = null,
                                    sourceFileName = srcCur,
                                    destFileName = null,
                                };
                                var result = OS_PrdCenter_FileService.Instance.WA_OS_DeleteFile(objRQ_File, addressAPIs_ProductCentrer);
                            }
                        }

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa Spec thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã Spec '" + objRQ_Mst_SpecUIInput.Mst_Spec.SpecCode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã Spec trống!");
                }

                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Update", null, resultEntry);
        }
        #endregion

        #region["Chi tiết - Xóa Spec"]
        [HttpGet]
        public ActionResult Detail(string speccode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            ViewBag.WaOrgID = waOrgID;
            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            ViewBag.IsParent = isparent;
            var domainCur = WA_OS_GetFileFromPath(GetNextTId(), waUserCode, waUserPassword);//"http://103.35.65.129:12908/idocNet.Test.ProductCenter.V10.WA/api/File";
            if (!CUtils.IsNullOrEmpty(speccode))
            {
                var objMst_SpecUI = new Mst_SpecUI()
                {
                    SpecCode = speccode,
                };

                var strWhereClauseMst_Spec = strWhereClause_Mst_SpecBySpecCode(objMst_SpecUI);

                var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_Spec,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Spec
                    Rt_Cols_Mst_Spec = "*",
                    Rt_Cols_Mst_SpecFiles = "*",
                    Rt_Cols_Mst_SpecImage = "*",
                    Mst_Spec = null,
                    Lst_Mst_SpecFiles = null,
                    Lst_Mst_SpecImage = null,
                };

                var objRT_Mst_SpecCur = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                if (objRT_Mst_SpecCur.Lst_Mst_Spec != null && objRT_Mst_SpecCur.Lst_Mst_Spec.Count > 0)
                {
                    #region["Model"]
                    //var objOS_PrdCenter_Mst_Model = new OS_PrdCenter_Mst_Model()
                    //{
                    //    FlagActive = FlagActive,
                    //};
                    //if (!isparent)
                    //{
                    //    objOS_PrdCenter_Mst_Model.OrgID = waOrgID;
                    //}
                    //var strWhereClauseMst_Model = strWhereClause_Mst_Model(objOS_PrdCenter_Mst_Model);
                    //var listOS_PrdCenter_Mst_Model = List_Mst_Model(strWhereClauseMst_Model);
                    //ViewBag.ListOS_PrdCenter_Mst_Model = listOS_PrdCenter_Mst_Model;
                    #endregion
                    #region["Mst_SpecType1"]
                    var objMst_SpecType1 = new ProductCentrer.Mst_SpecType1()
                    {
                        FlagActive = FlagActive,
                    };
                    if (!isparent)
                    {
                        objMst_SpecType1.OrgID = waOrgID;
                    }
                    var strWhereClauseMst_SpecType1 = strWhereClause_Mst_SpecType1(objMst_SpecType1);

                    var listMst_SpecType1 = List_Mst_SpecType1(strWhereClauseMst_SpecType1);
                    ViewBag.ListOS_PrdCenter_Mst_SpecType1 = listMst_SpecType1;
                    #endregion
                    #region["Mst_SpecType2"]
                    //var objOS_PrdCenter_Mst_SpecType2 = new OS_PrdCenter_Mst_SpecType2()
                    //{
                    //    FlagActive = FlagActive,
                    //};
                    //if (!isparent)
                    //{
                    //    objOS_PrdCenter_Mst_SpecType2.OrgID = waOrgID;
                    //}
                    //var strWhereClauseMst_SpecType2 = strWhereClause_Mst_SpecType2(objOS_PrdCenter_Mst_SpecType2);

                    //var listOS_PrdCenter_Mst_SpecType2 = List_Mst_SpecType2(strWhereClauseMst_SpecType2);
                    //ViewBag.ListOS_PrdCenter_Mst_SpecType2 = listOS_PrdCenter_Mst_SpecType2;
                    #endregion
                    #region["Mst_Unit"]
                    var objMst_Unit = new ProductCentrer.Mst_Unit()
                    {
                        FlagActive = FlagActive,
                    };

                    var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);

                    var list_Mst_Unit = List_Mst_Unit(strWhereClauseMst_Unit);
                    ViewBag.ListOS_PrdCenter_Mst_Unit = list_Mst_Unit;
                    #endregion
                    #region["Mst_SpecCustomField"]
                    var objMst_SpecCustomField = new ProductCentrer.Mst_SpecCustomField()
                    {
                        FlagActive = FlagActive,
                    };

                    var strWhereClauseMst_SpecCustomField = strWhereClause_Mst_SpecCustomField(objMst_SpecCustomField);

                    var list_Mst_SpecCustomField = List_Mst_SpecCustomField(strWhereClauseMst_SpecCustomField);
                    ViewBag.List_OS_PrdCenter_Mst_SpecCustomField = list_Mst_SpecCustomField;
                    #endregion
                    //Mst_SpecImage_File
                    var listMst_SpecFiles = new List<Mst_SpecImage_File>();
                    if (objRT_Mst_SpecCur.Lst_Mst_SpecFiles != null &&
                        objRT_Mst_SpecCur.Lst_Mst_SpecFiles.Count > 0)
                    {
                        var i = 0;
                        foreach (var item in objRT_Mst_SpecCur.Lst_Mst_SpecFiles)
                        {
                            var specFilePath = "";
                            var objMst_SpecImage_File = new Mst_SpecImage_File()
                            {
                                Id = i.ToString().Trim(),
                                Name = item.SpecFileName.ToString().Trim() ?? "",
                                Status = "O",
                            };
                            if (!CUtils.IsNullOrEmpty(item.SpecFilePath))
                            {
                                //objMst_SpecImage_File.SrcLocal = item.SpecFilePath.ToString().Trim();

                                specFilePath = domainCur + @"/" + item.SpecFilePath.ToString().Trim().Replace(@"\", @"/");
                                objMst_SpecImage_File.Src = specFilePath;
                            }


                            listMst_SpecFiles.Add(objMst_SpecImage_File);
                            i++;
                        }
                    }
                    var listMst_SpecImage = new List<Mst_SpecImage_File>();
                    if (objRT_Mst_SpecCur.Lst_Mst_SpecImage != null &&
                        objRT_Mst_SpecCur.Lst_Mst_SpecImage.Count > 0)
                    {
                        var i = 0;
                        foreach (var item in objRT_Mst_SpecCur.Lst_Mst_SpecImage)
                        {
                            var specImagePath = "";
                            var objMst_SpecImage_File = new Mst_SpecImage_File()
                            {
                                Id = i.ToString().Trim(),
                                Name = item.SpecImageName.ToString().Trim() ?? "",
                                Src = specImagePath,
                                FlagPrimaryImage = item.FlagPrimaryImage.ToString().Trim() ?? "",
                                Status = "O",
                            };
                            if (!CUtils.IsNullOrEmpty(item.SpecImagePath))
                            {
                                //objMst_SpecImage_File.SrcLocal = item.SpecImagePath.ToString().Trim();
                                specImagePath = domainCur + @"/" + item.SpecImagePath.ToString().Trim().Replace(@"\", @"/");
                                objMst_SpecImage_File.Src = specImagePath;
                            }


                            listMst_SpecImage.Add(objMst_SpecImage_File);
                            i++;
                        }
                    }
                    ViewBag.ListMst_SpecFiles = listMst_SpecFiles;
                    ViewBag.ListMst_SpecImage = listMst_SpecImage;
                    return View(objRT_Mst_SpecCur.Lst_Mst_Spec[0]);
                }
            }
            return View(new ProductCentrer.Mst_Spec());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string speccode, string orgID)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(speccode))
                {
                    var objMst_SpecUI = new Mst_SpecUI()
                    {
                        SpecCode = speccode,
                        OrgID = orgID
                    };

                    var strWhereClauseOS_PrdCenter_Mst_Spec = strWhereClause_Mst_SpecBySpecCode(objMst_SpecUI);

                    var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_ProductCentrer_GwUserCode,
                        GwPassword = OS_ProductCentrer_GwPassword,
                        OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                        NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseOS_PrdCenter_Mst_Spec,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Spec
                        Rt_Cols_Mst_Spec = "*",
                        Rt_Cols_Mst_SpecFiles = "*",
                        Rt_Cols_Mst_SpecImage = "*",
                        Mst_Spec = null,
                        Lst_Mst_SpecFiles = null,
                        Lst_Mst_SpecImage = null,
                    };

                    var objRT_Mst_SpecCur = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                    if (objRT_Mst_SpecCur.Lst_Mst_Spec != null && objRT_Mst_SpecCur.Lst_Mst_Spec.Count > 0)
                        if (objRT_Mst_SpecCur.Lst_Mst_Spec != null && objRT_Mst_SpecCur.Lst_Mst_Spec.Count > 0)
                        {
                            objRQ_Mst_Spec.Mst_Spec = objRT_Mst_SpecCur.Lst_Mst_Spec[0];
                            objRQ_Mst_Spec.Lst_Mst_SpecFiles =
                                objRT_Mst_SpecCur.Lst_Mst_SpecFiles;
                            objRQ_Mst_Spec.Lst_Mst_SpecImage =
                                objRT_Mst_SpecCur.Lst_Mst_SpecImage;
                            OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Del(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                            #region["Xóa File - Images"]

                            if (objRT_Mst_SpecCur.Lst_Mst_SpecFiles != null &&
                                objRT_Mst_SpecCur.Lst_Mst_SpecFiles.Count > 0)
                            {
                                foreach (var item in objRT_Mst_SpecCur.Lst_Mst_SpecFiles)
                                {
                                    var srcCur = item.SpecFilePath;
                                    var objRQ_File = new ProductCentrer.RQ_File()
                                    {
                                        FlagIsDelete = null,
                                        Tid = GetNextTId(),
                                        GwUserCode = OS_ProductCentrer_GwUserCode,
                                        GwPassword = OS_ProductCentrer_GwPassword,
                                        OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                        NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                        FuncType = null,
                                        Ft_RecordStart = Ft_RecordStart,
                                        Ft_RecordCount = Ft_RecordCount,
                                        Ft_WhereClause = null,
                                        Ft_Cols_Upd = null,
                                        WAUserCode = waUserCode,
                                        WAUserPassword = waUserPassword,
                                        UtcOffset = userState.UtcOffset,
                                        // RQ_File
                                        folderUpload = null,
                                        sourceFileName = srcCur,
                                        destFileName = null,
                                    };
                                    var result = OS_PrdCenter_FileService.Instance.WA_OS_DeleteFile(objRQ_File, addressAPIs_ProductCentrer);
                                }
                            }

                            if (objRT_Mst_SpecCur.Lst_Mst_SpecImage != null &&
                                objRT_Mst_SpecCur.Lst_Mst_SpecImage.Count > 0)
                            {
                                foreach (var item in objRT_Mst_SpecCur.Lst_Mst_SpecImage)
                                {
                                    var srcCur = item.SpecImagePath;
                                    var objRQ_File = new ProductCentrer.RQ_File()
                                    {
                                        FlagIsDelete = null,
                                        Tid = GetNextTId(),
                                        GwUserCode = OS_ProductCentrer_GwUserCode,
                                        GwPassword = OS_ProductCentrer_GwPassword,
                                        OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                        NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                        FuncType = null,
                                        Ft_RecordStart = Ft_RecordStart,
                                        Ft_RecordCount = Ft_RecordCount,
                                        Ft_WhereClause = null,
                                        Ft_Cols_Upd = null,
                                        WAUserCode = waUserCode,
                                        WAUserPassword = waUserPassword,
                                        UtcOffset = userState.UtcOffset,
                                        // RQ_File
                                        folderUpload = null,
                                        sourceFileName = srcCur,
                                        destFileName = null,
                                    };
                                    var result = OS_PrdCenter_FileService.Instance.WA_OS_DeleteFile(objRQ_File, addressAPIs_ProductCentrer);
                                }
                            }
                            #endregion
                            resultEntry.Success = true;
                            resultEntry.AddMessage("Xóa Spec thành công!");
                            resultEntry.RedirectUrl = Url.Action("Index");
                        }
                        else
                        {
                            resultEntry.Success = true;
                            resultEntry.AddMessage("Mã Spec '" + speccode + "' không có trong hệ thống!");
                        }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã Spec trống!");
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
            //return Json(new { Success = false, Detail = resultEntry.Detail });
        }
        #endregion

        #region["Import excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var orgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;
            var exitsData = "";

            #region["Mst_SpecCustomField"]
            var objMst_SpecCustomField = new ProductCentrer.Mst_SpecCustomField()
            {
                FlagActive = FlagActive,
            };

            var strWhereClauseMst_SpecCustomField = strWhereClause_Mst_SpecCustomField(objMst_SpecCustomField);

            var list_Mst_SpecCustomField = List_Mst_SpecCustomField(strWhereClauseMst_SpecCustomField);

            int count = list_Mst_SpecCustomField.Count + 7;
            #endregion

            try
            {
                string fileId = Guid.NewGuid().ToString("D");
                string oldFileName = file.FileName;
                var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                if (ext != ".xlsx" && ext != ".xls")
                {
                    exitsData = "File excel import không hợp lệ!";
                    resultEntry.AddMessage(exitsData);
                    return Json(resultEntry);
                }
                if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                {
                    FileUtils.SaveTempFile(file, fileId, ext);
                }
                else
                {
                    throw new Exception("File excel import không hợp lệ!");
                }
                string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);
                var listMst_Spec = new List<ProductCentrer.Mst_Spec>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != count)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    else
                    {
                        #region["Check null"]
                        var idx = 0;
                        var iRows = 2;
                        var strRows = " - Dòng ";
                        foreach (DataRow dr in table.Rows)
                        {
                            iRows = 2;
                            iRows = (iRows + idx + 1);
                            strRows = " - Dòng ";
                            strRows += iRows;

                            //if (CUtils.IsNullOrEmpty(dr[TblMst_Spec.OrgID]))
                            //{
                            //    exitsData = "Mã Tổ chức không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Spec.SpecCode]))
                            {
                                exitsData = "Mã Spec không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Spec.SpecName]))
                            {
                                exitsData = "Tên Spec không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            //if (CUtils.IsNullOrEmpty(dr[TblMst_Spec.ModelCode]))
                            //{
                            //    exitsData = "Mã Model không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            //if (CUtils.IsNullOrEmpty(dr[TblMst_Spec.SpecType1]))
                            //{
                            //    exitsData = "Loại sản phẩm không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            //if (CUtils.IsNullOrEmpty(dr[TblMst_Spec.SpecType2]))
                            //{
                            //    exitsData = "Nhóm sản phẩm không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            //if (CUtils.IsNullOrEmpty(dr[TblMst_Spec.Color]))
                            //{
                            //    exitsData = "Tên màu không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}

                            if (!CUtils.IsNullOrEmpty(dr[TblMst_Spec.SpecDesc]))
                            {
                                var specDesc = CUtils.StrValueOrNull(TblMst_Spec.SpecDesc);
                                if (specDesc.Length > Nonsense.RemarkLength)
                                {
                                    exitsData = "Mô tả > " + Nonsense.RemarkLength + " ký tự!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                            }
                            idx++;
                        }
                        #endregion

                        #region["Check duplicate"]
                        iRows = 2;
                        strRows = " - Dòng ";
                        var jRows = 2;
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            iRows = 2;
                            iRows = (iRows + i + 1);
                            strRows = " - Dòng ";
                            strRows += iRows;
                            var specCodeCur = table.Rows[i][TblMst_Spec.SpecCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _specCodeCur = table.Rows[j][TblMst_Spec.SpecCode].ToString().Trim();
                                    if (specCodeCur.Equals(_specCodeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã Spec '" + specCodeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_Spec = DataTableCmUtils.ToListof<ProductCentrer.Mst_Spec>(table);
                    // Gọi hàm save data
                    if (listMst_Spec != null && listMst_Spec.Count > 0)
                    {

                        foreach (var item in listMst_Spec)
                        {
                            item.OrgID = orgID;
                            item.NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID);
                            // Nâng cấp 2019_11_06 Set các giá trị default
                            item.ModelCode = "SYSMODEL";
                            item.SpecType2 = "SYSSPECTYPE2";
                            item.FlagHasLOT = "0";
                            item.StandardUnitCode = item.DefaultUnitCode;
                            //

                            //item.FlagActive = FlagActive;
                            var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec
                            {
                                FlagIsDelete = null,
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                FuncType = null,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = null,
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                UtcOffset = userState.UtcOffset,
                                // RQ_Mst_Spec
                                Rt_Cols_Mst_Spec = null,
                                Mst_Spec = item,
                                Lst_Mst_SpecImage = new List<ProductCentrer.Mst_SpecImage>(),
                                Lst_Mst_SpecFiles = new List<ProductCentrer.Mst_SpecFiles>(),
                            };
                            OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Add(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                        }
                    }

                    resultEntry.Success = true;
                    exitsData = "Đã nhập dữ liệu excel thành công!";
                    resultEntry.AddMessage(exitsData);
                }
                else
                {
                    exitsData = "File excel import không có dữ liệu!";
                    resultEntry.AddMessage(exitsData);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportMapping(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var orgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;
            var exitsData = "";

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }
            try
            {
                string fileId = Guid.NewGuid().ToString("D");
                string oldFileName = file.FileName;
                var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                if (ext != ".xlsx" && ext != ".xls")
                {
                    exitsData = "File excel import không hợp lệ!";
                    resultEntry.AddMessage(exitsData);
                    return Json(resultEntry);
                }
                if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                {
                    FileUtils.SaveTempFile(file, fileId, ext);
                }
                else
                {
                    throw new Exception("File excel import không hợp lệ!");
                }
                string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);
                var listMst_Spec = new List<ProductCentrer.Mst_Spec>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 3)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    else
                    {
                        #region["Check null"]
                        var idx = 0;
                        var iRows = 2;
                        var strRows = " - Dòng ";
                        foreach (DataRow dr in table.Rows)
                        {
                            iRows = 2;
                            iRows = (iRows + idx + 1);
                            strRows = " - Dòng ";
                            strRows += iRows;

                            if (CUtils.IsNullOrEmpty(dr[TblMst_Spec.OrgID]))
                            {
                                exitsData = "Mã Org không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var orgId = CUtils.StrValueOrNull(dr[TblMst_Spec.OrgID]);
                                if (orgId.Equals(waNetworkID))
                                {
                                    exitsData = "Không được mapping mã spec chung cho đơn vị tổng!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Spec.SpecCode]))
                            {
                                exitsData = "Mã Spec không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            
                            //if (CUtils.IsNullOrEmpty(dr[TblMst_Spec.NetworkSpecCode]))
                            //{
                            //    exitsData = "Mã Spec chung không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);

                            //}
                            idx++;
                        }
                        #endregion
                    }
                    listMst_Spec = DataTableCmUtils.ToListof<ProductCentrer.Mst_Spec>(table);
                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_Spec = TableName.Mst_Spec + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.OrgID);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.SpecCode);
                    //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.NetworkSpecCode);

                    if (isparent)
                    {
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Spec, TblMst_Spec.NetworkSpecCode);
                    }
                    // Gọi hàm save data
                    if (listMst_Spec != null && listMst_Spec.Count > 0)
                    {

                        foreach (var item in listMst_Spec)
                        {
                            var objMst_SpecCur = new ProductCentrer.Mst_Spec()
                            {
                                OrgID = item.OrgID,
                                SpecCode = item.SpecCode,
                                NetworkSpecCode = item.NetworkSpecCode,
                            };
                            
                            var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                FuncType = null,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = strFt_Cols_Upd,
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                UtcOffset = userState.UtcOffset,
                                // RQ_Mst_Spec
                                Rt_Cols_Mst_Spec = null,
                                Rt_Cols_Mst_SpecFiles = null,
                                Rt_Cols_Mst_SpecImage = null,
                                Mst_Spec = null,
                                Lst_Mst_SpecFiles = null,
                                Lst_Mst_SpecImage = null,
                            };
                            objRQ_Mst_Spec.Ft_WhereClause = null;
                            objRQ_Mst_Spec.Ft_Cols_Upd = strFt_Cols_Upd;
                            objRQ_Mst_Spec.Rt_Cols_Mst_Spec = null;
                            objRQ_Mst_Spec.Mst_Spec = objMst_SpecCur;

                            OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Upd(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                            
                        }
                    }
                    else
                    {
                        exitsData = "File excel import không có dữ liệu!";
                        resultEntry.AddMessage(exitsData);
                    }
                    resultEntry.Success = true;
                    exitsData = "Đã nhập dữ liệu excel thành công!";
                    resultEntry.AddMessage(exitsData);
                }
                else
                {
                    exitsData = "File excel import không có dữ liệu!";
                    resultEntry.AddMessage(exitsData);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
        #endregion

        #region["Export excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<ProductCentrer.Mst_Spec>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_Spec).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Spec"));

                return Json(new { Success = true, Title = "Xuất file excel template thành công!", CheckSuccess = "1", strUrl = url });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplateMapping()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<ProductCentrer.Mst_Spec>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateMapping();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_Spec).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Spec"));

                return Json(new { Success = true, Title = "Xuất file excel template mapping thành công!", CheckSuccess = "1", strUrl = url });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string orgid = "", string brandcode = "", string modelcode = "", string speccode = "", string specname = "", string spectype1 = "", string spectype2 = "", string flagactive = "", int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_Spec = new List<ProductCentrer.Mst_Spec>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;
            #region["Mst_SpecCustomField"]
            var objMst_SpecCustomField = new ProductCentrer.Mst_SpecCustomField()
            {
                FlagActive = FlagActive,
            };

            var strWhereClauseMst_SpecCustomField = strWhereClause_Mst_SpecCustomField(objMst_SpecCustomField);

            var list_Mst_SpecCustomField = List_Mst_SpecCustomField(strWhereClauseMst_SpecCustomField);
            ViewBag.List_OS_PrdCenter_Mst_SpecCustomField = list_Mst_SpecCustomField;
            #endregion

            try
            {
                #region["Search"]

                var objMst_SpecUI = new Mst_SpecUI()
                {
                    BrandCode = brandcode,
                    ModelCode = modelcode,
                    SpecCode = speccode,
                    SpecName = specname,
                    SpecType1 = spectype1,
                    SpecType2 = spectype2,
                    FlagActive = flagactive,
                    OrgID = orgid,
                };

                var strWhereClauseMst_Spec = strWhereClause_Mst_Spec(objMst_SpecUI);

                var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_Spec,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Spec
                    Rt_Cols_Mst_Spec = "*",
                    Rt_Cols_Mst_SpecFiles = "*",
                    Rt_Cols_Mst_SpecImage = "*",
                    Mst_Spec = null,
                    Lst_Mst_SpecFiles = null,
                    Lst_Mst_SpecImage = null,
                };

                var objRT_Mst_SpecCur = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                if (objRT_Mst_SpecCur != null && objRT_Mst_SpecCur.Lst_Mst_Spec != null)
                {
                    if (objRT_Mst_SpecCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_SpecCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_SpecCur.Lst_Mst_Spec != null && objRT_Mst_SpecCur.Lst_Mst_Spec.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_Spec.AddRange(objRT_Mst_SpecCur.Lst_Mst_Spec);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_Spec).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_Spec, dicColNames, filePath, string.Format("Mst_Spec"));


                    #region["Export các trang còn lại"]
                    listMst_Spec.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_Spec.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_SpecExportCur = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                            if (objRT_Mst_SpecExportCur != null && objRT_Mst_SpecExportCur.Lst_Mst_Spec != null && objRT_Mst_SpecExportCur.Lst_Mst_Spec.Count > 0)
                            {
                                listMst_Spec.AddRange(objRT_Mst_SpecExportCur.Lst_Mst_Spec);
                                ExcelExport.ExportToExcelFromList(listMst_Spec, dicColNames, filePath, string.Format("Mst_Spec_" + i));
                                listMst_Spec.Clear();
                            }
                        }
                    }
                    #endregion

                    return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                }
                #endregion

                else
                {
                    return Json(new { Success = true, Title = "Dữ liệu trống!", CheckSuccess = "1" });
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }
        #endregion

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            #region["Mst_SpecCustomField"]
            var objMst_SpecCustomField = new ProductCentrer.Mst_SpecCustomField()
            {
                FlagActive = FlagActive,
            };

            var strWhereClauseMst_SpecCustomField = strWhereClause_Mst_SpecCustomField(objMst_SpecCustomField);

            var list_Mst_SpecCustomField = List_Mst_SpecCustomField(strWhereClauseMst_SpecCustomField);
            #endregion

            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                //{TblMst_Spec.OrgID,"Mã tổ chức (*)"},
                {TblMst_Spec.SpecCode,"Mã Spec (*)"},
                {TblMst_Spec.SpecName,"Tên Spec (*)"},
                //{TblMst_Spec.ModelCode,"Mã Model (*)"},
                {TblMst_Spec.SpecType1,"Loại sản phẩm (*)"},
                //{TblMst_Spec.SpecType2,"Nhóm sản phẩm (*)"},
                {TblMst_Spec.Color,"Tên màu"},
                {TblMst_Spec.FlagHasSerial,"Cờ quản lý Serial"},
                //{TblMst_Spec.FlagHasLOT,"Cờ quản lý LOT"},
                {TblMst_Spec.DefaultUnitCode,"Đơn vị thường dùng (*)"},
                //{TblMst_Spec.StandardUnitCode,"Đơn vị chuẩn (*)"},
                {TblMst_Spec.SpecDesc,"Mô tả"},
            };

            foreach (var item in list_Mst_SpecCustomField)
            {
                dictionary.Add(item.SpecCustomFieldCode.ToString(), item.SpecCustomFieldName.ToString());
            }

            return dictionary;
        }

        private Dictionary<string, string> GetImportDicColumsTemplateMapping()
        {
            #region["Mst_SpecCustomField"]
            //var objOS_PrdCenter_Mst_SpecCustomField = new OS_PrdCenter_Mst_SpecCustomField()
            //{
            //    FlagActive = FlagActive,
            //};

            //var strWhereClauseMst_SpecCustomField = strWhereClause_Mst_SpecCustomField(objOS_PrdCenter_Mst_SpecCustomField);

            //var list_Mst_SpecCustomField = List_Mst_SpecCustomField(strWhereClauseMst_SpecCustomField);
            #endregion

            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                { TblMst_Spec.OrgID,"Mã Org (*)"},
                {TblMst_Spec.SpecCode,"Mã Spec (*)"},
                {TblMst_Spec.NetworkSpecCode,"Mã Spec Chung"},
            };

            //foreach (var item in list_Mst_SpecCustomField)
            //{
            //    dictionary.Add(item.SpecCustomFieldCode.ToString(), item.SpecCustomFieldName.ToString());
            //}

            return dictionary;
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            var userState = this.UserState;
            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            #region["Mst_SpecCustomField"]
            var objMst_SpecCustomField = new ProductCentrer.Mst_SpecCustomField()
            {
                FlagActive = FlagActive,
            };

            var strWhereClauseMst_SpecCustomField = strWhereClause_Mst_SpecCustomField(objMst_SpecCustomField);

            var list_Mst_SpecCustomField = List_Mst_SpecCustomField(strWhereClauseMst_SpecCustomField);
            #endregion
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {TblMst_Spec.OrgID,"Mã tổ chức (*)"},
                {TblMst_Spec.SpecCode,"Mã Spec (*)"},
            };
            if (waOrgID.Equals(waNetworkID))
            {
                dictionary.Add(TblMst_Spec.NetworkSpecCode, "Mã Spec Chung");
            }

            dictionary.Add(TblMst_Spec.SpecName, "Tên Spec (*)");
            dictionary.Add(TblMst_Spec.ModelCode, "Mã Model (*)");
            dictionary.Add(TblMst_Spec.SpecType1, "Loại sản phẩm (*)");
            dictionary.Add(TblMst_Spec.SpecType2, "Nhóm sản phẩm (*)");
            dictionary.Add(TblMst_Spec.Color, "Tên màu");
            dictionary.Add(TblMst_Spec.FlagHasSerial, "Cờ quản lý Serial");
            dictionary.Add(TblMst_Spec.FlagHasLOT, "Cờ quản lý LOT");
            dictionary.Add(TblMst_Spec.DefaultUnitCode, "Đơn vị thường dùng (*)");
            dictionary.Add(TblMst_Spec.StandardUnitCode, "Đơn vị chuẩn (*)");
            dictionary.Add(TblMst_Spec.SpecDesc, "Mô tả");
            foreach (var item in list_Mst_SpecCustomField)
            {
                dictionary.Add(item.SpecCustomFieldCode.ToString(), item.SpecCustomFieldName.ToString());
            }

            return dictionary;
        }
        #endregion

        #region["DicColums"]

        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Mst_NNT(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Model(ProductCentrer.Mst_Model data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Model = TableName.Mst_Model + ".";
            if (!CUtils.IsNullOrEmpty(data.ModelCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.ModelCode, CUtils.StrValueOrNull(data.ModelCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ModelName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.ModelName, "%" + CUtils.StrValueOrNull(data.ModelName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.BrandCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.BrandCode, CUtils.StrValueOrNull(data.BrandCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgModelCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.OrgModelCode, CUtils.StrValueOrNull(data.OrgModelCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_SpecType1(ProductCentrer.Mst_SpecType1 data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_SpecType1 = TableName.Mst_SpecType1 + ".";
            if (!CUtils.IsNullOrEmpty(data.SpecType1))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecType1 + TblMst_SpecType1.SpecType1, CUtils.StrValueOrNull(data.SpecType1), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecType1 + TblMst_SpecType1.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.SpecType1Name))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecType1 + TblMst_SpecType1.SpecType1Name, "%" + CUtils.StrValueOrNull(data.SpecType1Name) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecType1 + TblMst_SpecType1.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_SpecType2(ProductCentrer.Mst_SpecType2 data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_SpecType2 = TableName.Mst_SpecType2 + ".";
            if (!CUtils.IsNullOrEmpty(data.SpecType2))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecType2 + TblMst_SpecType2.SpecType2, CUtils.StrValueOrNull(data.SpecType2), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecType2 + TblMst_SpecType2.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.SpecType2Name))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecType2 + TblMst_SpecType2.SpecType2Name, "%" + CUtils.StrValueOrNull(data.SpecType2Name) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecType2 + TblMst_SpecType2.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Unit(ProductCentrer.Mst_Unit data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Unit = TableName.Mst_Unit + ".";
            if (!CUtils.IsNullOrEmpty(data.UnitCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Unit + TblMst_Unit.UnitCode, CUtils.StrValueOrNull(data.UnitCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.UnitName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Unit + TblMst_Unit.UnitName, "%" + CUtils.StrValueOrNull(data.UnitName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Unit + TblMst_Unit.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_SpecCustomField(ProductCentrer.Mst_SpecCustomField data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_SpecCustomField = TableName.Mst_SpecCustomField + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecCustomField + TblMst_SpecCustomField.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecCustomField + TblMst_SpecCustomField.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Brand(ProductCentrer.Mst_Brand data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Brand = TableName.Mst_Brand + ".";
            if (!CUtils.IsNullOrEmpty(data.BrandCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + TblMst_Brand.BrandCode, CUtils.StrValueOrNull(data.BrandCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + TblMst_Brand.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.BrandName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + TblMst_Brand.BrandName, "%" + CUtils.StrValueOrNull(data.BrandName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + TblMst_Brand.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Spec(Mst_SpecUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Spec = TableName.Mst_Spec + ".";
            var Tbl_Mst_Brand = TableName.Mst_Brand + ".";

            if (!CUtils.IsNullOrEmpty(data.SpecCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.SpecCode, "%" + CUtils.StrValueOrNull(data.SpecCode) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.SpecName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.SpecName, "%" + CUtils.StrValueOrNull(data.SpecName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.Color))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.Color, "%" + CUtils.StrValueOrNull(data.Color) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ModelCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.ModelCode, CUtils.StrValueOrNull(data.ModelCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.BrandCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + TblMst_Brand.BrandCode, CUtils.StrValueOrNull(data.BrandCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.SpecType1))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.SpecType1, CUtils.StrValueOrNull(data.SpecType1), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.SpecType2))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.SpecType2, CUtils.StrValueOrNull(data.SpecType2), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.DefaultUnitCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.DefaultUnitCode, CUtils.StrValueOrNull(data.DefaultUnitCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.StandardUnitCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.StandardUnitCode, CUtils.StrValueOrNull(data.StandardUnitCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_SpecBySpecCode(Mst_SpecUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Spec = TableName.Mst_Spec + ".";

            if (!CUtils.IsNullOrEmpty(data.SpecCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.SpecCode, CUtils.StrValueOrNull(data.SpecCode), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}