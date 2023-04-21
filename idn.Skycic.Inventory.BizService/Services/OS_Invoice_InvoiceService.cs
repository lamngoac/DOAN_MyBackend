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
	public class OS_Invoice_InvoiceService : ClientServiceBase<OS_Invoice_InvoiceService>
	{
		public static OS_Invoice_InvoiceService Instance
		{
			get
			{
				return GetInstance<OS_Invoice_InvoiceService>();
			}
		}

		public override string ApiControllerName
		{
			get
			{
				return "MstBrand";
			}
		}
	}
}
