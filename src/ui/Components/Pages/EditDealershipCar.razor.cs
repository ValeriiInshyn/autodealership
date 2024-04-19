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
    public partial class EditDealershipCar
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

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            dealershipCar = await AutoDealershipService.GetDealershipCarById(Id);

            autoDealershipsForDealershipId = await AutoDealershipService.GetAutoDealerships();

            dealershipCarStatusesForCarStatusId = await AutoDealershipService.GetDealershipCarStatuses();
        }
        protected bool errorVisible;
        protected CourseWork.Models.AutoDealership.DealershipCar dealershipCar;

        protected IEnumerable<CourseWork.Models.AutoDealership.AutoDealership> autoDealershipsForDealershipId;

        protected IEnumerable<CourseWork.Models.AutoDealership.DealershipCarStatus> dealershipCarStatusesForCarStatusId;

        protected async Task FormSubmit()
        {
            try
            {
                await AutoDealershipService.UpdateDealershipCar(Id, dealershipCar);
                DialogService.Close(dealershipCar);
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


        protected async Task ReloadButtonClick(MouseEventArgs args)
        {
           AutoDealershipService.Reset();
            hasChanges = false;
            canEdit = true;

            dealershipCar = await AutoDealershipService.GetDealershipCarById(Id);
        }
    }
}