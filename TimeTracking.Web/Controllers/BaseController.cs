using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TimeTracking.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

        
    }
}
