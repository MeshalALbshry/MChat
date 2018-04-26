using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mohdthat.Controllers
{
    [Authorize]
    public class BroadcastController : Controller
    {
        //
        // GET: /Broadcast/
        public ActionResult Index()
        {
            return View();
        }
	}
}