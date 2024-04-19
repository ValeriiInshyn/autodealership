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
    public partial class EditCarSafetyOption
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
        public int CarId { get; set; }

        [Parameter]
        public int SafetyOptionId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            carSafetyOption = await AutoDealershipService.GetCarSafetyOptionByCarIdAndSafetyOptionId(CarId, SafetyOptionId);

            carsForCarId = await AutoDealershipService.GetCars();

            safetyOptionsForSafetyOptionId = await AutoDealershipService.GetSafetyOptions();
        }
        protected bool errorVisible;
        protected CourseWork.Models.AutoDealership.CarSafetyOption carSafetyOption;

        protected IEnumerable<CourseWork.Models.AutoDealership.Car> carsForCarId;

        protected IEnumerable<CourseWork.Models.AutoDealership.SafetyOption> safetyOptionsForSafetyOptionId;

        protected async Task FormSubmit()
        {
            try
            {
                await AutoDealershipService.UpdateCarSafetyOption(CarId, SafetyOptionId, carSafetyOption);
                DialogService.Close(carSafetyOption);
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

            carSafetyOption = await AutoDealershipService.GetCarSafetyOptionByCarIdAndSafetyOptionId(CarId, SafetyOptionId);
        }
    }
}