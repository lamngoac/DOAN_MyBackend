using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using TUtils = idn.Skycic.Inventory.Utils;
using TConst = idn.Skycic.Inventory.Constants;

namespace idn.Skycic.Inventory.BizService.Services
{
    public class ClientServiceBase<E> //where E : WARTBase
    {
        #region ["Utils"]
        private static Dictionary<Type, object> instanceDic = new Dictionary<Type, object>();
        public static T GetInstance<T>()
            where T : ClientServiceBase<E>
        {
            var type = typeof(T);

            if (!instanceDic.ContainsKey(type))
            {
                instanceDic[type] = (T)Activator.CreateInstance(type);
            }
            return (T)instanceDic[type];

        }


        //Nên để trong webconfig 
        //protected string BaseServiceAddress { get { return "http://localhost:29674/"; } }

        //protected string BaseServiceAddress { get { return "http://118.70.233.101:3962/idocNet.Test.idn.iNOSiCICHyundai.V10.WebAPI/Help"; } }
        protected string BaseServiceAddress(string strUrlType)
        {
            string baseServiceAddress = GetServiceAddress(strUrlType);
            //baseServiceAddress = BizMasterServerAPIAddress.BaseBizMasterServerAPIAddress;
            //var baseServiceAddress = System.Configuration.ConfigurationManager.AppSettings["API_Url_idn.Skycic.Inventory"];
            return baseServiceAddress;
        }
        protected HttpClient GetHttpClient(string strUrlType)
        {
            var client = new HttpClient { BaseAddress = new Uri(BaseServiceAddress(strUrlType)) };

            // Define the serialization used json/xml/ etc
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

		protected HttpClient MstSvRoute_GetHttpClient(string strUrl)
		{
			var client = new HttpClient { BaseAddress = new Uri(strUrl) };

			// Define the serialization used json/xml/ etc
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return client;
		}
		protected string GetServiceAddress(string strUrlType)
        {
            string baseServiceAddress = "";
            if (strUrlType == TConst.UrlType.UrlMstSvPrdCenter)
            {
                baseServiceAddress = BizMasterServerPrdCenterAPIAddress.BaseBizMasterServerAPIAddress;

            }
            else if(strUrlType == TConst.UrlType.UrlPrdCenter)
            {
                //baseServiceAddress = BizidNInventoryAPIAddress.BaseBizidNInventoryAPIAddress;
                //baseServiceAddress = @"--http://localhost:12308/";

            }
            else if (strUrlType == TConst.UrlType.UrlMstSvSolution)
            {
                baseServiceAddress = BizMasterServerSolutionAPIAddress.BaseBizMasterServerSolutionAPIAddress;

            }
            return baseServiceAddress;
        }

        protected string BuildUrl(string controller, string action, object paramListObject)
        {
            var url = new StringBuilder(string.Format("api/{0}/{1}", controller, action));

            var paramDic = new RouteValueDictionary(paramListObject);
            if (paramDic != null && paramDic.Count > 0)
            {
                var paramString = string.Join("&", paramDic.Keys.Select(k => string.Format("{0}={1}", k, paramDic[k])).ToList());
                url.AppendFormat("?{0}", paramString);
            }

            return url.ToString();
        }


        protected R PostData<R, M>(string strUrlType, string controler, string action, object paramListObject, M model)
            where R : WARTBase
        {
            using (HttpClient client = GetHttpClient(strUrlType))
            {
                string url = BuildUrl(controler, action, paramListObject);
                // Call the method
                HttpResponseMessage response = client.PostAsJsonAsync<M>(url, model).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Convert the result into business object
                    var result = response.Content.ReadAsAsync<ServiceResult<R>>().Result;
                    //var result = response.Content.ReadAsAsync<R>().Result;
                    //return result;
                    if (result.Success)
                    {
                        return result.Data;
                    }

                    else
                    {
                        var c_K_DT_Sys = result.Data.c_K_DT_Sys;
                        var exception = result.Exception;
                        var clientException = new ClientException(c_K_DT_Sys, exception);
                        throw clientException;
                    }

                }
                else
                {
                    var c_K_DT_Sys = new c_K_DT_Sys();
                    var exception = new Exception("HttpClient error");
                    var clientException = new ClientException(c_K_DT_Sys, exception);
                    throw clientException;
                }
            }

        }

		protected R MstSvRoute_PostData<R, M>(string strUrl, string controler, string action, object paramListObject, M model)
			where R : WARTBase
		{
			using (HttpClient client = MstSvRoute_GetHttpClient(strUrl))
			{
				string url = BuildUrl(controler, action, paramListObject);
				// Call the method
				HttpResponseMessage response = client.PostAsJsonAsync<M>(url, model).Result;
				if (response.IsSuccessStatusCode)
				{
					// Convert the result into business object
					var result = response.Content.ReadAsAsync<ServiceResult<R>>().Result;
					//var result = response.Content.ReadAsAsync<R>().Result;
					//return result;
					if (result.Success)
					{
						return result.Data;
					}

					else
					{
						var c_K_DT_Sys = result.Data.c_K_DT_Sys;
						var exception = result.Exception;
						var clientException = new ClientException(c_K_DT_Sys, exception);
						throw clientException;
					}

				}
				else
				{
					var c_K_DT_Sys = new c_K_DT_Sys();
					var exception = new Exception("HttpClient error");
					var clientException = new ClientException(c_K_DT_Sys, exception);
					throw clientException;
				}
			}

		}



		#endregion

		#region ["Properties"]
		public virtual string ApiControllerName { get; set; }
        #endregion

        #region ["Actions"]
        //public virtual E Get(string sessionId, E dummy)
        //{
        //    return PostData<E, E>(ApiControllerName, "Get", new { sessionId = sessionId }, dummy);
        //}

        //public virtual E Add(string sessionId, E data)
        //{
        //    return PostData<E, E>(ApiControllerName, "Add", new { sessionId = sessionId }, data);
        //}

        //public virtual E Update(string sessionId, E data)
        //{
        //    return PostData<E, E>(ApiControllerName, "Update", new { sessionId = sessionId }, data);
        //}

        //public virtual E Delete(string sessionId, E data)
        //{
        //    return PostData<int, E>(ApiControllerName, "Delete", new { sessionId = sessionId }, data);
        //}

        //public virtual List<E> Search(string sessionId, SearchInput searchInput)
        //{
        //    return PostData<List<E>, SearchInput>(ApiControllerName, "Search", new { sessionId = sessionId }, searchInput);
        //}
        #endregion

        protected string SkyCICBuildUrl(string path, object paramListObject)
        {
            var url = new StringBuilder(string.Format("{0}", path));

            var paramDic = new RouteValueDictionary(paramListObject);
            if (paramDic != null && paramDic.Count > 0)
            {
                var paramString = string.Join("&", paramDic.Keys.Select(k => string.Format("{0}={1}", k, paramDic[k])).ToList());
                url.AppendFormat("?{0}", paramString);
            }

            return url.ToString();
        }

        public R SkyCICPostData<R, M>(string baseAddress, string path, string strAuthorization, object paramListObject, M model)
        {
            using (HttpClient client = MstSvRoute_GetHttpClient(baseAddress))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + strAuthorization);
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                client.DefaultRequestHeaders.Add("orgId", "0");

                string url = SkyCICBuildUrl(path, paramListObject);
                // Call the method
                HttpResponseMessage response = client.PostAsJsonAsync<M>(url, model).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Convert the result into business object
                    var result = response.Content.ReadAsAsync<ServiceResult<R>>().Result;

                    if (result.Success)
                    {
                        return result.Data;
                    }

                    else
                        throw result.Exception;

                }

                else throw new Exception("HttpClient error");
            }

        }
    }

    public class ClientServiceBaseFile
    {

        protected string BaseServiceAddress(string strFlagUrlMasterServer)
        {
            string baseServiceAddress = GetServiceAddress(strFlagUrlMasterServer);
            //baseServiceAddress = BizMasterServerAPIAddress.BaseBizMasterServerAPIAddress;
            //var baseServiceAddress = System.Configuration.ConfigurationManager.AppSettings["API_Url_idn.Skycic.Inventory"];
            return baseServiceAddress;
        }
        protected HttpClient GetHttpClient(string strFlagUrlMasterServer)
        {
            var client = new HttpClient { BaseAddress = new Uri(BaseServiceAddress(strFlagUrlMasterServer)) };

            // Define the serialization used json/xml/ etc
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
        protected string GetServiceAddress(string strFlagUrlMasterServer)
        {
            bool bFlagUrlMasterServer = (strFlagUrlMasterServer != null && strFlagUrlMasterServer.Length > 0 && strFlagUrlMasterServer == "1");
            string baseServiceAddress = "";
            if (strFlagUrlMasterServer == "1")
            {
                baseServiceAddress = BizMasterServerPrdCenterAPIAddress.BaseBizMasterServerAPIAddress;

            }
            else
            {
               // baseServiceAddress = BizidNInventoryAPIAddress.BaseBizidNInventoryAPIAddress;

            }
            return baseServiceAddress;
        }

        protected string BuildUrl(string controller, string action, object paramListObject)
        {
            var url = new StringBuilder(string.Format("api/{0}/{1}", controller, action));

            var paramDic = new RouteValueDictionary(paramListObject);
            if (paramDic != null && paramDic.Count > 0)
            {
                var paramString = string.Join("&", paramDic.Keys.Select(k => string.Format("{0}={1}", k, paramDic[k])).ToList());
                url.AppendFormat("?{0}", paramString);
            }

            return url.ToString();
        }

        protected string[] Up_Move_File(string strFlagUrlMasterServer, string controler, string action, object paramListObject, RQ_File model)
        {
            using (HttpClient client = GetHttpClient(strFlagUrlMasterServer))
            {
                string url = BuildUrl(controler, action, paramListObject);
                // Call the method
                HttpResponseMessage response = client.PostAsJsonAsync<RQ_File>(url, model).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Convert the result into business object
                    var result = response.Content.ReadAsAsync<string[]>().Result;
                    //var result = response.Content.ReadAsAsync<R>().Result;
                    return result;
                }
                else
                {
                    var result = new string[1];
                    result[0] = "Có lỗi trong khi upload file";
                    return result;
                    //var c_K_DT_Sys = new c_K_DT_Sys();
                    //var exception = new Exception("HttpClient error");
                    //var clientException = new ClientException(c_K_DT_Sys, exception);
                    //throw clientException;
                }
            }

        }


    }
}
