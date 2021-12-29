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

        public ViewResult GetFeeds()
        {
            var feedcoll=_subSvc.Read();
            return View(feedcoll.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<SubmissionModel> Create(SubmissionModel submission)
        {
            submission.Created = submission.LastUpdated = DateTime.Now;
            submission.UserId = "0232131232234";
            submission.UserName = "Anil";
            //submission.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            //ubmission.UserName = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _subSvc.Create(submission);
            }
            return RedirectToAction("GetFeeds");
        }
    }
}
