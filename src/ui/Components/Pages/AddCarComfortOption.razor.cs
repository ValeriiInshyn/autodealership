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
    public partial class AddCarComfortOption
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
            carComfortOption = new CourseWork.Models.AutoDealership.CarComfortOption();

            carsForCarId = await AutoDealershipService.GetCars();

            comfortOptionsForComfortOptionId = await AutoDealershipService.GetComfortOptions();
        }
        protected bool errorVisible;
        protected CourseWork.Models.AutoDealership.CarComfortOption carComfortOption;

        protected IEnumerable<CourseWork.Models.AutoDealership.Car> carsForCarId;

        protected IEnumerable<CourseWork.Models.AutoDealership.ComfortOption> comfortOptionsForComfortOptionId;

        protected async Task FormSubmit()
        {
            try
            {
                await AutoDealershipService.CreateCarComfortOption(carComfortOption);
                DialogService.Close(carComfortOption);
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