using CustomerRegistration.Core.Entities;
using CustomerRegistration.Report.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : CustomBaseController
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet("/[action]")]
        public async Task<IActionResult> CountListWithDates()
        {
            var response = await _reportService.GetListOfCountByCityReports();
            return ActionResultInstance(response);
        }
        [HttpGet("/[action]")]
        public async Task<IActionResult> TopFiveListWithDates()
        {
            var response = await _reportService.GetListOfTopFiveByActivityReports();
            return ActionResultInstance(response);
        }

        [HttpPost("/[action]")]
        public async Task<IActionResult> DownloadCountReport(int reportId)
        {
            var header = "Report";
            var response = await _reportService.DownloadCountByCityReports(reportId, header);
            return ActionResultInstance(response);
        }

        [HttpPost("/[action]")]
        public async Task<IActionResult> DownloadTopFiveReport(int reportId)
        {
            var header = "Report";
            var response = await _reportService.DownloadTopFiveByActivityReports(reportId, header);
            return ActionResultInstance(response);
        }
    }
}
