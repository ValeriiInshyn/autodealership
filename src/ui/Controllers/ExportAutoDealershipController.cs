using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using CourseWork.Data;

namespace CourseWork.Controllers
{
    public partial class ExportAutoDealershipController : ExportController
    {
        private readonly AutoDealershipContext context;
        private readonly AutoDealershipService service;

        public ExportAutoDealershipController(AutoDealershipContext context, AutoDealershipService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/AutoDealership/autodealerships/csv")]
        [HttpGet("/export/AutoDealership/autodealerships/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAutoDealershipsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAutoDealerships(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/autodealerships/excel")]
        [HttpGet("/export/AutoDealership/autodealerships/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAutoDealershipsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAutoDealerships(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/brands/csv")]
        [HttpGet("/export/AutoDealership/brands/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBrandsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBrands(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/brands/excel")]
        [HttpGet("/export/AutoDealership/brands/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBrandsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBrands(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/carbodytypes/csv")]
        [HttpGet("/export/AutoDealership/carbodytypes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarBodyTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCarBodyTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/carbodytypes/excel")]
        [HttpGet("/export/AutoDealership/carbodytypes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarBodyTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCarBodyTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/carcomfortoptions/csv")]
        [HttpGet("/export/AutoDealership/carcomfortoptions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarComfortOptionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCarComfortOptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/carcomfortoptions/excel")]
        [HttpGet("/export/AutoDealership/carcomfortoptions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarComfortOptionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCarComfortOptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/cardeliveries/csv")]
        [HttpGet("/export/AutoDealership/cardeliveries/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarDeliveriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCarDeliveries(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/cardeliveries/excel")]
        [HttpGet("/export/AutoDealership/cardeliveries/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarDeliveriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCarDeliveries(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/carmultimediaoptions/csv")]
        [HttpGet("/export/AutoDealership/carmultimediaoptions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarMultimediaOptionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCarMultimediaOptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/carmultimediaoptions/excel")]
        [HttpGet("/export/AutoDealership/carmultimediaoptions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarMultimediaOptionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCarMultimediaOptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/cars/csv")]
        [HttpGet("/export/AutoDealership/cars/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCars(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/cars/excel")]
        [HttpGet("/export/AutoDealership/cars/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCars(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/carsafetyoptions/csv")]
        [HttpGet("/export/AutoDealership/carsafetyoptions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarSafetyOptionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCarSafetyOptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/carsafetyoptions/excel")]
        [HttpGet("/export/AutoDealership/carsafetyoptions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarSafetyOptionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCarSafetyOptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/carsales/csv")]
        [HttpGet("/export/AutoDealership/carsales/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarSalesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCarSales(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/carsales/excel")]
        [HttpGet("/export/AutoDealership/carsales/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarSalesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCarSales(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/cartypes/csv")]
        [HttpGet("/export/AutoDealership/cartypes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCarTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/cartypes/excel")]
        [HttpGet("/export/AutoDealership/cartypes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCarTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCarTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/cities/csv")]
        [HttpGet("/export/AutoDealership/cities/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCitiesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCities(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/cities/excel")]
        [HttpGet("/export/AutoDealership/cities/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCitiesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCities(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/colors/csv")]
        [HttpGet("/export/AutoDealership/colors/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColorsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetColors(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/colors/excel")]
        [HttpGet("/export/AutoDealership/colors/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportColorsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetColors(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/comfortoptions/csv")]
        [HttpGet("/export/AutoDealership/comfortoptions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportComfortOptionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetComfortOptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/comfortoptions/excel")]
        [HttpGet("/export/AutoDealership/comfortoptions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportComfortOptionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetComfortOptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/conditions/csv")]
        [HttpGet("/export/AutoDealership/conditions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportConditionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetConditions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/conditions/excel")]
        [HttpGet("/export/AutoDealership/conditions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportConditionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetConditions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/countries/csv")]
        [HttpGet("/export/AutoDealership/countries/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCountriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCountries(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/countries/excel")]
        [HttpGet("/export/AutoDealership/countries/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCountriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCountries(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/customers/csv")]
        [HttpGet("/export/AutoDealership/customers/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCustomersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCustomers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/customers/excel")]
        [HttpGet("/export/AutoDealership/customers/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCustomersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCustomers(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/dealershipcars/csv")]
        [HttpGet("/export/AutoDealership/dealershipcars/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDealershipCarsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDealershipCars(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/dealershipcars/excel")]
        [HttpGet("/export/AutoDealership/dealershipcars/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDealershipCarsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDealershipCars(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/dealershipcarstatuses/csv")]
        [HttpGet("/export/AutoDealership/dealershipcarstatuses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDealershipCarStatusesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDealershipCarStatuses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/dealershipcarstatuses/excel")]
        [HttpGet("/export/AutoDealership/dealershipcarstatuses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDealershipCarStatusesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDealershipCarStatuses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/distributors/csv")]
        [HttpGet("/export/AutoDealership/distributors/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDistributorsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDistributors(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/distributors/excel")]
        [HttpGet("/export/AutoDealership/distributors/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDistributorsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDistributors(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/employees/csv")]
        [HttpGet("/export/AutoDealership/employees/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmployeesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEmployees(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/employees/excel")]
        [HttpGet("/export/AutoDealership/employees/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEmployeesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEmployees(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/engines/csv")]
        [HttpGet("/export/AutoDealership/engines/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEnginesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEngines(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/engines/excel")]
        [HttpGet("/export/AutoDealership/engines/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEnginesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEngines(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/enginetypes/csv")]
        [HttpGet("/export/AutoDealership/enginetypes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEngineTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEngineTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/enginetypes/excel")]
        [HttpGet("/export/AutoDealership/enginetypes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEngineTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEngineTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/gearboxtypes/csv")]
        [HttpGet("/export/AutoDealership/gearboxtypes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGearBoxTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetGearBoxTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/gearboxtypes/excel")]
        [HttpGet("/export/AutoDealership/gearboxtypes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGearBoxTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetGearBoxTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/leaseproposalconditions/csv")]
        [HttpGet("/export/AutoDealership/leaseproposalconditions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLeaseProposalConditionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLeaseProposalConditions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/leaseproposalconditions/excel")]
        [HttpGet("/export/AutoDealership/leaseproposalconditions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLeaseProposalConditionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLeaseProposalConditions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/leaseproposals/csv")]
        [HttpGet("/export/AutoDealership/leaseproposals/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLeaseProposalsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLeaseProposals(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/leaseproposals/excel")]
        [HttpGet("/export/AutoDealership/leaseproposals/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLeaseProposalsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLeaseProposals(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/leases/csv")]
        [HttpGet("/export/AutoDealership/leases/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLeasesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLeases(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/leases/excel")]
        [HttpGet("/export/AutoDealership/leases/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLeasesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLeases(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/leasetypes/csv")]
        [HttpGet("/export/AutoDealership/leasetypes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLeaseTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLeaseTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/leasetypes/excel")]
        [HttpGet("/export/AutoDealership/leasetypes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportLeaseTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLeaseTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/multimediaoptions/csv")]
        [HttpGet("/export/AutoDealership/multimediaoptions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMultimediaOptionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMultimediaOptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/multimediaoptions/excel")]
        [HttpGet("/export/AutoDealership/multimediaoptions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMultimediaOptionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMultimediaOptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/paymentmethods/csv")]
        [HttpGet("/export/AutoDealership/paymentmethods/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPaymentMethodsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetPaymentMethods(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/paymentmethods/excel")]
        [HttpGet("/export/AutoDealership/paymentmethods/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPaymentMethodsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetPaymentMethods(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/safetyoptions/csv")]
        [HttpGet("/export/AutoDealership/safetyoptions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSafetyOptionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSafetyOptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/safetyoptions/excel")]
        [HttpGet("/export/AutoDealership/safetyoptions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSafetyOptionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSafetyOptions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/salestatuses/csv")]
        [HttpGet("/export/AutoDealership/salestatuses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSaleStatusesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSaleStatuses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/AutoDealership/salestatuses/excel")]
        [HttpGet("/export/AutoDealership/salestatuses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSaleStatusesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSaleStatuses(), Request.Query, false), fileName);
        }
    }
}
