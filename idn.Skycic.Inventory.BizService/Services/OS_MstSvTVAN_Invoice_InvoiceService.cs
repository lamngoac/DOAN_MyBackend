using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUtils = idn.Skycic.Inventory.Utils;
using TConst = idn.Skycic.Inventory.Constants;

namespace idn.Skycic.Inventory.BizService.Services
{
	public class OS_MstSvTVAN_Invoice_InvoiceService : ClientServiceBase<OS_MstSvTVAN_Invoice_InvoiceService>
	{
		public static OS_MstSvTVAN_Invoice_InvoiceService Instance
		{
			get
			{
				return GetInstance<OS_MstSvTVAN_Invoice_InvoiceService>();
			}
		}

		public override string ApiControllerName
		{
			get
			{
				return "InvoiceInvoice";
			}
		}

		public RT_Invoice_Invoice WA_Invoice_Invoice_Get(string strNetWorkUrl, RQ_Invoice_Invoice objRQ_Invoice_Invoice)
		{
			var result = MstSvRoute_PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>(strNetWorkUrl, "InvoiceInvoice", "WA_Invoice_Invoice_Get", new { }, objRQ_Invoice_Invoice);
			return result;
		}

		public RT_Invoice_Invoice WA_Invoice_Invoice_GetNoSession(string strNetWorkUrl, RQ_Invoice_Invoice objRQ_Invoice_Invoice)
		{
			var result = MstSvRoute_PostData<RT_Invoice_Invoice, RQ_Invoice_Invoice>(strNetWorkUrl, "InvoiceInvoice", "WA_Invoice_Invoice_GetNoSession", new { }, objRQ_Invoice_Invoice);
			return result;
		}
	}
}
