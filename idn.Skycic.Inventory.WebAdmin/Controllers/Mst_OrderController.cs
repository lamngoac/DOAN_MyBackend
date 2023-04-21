using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.WebAdmin.Models;
using idn.Skycic.Inventory.WebAdmin.Utils;
using inos.common.Model;
using inos.common.Service;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    public class Mst_OrderController : BaseController
    {
        // GET: Mst_Order
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }

    }
}