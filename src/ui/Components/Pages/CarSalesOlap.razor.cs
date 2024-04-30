using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace CourseWork.Components.Pages
{
    public partial class CarSalesOlap
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public AutoDealershipOLAPService AutoDealershipOLAPService { get; set; }

        protected IEnumerable<CourseWork.Models.AutoDealershipOLAP.CarSale> carSales;
        protected IEnumerable<CourseWork.Models.AutoDealershipOLAP.CarSale> carSalesSum;


        protected RadzenDataGrid<CourseWork.Models.AutoDealershipOLAP.CarSale> grid0;

        protected string search = "";

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            carSales = await AutoDealershipOLAPService.GetCarSales(new Query { Expand = "AutoDealership,Brand,Date1,Date" });
        }
        protected override async Task OnInitializedAsync()
        {
            await GetDataAsync();
        }

        private async Task GetDataAsync()
        {
            carSales = await AutoDealershipOLAPService.GetCarSales(new Query { Expand = "AutoDealership,Brand,Date1,Date" });
            carSalesSum = [await AutoDealershipOLAPService.GetCarSalesSummary()];
        }



        protected async Task EditRow(DataGridRowMouseEventArgs<CourseWork.Models.AutoDealershipOLAP.CarSale> args)
        {
            await DialogService.OpenAsync<EditCarSalesOlap>("Edit CarSale", new Dictionary<string, object> { { "Id", args.Data.Id } });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, CourseWork.Models.AutoDealershipOLAP.CarSale carSale)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await AutoDealershipOLAPService.DeleteCarSale(carSale.Id);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete CarSale"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await AutoDealershipOLAPService.ExportCarSalesToCSV(new Query
                {
                    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter) ? "true" : grid0.Query.Filter)}",
                    OrderBy = $"{grid0.Query.OrderBy}",
                    Expand = "AutoDealership,Brand,Date1,Date",
                    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
                }, "CarSales");
            }

            if (args == null || args.Value == "xlsx")
            {
                await AutoDealershipOLAPService.ExportCarSalesToExcel(new Query
                {
                    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter) ? "true" : grid0.Query.Filter)}",
                    OrderBy = $"{grid0.Query.OrderBy}",
                    Expand = "AutoDealership,Brand,Date1,Date",
                    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
                }, "CarSales");
            }
        }

        private Task UpdateFullDataButtonClick()
        {
            AutoDealershipOLAPService.UpdateOlapData();
            return GetDataAsync();
        }
    }
}