using Microsoft.AspNetCore.Mvc;
using UMS.Application.Interfaces;

namespace UMS.Controllers
{
    /// <summary>
    /// Controller for managing enrollment operations
    /// </summary>
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            return View(enrollments);
        }
    }
}
