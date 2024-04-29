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
    public partial class AddCarMultimediaOption
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
            carMultimediaOption = new CourseWork.Models.AutoDealership.CarMultimediaOption();

            carsForCarId = await AutoDealershipService.GetCars();

            multimediaOptionsForMultimediaOptionId = await AutoDealershipService.GetMultimediaOptions();
        }
        protected bool errorVisible;
        protected CourseWork.Models.AutoDealership.CarMultimediaOption carMultimediaOption;

        protected IEnumerable<CourseWork.Models.AutoDealership.Car> carsForCarId;

        protected IEnumerable<CourseWork.Models.AutoDealership.MultimediaOption> multimediaOptionsForMultimediaOptionId;

        protected async Task FormSubmit()
        {
            try
            {
                await AutoDealershipService.CreateCarMultimediaOption(carMultimediaOption);
                DialogService.Close(carMultimediaOption);
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