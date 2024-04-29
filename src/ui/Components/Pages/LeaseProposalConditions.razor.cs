using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Services;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace CourseWork.Components.Pages
{
    public partial class LeaseProposalConditions
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
        public AutoDealershipService AutoDealershipService { get; set; }

        protected IEnumerable<CourseWork.Models.AutoDealership.LeaseProposalCondition> leaseProposalConditions;

        protected RadzenDataGrid<CourseWork.Models.AutoDealership.LeaseProposalCondition> grid0;

        protected string search = "";

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            leaseProposalConditions = await AutoDealershipService.GetLeaseProposalConditions(new Query { Expand = "LeaseProposal,Condition" });
        }
        protected override async Task OnInitializedAsync()
        {
            leaseProposalConditions = await AutoDealershipService.GetLeaseProposalConditions(new Query { Expand = "LeaseProposal,Condition" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddLeaseProposalCondition>("Add LeaseProposalCondition", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<CourseWork.Models.AutoDealership.LeaseProposalCondition> args)
        {
            await DialogService.OpenAsync<EditLeaseProposalCondition>("Edit LeaseProposalCondition", new Dictionary<string, object> { {"Id", args.Data.Id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, CourseWork.Models.AutoDealership.LeaseProposalCondition leaseProposalCondition)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await AutoDealershipService.DeleteLeaseProposalCondition(leaseProposalCondition.Id);

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
                    Detail = $"Unable to delete LeaseProposalCondition"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await AutoDealershipService.ExportLeaseProposalConditionsToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "LeaseProposal,Condition",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "LeaseProposalConditions");
            }

            if (args == null || args.Value == "xlsx")
            {
                await AutoDealershipService.ExportLeaseProposalConditionsToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "LeaseProposal,Condition",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "LeaseProposalConditions");
            }
        }
    }
}