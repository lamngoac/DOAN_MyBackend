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
using idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.ClientService.Services
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
        //protected string BaseServiceAddress
        //{
        //    get
        //    {
        //        var baseServiceAddress = ServiceAddress.BaseServiceAddress;
        //        return baseServiceAddress;
        //    }
        //}

        protected string BaseReportServerAPIAddress
        {
            get
            {
                var baseServiceAddress = ServiceAddress.BaseReportServerAPIAddress;
                return baseServiceAddress;
            }
        }
        protected HttpClient GetHttpClient(string addressAPIs = "")
        {
            var client = new HttpClient { BaseAddress = new Uri(addressAPIs) };

            // Define the serialization used json/xml/ etc
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var timeOut = TimeSpan.MaxValue;
            client.Timeout = TimeSpan.FromHours(24);
            return client;
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

        protected string BuildUrl1(string controller, string action, object paramListObject)
        {
            var url = new StringBuilder(string.Format("{0}/{1}", controller, action));

            var paramDic = new RouteValueDictionary(paramListObject);
            if (paramDic != null && paramDic.Count > 0)
            {
                var paramString = string.Join("&", paramDic.Keys.Select(k => string.Format("{0}={1}", k, paramDic[k])).ToList());
                url.AppendFormat("?{0}", paramString);
            }

            return url.ToString();
        }


        //protected R PostData<R, M>(string controler, string action, object paramListObject, M model)
        //    where R : WARTBase
        //{
        //    using (HttpClient client = GetHttpClient())
        //    {
        //        string url = BuildUrl(controler, action, paramListObject);
        //        // Call the method
        //        HttpResponseMessage response = client.PostAsJsonAsync<M>(url, model).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Convert the result into business object
        //            var result = response.Content.ReadAsAsync<ServiceResult<R>>().Result;
        //            //var result = response.Content.ReadAsAsync<R>().Result;
        //            //return result;
        //            if (result.Success)
        //            {
        //                return result.Data;
        //            }

        //            else
        //            {
        //                var c_K_DT_Sys = result.Data.c_K_DT_Sys;
        //                var exception = result.Exception;
        //                var clientException = new ClientException(c_K_DT_Sys, exception);
        //                throw clientException;
        //            }

        //        }
        //        else
        //        {
        //            var c_K_DT_Sys = new c_K_DT_Sys();
        //            var exception = new Exception("HttpClient error");
        //            var clientException = new ClientException(c_K_DT_Sys, exception);
        //            throw clientException;
        //        }
        //    }

        //}

        protected R PostData<R, M>(string controler, string action, object paramListObject, M model, string addressAPIs)
            where R : WARTBase
        {
            using (HttpClient client = GetHttpClient(addressAPIs))
            {
                string url = BuildUrl(controler, action, paramListObject);
                // Call the method
                HttpResponseMessage response = client.PostAsJsonAsync<M>(url, model).Result;
                //var response1 = client.PutAsJsonAsync(url, model).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Convert the result into business object
                    var result = response.Content.ReadAsAsync<ServiceResult<R>>().Result;
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

        protected dynamic PutData<R, M>(string controler, string action, object paramListObject, M model, string addressAPIs)
            //where R : WARTBase
        {
            using (HttpClient client = GetHttpClient(addressAPIs))
            {
                string url = BuildUrl1(controler, action, paramListObject);
                // Call the method
                var response = client.PutAsJsonAsync(url, model).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Convert the result into business object
                    var result = response.Content.ReadAsAsync<ServiceResult<R>>().Result;
                    if (result.Success)
                    {
                        var abc = result.Data;
                    }

                    return null;
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
    }


    public class ClientServiceBase1
    {

        protected string BaseServiceAddress
        {
            get
            {
                //var baseServiceAddress = ServiceAddress.BaseServiceAddress;
                var baseServiceAddress = "20190708";
                return baseServiceAddress;
            }
        }
        protected HttpClient GetHttpClient(string addressAPIs)
        {
            var client = new HttpClient { BaseAddress = new Uri(addressAPIs) };

            // Define the serialization used json/xml/ etc
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
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

        protected string[] Up_Move_File(string controler, string action, object paramListObject, RQ_File model, string addressAPIs)
        {
            using (HttpClient client = GetHttpClient(addressAPIs))
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

    public class ClientServicePayGate
    {
        public virtual string ApiControllerName { get; set; }
        protected string BasePayGateAPIAddress
        {
            get
            {
                var baseServiceAddress = ServiceAddress.BasePayGateAddress;
                return baseServiceAddress;
            }
        }
        protected HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ServiceAddress.BasePayGateAddress) };

            // Define the serialization used json/xml/ etc
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
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

        protected string PmtOrd_PaymentOrderVnp_Add(string controler, string action, object paramListObject, string model)
        {
            using (HttpClient client = GetHttpClient())
            {
                string url = BuildUrl(controler, action, paramListObject);
                // Call the method
                HttpResponseMessage response = client.PostAsJsonAsync<string>(url, model).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Convert the result into business object
                    var result = response.Content.ReadAsAsync<string>().Result;                    
                    return result;
                }
                else
                {
                    var result = "Có lỗi trong cập nhật dữ liệu";
                    return result;                    
                }
            }
        }
    }
}
