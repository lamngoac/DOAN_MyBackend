using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Email_BatchSendEmail : WARQBase
	{
		public Email_BatchSendEmail Email_BatchSendEmail { get; set; }

		public List<Email_BatchSendEmailTo> Lst_Email_BatchSendEmailTo { get; set; }

		public List<Email_BatchSendEmailCC> Lst_Email_BatchSendEmailCC { get; set; }

		public List<Email_BatchSendEmailBCC> Lst_Email_BatchSendEmailBCC { get; set; }

		public List<Email_BatchSendEmailFileAttach> Lst_Email_BatchSendEmailFileAttach { get; set; }
	}
}
