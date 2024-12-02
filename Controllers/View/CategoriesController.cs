using Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Application.Controllers.View
{

    public class CategoriesController : Controller
    {
        //private readonly ILogger<CategoriesViewController> _logger;

        //public CategoriesViewController(ILogger<CategoriesViewController> logger)
        //{
        //    _logger = logger;
        //}
        [HttpGet]
        [Route("Categories")]
        public ActionResult Index()
        {
            return View();
        }


    }
}
