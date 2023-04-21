using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using CmUtils = CommonUtils;
//using TDAL = EzDAL.MyDB;
//using TDALUtils = EzDAL.Utils;
using TConst = idn.Skycic.Inventory.Constants;
using TUtils = idn.Skycic.Inventory.Utils;
using TError = idn.Skycic.Inventory.Errors;
using TBiz = idn.Skycic.Inventory.Biz;
using idn.Skycic.Inventory.Common.Models;
using mbiz.core.Data.Models;
using mbiz.core.Data.Services;
using System.Collections;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class ApiControllerBase : ApiController
    {
        protected ServiceResult<T> Success<T>(T data)
        {
            return new ServiceResult<T>()
            {
                Data = data,
                Success = true
            };
        }

        protected ServiceResult<T> Error<T>(Exception ex)
        {            
            return new ServiceResult<T>()
            {
                Data = default(T),
                Success = false,
                Exception = ex
			};
        }

		protected ServiceResult<T> Error<T>(Exception ex, T Data)
		{
			//var svex = new ServiceException(ex);
			//svex.ErrorDetail = svex.ErrorDetail + Newtonsoft.Json.JsonConvert.SerializeObject(Data);

			return new ServiceResult<T>()
			{
				Data = Data,
				Success = false,
				Exception = null
			};
		}

		// // 20190704.DũngND
		protected ServiceResult<R> ProcessMyDSInitError<R>() where R : WARTBase, new()
		{
			//var svex = new ServiceException(ex);
			//svex.ErrorDetail = svex.ErrorDetail + Newtonsoft.Json.JsonConvert.SerializeObject(Data);
			var result = new R();
			result.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(_mdsInitError);

			return Error<R>(new Exception(), result);
		}

		public ApiControllerBase()
        {
            ArrayList alParamsCoupleError = new ArrayList();
            try
            {
				LoadConfig();
				_biz.LoadInitAPIUrl_OutSide(
					ref alParamsCoupleError // alParamsCoupleError
					);

			}
			catch (Exception exc)
			{
				_mdsInitError = CmUtils.CMyDataSet.NewMyDataSet(DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff"));
				///CmUtils.CMyDataSet.SetErrorCode(ref _mdsInitError, TError.ErridnInventory.CmSys_ServiceInit);
                TUtils.CProcessExc.Process(
                    ref _mdsInitError
                    , exc
                    , TError.ErridnInventory.CmSys_ServiceInit
                    , alParamsCoupleError.ToArray()
                    );
                CmUtils.CMyDataSet.AppendErrorParams(
					ref _mdsInitError
					, "Exception.Message", exc.Message
					, "Exception.StackTrace", exc.StackTrace
					);
				_mdsInitError.AcceptChanges();
			}
		}

		public TBiz.BizidNInventory _biz = null;
		public DataSet _mdsInitError = null;
		private void LoadConfig()
		{
			_biz = new TBiz.BizidNInventory();
			_biz.LoadConfig(ConfigurationManager.AppSettings, "WS");
        }
	}
}
