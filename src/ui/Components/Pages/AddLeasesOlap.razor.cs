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
    public partial class AddLeasesOlap
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

        protected override async Task OnInitializedAsync()
        {
            lease = new CourseWork.Models.AutoDealershipOLAP.Lease();

            carsForCarId = await AutoDealershipOLAPService.GetCars();

            datesForLeaseSignDateId = await AutoDealershipOLAPService.GetDates();

            datesForLeaseStartDateId = await AutoDealershipOLAPService.GetDates();

            datesForLeaseEndDateId = await AutoDealershipOLAPService.GetDates();
        }
        protected bool errorVisible;
        protected CourseWork.Models.AutoDealershipOLAP.Lease lease;

        protected IEnumerable<CourseWork.Models.AutoDealershipOLAP.Car> carsForCarId;

        protected IEnumerable<CourseWork.Models.AutoDealershipOLAP.Date> datesForLeaseSignDateId;

        protected IEnumerable<CourseWork.Models.AutoDealershipOLAP.Date> datesForLeaseStartDateId;

        protected IEnumerable<CourseWork.Models.AutoDealershipOLAP.Date> datesForLeaseEndDateId;

        protected async Task FormSubmit()
        {
            try
            {
                await AutoDealershipOLAPService.CreateLease(lease);
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