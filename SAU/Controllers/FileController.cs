using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SAU.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Files/");
            var file = dir.GetFiles().ToList();
            return View(file);
        }

        public ActionResult List()
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Files/");
            var file = dir.GetFiles().ToList();
            return PartialView("_List", file);
        }

        public ActionResult Dowload(string Name)
        {
            var FileVirtualPath = AppDomain.CurrentDomain.BaseDirectory + "Files/" + Name;
            return File(FileVirtualPath, "application / force - download", Path.GetFileName(FileVirtualPath));
        }

        public ActionResult Delete(string Name)
        {
            var FileVirtualPath = AppDomain.CurrentDomain.BaseDirectory + "Files/" + Name;
            if (System.IO.File.Exists(FileVirtualPath))
            {
                System.IO.File.Delete(FileVirtualPath);
            }
           return View("Index");
        }
    }
}