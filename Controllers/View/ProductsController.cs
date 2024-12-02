using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.View
{
    public class ProductsController : Controller
    {
        [HttpGet]
        [Route("Products")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddProduct()
        {
            return View();
        }
    }
}
