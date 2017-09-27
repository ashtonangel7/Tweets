namespace Tweets.Controllers
{
    using Models;
    using System;
    using System.Web.Mvc;
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            ErrorModel model = new ErrorModel();

            if (HttpContext.Items.Contains("Error"))
            {
                Exception ex = (Exception)HttpContext.Items["Error"];
                model.Message = ex.Message + " " + ex.InnerException?.Message;
            }

            return View(model);
        }
    }
}