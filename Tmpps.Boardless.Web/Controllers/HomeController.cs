using Microsoft.AspNetCore.Mvc;

namespace Tmpps.Boardless.Web.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "welcome boardless";
        }
    }
}