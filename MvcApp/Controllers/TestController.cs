using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcApp.Models;
using MvcApp.Services;
using System;
using System.Linq;
using System.Security.Claims;

namespace MvcApp.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly SubmissionService _subSvc;


        public TestController(ILogger<TestController> logger, SubmissionService submissionService)
        {
            _subSvc = submissionService;

            _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<SubmissionModel> Create(SubmissionModel submission)
        {
            submission.Created = submission.LastUpdated = DateTime.Now;
            submission.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            submission.UserName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _subSvc.Create(submission);
            }
            return RedirectToAction("Index");
        }
    }
}
