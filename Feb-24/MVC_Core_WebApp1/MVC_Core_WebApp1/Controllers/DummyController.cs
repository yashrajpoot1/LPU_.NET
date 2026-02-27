using Microsoft.AspNetCore.Mvc;

namespace MVC_Core_WebApp1.Controllers
{
    public class DummyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Dummy()
        {
            return View();
        }

        public ActionResult CallError()
        {
            throw new Exception("Custom");
        }


        public ActionResult DoDivision(int num1, int num2)
        {
            float res = 0;
            try
            {
                res = (float)num1 / num2;
            }
            catch(DivideByZeroException e)
            {
                throw e;
            }
            finally
            {
                ViewBag.Message = res;
            }
            return View();
        }
    }
}
