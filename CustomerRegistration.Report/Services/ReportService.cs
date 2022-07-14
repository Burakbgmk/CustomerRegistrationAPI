using CustomerRegistration.Core.Entities;
using CustomerRegistration.Data;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRegistration.Report.Services
{
    public class ReportService : IReportService
    {
        private readonly ExcelService<CustomerCountByCityReportDetail> _countExcelService;
        private readonly ExcelService<TopFiveCustomersByActivityReportDetail> _topFiveExcelService;
        private readonly AppDbContext _context;
        private readonly DbSet<CustomerCountByCityReport> _countReportSet;
        private readonly DbSet<TopFiveCustomersByActivityReport> _topFiveReportSet;
        private readonly DbSet<CustomerCountByCityReportDetail> _reportDetailSet;
        private readonly DbSet<TopFiveCustomersByActivityReportDetail> _topFiveDetailSet;
        public ReportService(AppDbContext context, ExcelService<CustomerCountByCityReportDetail> countExcelService, ExcelService<TopFiveCustomersByActivityReportDetail> topFiveExcelService)
        {
            _context = context;
            _countReportSet = _context.Set<CustomerCountByCityReport>();
            _topFiveReportSet = _context.Set<TopFiveCustomersByActivityReport>();
            _countExcelService = countExcelService;
            _reportDetailSet = _context.Set<CustomerCountByCityReportDetail>();
            _topFiveDetailSet = _context.Set<TopFiveCustomersByActivityReportDetail>();
            _topFiveExcelService = topFiveExcelService;
        }

        public async Task<Response<List<CustomerCountByCityReport>>> GetListOfCountByCityReports()
        {
            var list = await _countReportSet.ToListAsync();
            if (list == null)
                return Response<List<CustomerCountByCityReport>>.Fail("No report is found!",404,true);
            return Response<List<CustomerCountByCityReport>>.Success(list, 200);
        }

        public async Task<Response<NoDataDto>> DownloadCountByCityReports(int reportId, string header)
        {
            var list = await _reportDetailSet.ToListAsync();
            var report = list.Where(x => x.ReportId == reportId).ToList();
            await _countExcelService.ProcessExcel(report, header);
            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<List<TopFiveCustomersByActivityReport>>> GetListOfTopFiveByActivityReports()
        {
            var list = await _topFiveReportSet.ToListAsync();
            if (list == null)
                return Response<List<TopFiveCustomersByActivityReport>>.Fail("No report is found!", 404, true);
            return Response<List<TopFiveCustomersByActivityReport>>.Success(list, 200);
        }

        public async Task<Response<NoDataDto>> DownloadTopFiveByActivityReports(int reportId, string header)
        {
            var list = await _topFiveDetailSet.ToListAsync();
            var report = list.Where(x => x.ReportId == reportId).ToList();
            await _topFiveExcelService.ProcessExcel(report, header);
            return Response<NoDataDto>.Success(204);
        }

    }
}
