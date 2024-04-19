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
    public partial class AddLease
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

        protected override async Task OnInitializedAsync()
        {
            lease = new CourseWork.Models.AutoDealership.Lease();

            employeesForEmployeeId = await AutoDealershipService.GetEmployees();

            leaseProposalsForProposalId = await AutoDealershipService.GetLeaseProposals();

            customersForCustomerId = await AutoDealershipService.GetCustomers();

            dealershipCarsForDealershipCarId = await AutoDealershipService.GetDealershipCars();
        }
        protected bool errorVisible;
        protected CourseWork.Models.AutoDealership.Lease lease;

        protected IEnumerable<CourseWork.Models.AutoDealership.Employee> employeesForEmployeeId;

        protected IEnumerable<CourseWork.Models.AutoDealership.LeaseProposal> leaseProposalsForProposalId;

        protected IEnumerable<CourseWork.Models.AutoDealership.Customer> customersForCustomerId;

        protected IEnumerable<CourseWork.Models.AutoDealership.DealershipCar> dealershipCarsForDealershipCarId;

        protected async Task FormSubmit()
        {
            try
            {
                await AutoDealershipService.CreateLease(lease);
                DialogService.Close(lease);
            }
            catch (Exception ex)
            {
                hasChanges = ex is Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException;
                canEdit = !(ex is Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException);
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }


        protected bool hasChanges = false;
        protected bool canEdit = true;
    }
}