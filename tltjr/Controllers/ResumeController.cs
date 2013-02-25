using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tltjr.Controllers
{
    public class ResumeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public FileResult Pdf()
        {
            return File("~/Resumes/thornton-resume.pdf", "application/pdf", "thornton-resume.pdf");
        }

        public FileResult Docx()
        {
            return File("~/Resumes/thornton-resume.docx",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "thornton-resume.docx");
        }
    }
}
