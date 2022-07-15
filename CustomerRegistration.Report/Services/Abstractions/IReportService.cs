using CustomerRegistration.Core.Entities;
using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Report.Services.Abstractions
{
    public interface IReportService
    {
        Task<Response<List<CustomerCountByCityReport>>> GetListOfCountByCityReports();
        Task<Response<NoDataDto>> DownloadCountByCityReports(int reportId, string header);
        Task<Response<List<TopFiveCustomersByActivityReport>>> GetListOfTopFiveByActivityReports();
        Task<Response<NoDataDto>> DownloadTopFiveByActivityReports(int reportId, string header);

    }
}
