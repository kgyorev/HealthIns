using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HealthIns.Web.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace HealthIns.Web.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        private const string MESSAGE = "Unexpected Status Code: {0}, OriginalPath: {1}";
        private readonly ILogger<HomeController> _logger;

        public ErrorController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: /<controller>/
        [HttpGet("/Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            string message = string.Format(MESSAGE, statusCode, reExecute.OriginalPath);
            _logger.LogInformation(message);
            return View(statusCode);
        }
    }
}
