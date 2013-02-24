using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tltjr.Models;
using tltjr.Data;

namespace tltjr.Controllers
{
    public class ContactController : Controller
    {
        private EmailProvider _emailProvider = new EmailProvider();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send(ContactModel model)
        {
            var success = _emailProvider.TrySendEmail(model);
            return View("Index");
        }
    }
}
