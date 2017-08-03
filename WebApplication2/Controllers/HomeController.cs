using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //using (var db = new ApplicationDbContext())
            //{
            //    var student = db.Students.First();

            //    //db.Dispose();
            //}

            //string[] meseci = { "januar", "februar", "mart", "april", "maj", "jun" };

            //var filtriraniMeseci = meseci.Where(mesec => mesec.Length < 5);

            //var poredjaniMeseci = filtriraniMeseci.OrderBy(x => x.Length);
            //var poredjaniMeseciDesc = filtriraniMeseci.OrderByDescending(x => x.Length);

            int[] brojevi = { 1, 2, 3, 66, 72, 5, 11, 13 };

            var manjiod10 = brojevi.Where(b => b < 10);
            var sumamanjiod10 = manjiod10.Sum();

            var suma = brojevi.Sum();
            var sredina = brojevi.Average();


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Broj = 20;
            ViewBag.Student = new Student() { Name = "Zeljko", Surname = "Kalezic" };

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //RouteTest(int? year, string category = "web programiranje")
        public ActionResult RouteTest(int? year, string category = "web programiranje")
        {
            return Content(string.Format("{0}/{1}", year, category));
        }

        [Route("FreeWebProgrammingCourse/{year}/{category:length(1,5)}", Order = 2)]
        [Route("FreeProgrammingCourse/{year:int}/{category?}", Order = 1)]
        public ActionResult AttributeRouteTest(int? year, string category)
        {
            return Content(string.Format("{0}/{1}", year, category));
        }

        public ActionResult GetImage()
        {
            return File(Server.MapPath("~/Content/Images/nature.jpg"), MimeMapping.GetMimeMapping("nature.jpg"), "slika.jpg");
        }
    }
}