using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using CourseWork.Data;

namespace CourseWork.Controllers
{
    public partial class ExportAutoDealershipOLAPController : ExportController
    {
        private readonly AutoDealershipOLAPContext context;
        private readonly AutoDealershipOLAPService service;

        public ExportAutoDealershipOLAPController(AutoDealershipOLAPContext context, AutoDealershipOLAPService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/AutoDealershipOLAP/autodealerships/csv")]
        [HttpGet("/export/AutoDealershipOLAP/autodealerships/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAutoDealershipsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAutoDealerships(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealershipOLAP/autodealerships/excel")]
        [HttpGet("/export/AutoDealershipOLAP/autodealerships/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAutoDealershipsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAutoDealerships(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealershipOLAP/brands/csv")]
        [HttpGet("/export/AutoDealershipOLAP/brands/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBrandsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBrands(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealershipOLAP/brands/excel")]
        [HttpGet("/export/AutoDealershipOLAP/brands/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBrandsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBrands(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealershipOLAP/cars/csv")]
        [HttpGet("/export/AutoDealershipOLAP/cars/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCars(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealershipOLAP/cars/excel")]
        [HttpGet("/export/AutoDealershipOLAP/cars/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCars(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealershipOLAP/carsales/csv")]
        [HttpGet("/export/AutoDealershipOLAP/carsales/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarSalesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCarSales(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealershipOLAP/carsales/excel")]
        [HttpGet("/export/AutoDealershipOLAP/carsales/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarSalesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCarSales(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealershipOLAP/dates/csv")]
        [HttpGet("/export/AutoDealershipOLAP/dates/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDatesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDates(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealershipOLAP/dates/excel")]
        [HttpGet("/export/AutoDealershipOLAP/dates/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDatesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDates(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealershipOLAP/leases/csv")]
        [HttpGet("/export/AutoDealershipOLAP/leases/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLeasesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLeases(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealershipOLAP/leases/excel")]
        [HttpGet("/export/AutoDealershipOLAP/leases/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLeasesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLeases(), Request.Query, false), fileName);
        }
    }
}
