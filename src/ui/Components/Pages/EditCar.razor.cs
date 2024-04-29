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
    public partial class EditCar
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
            car = await AutoDealershipService.GetCarById(Id);

            colorsForColorId = await AutoDealershipService.GetColors();

            carBodyTypesForBodyTypeId = await AutoDealershipService.GetCarBodyTypes();

            enginesForEngineId = await AutoDealershipService.GetEngines();

            brandsForBrandId = await AutoDealershipService.GetBrands();

            gearBoxTypesForGearBoxId = await AutoDealershipService.GetGearBoxTypes();
        }
        protected bool errorVisible;
        protected CourseWork.Models.AutoDealership.Car car;

        protected IEnumerable<CourseWork.Models.AutoDealership.Color> colorsForColorId;

        protected IEnumerable<CourseWork.Models.AutoDealership.CarBodyType> carBodyTypesForBodyTypeId;

        protected IEnumerable<CourseWork.Models.AutoDealership.Engine> enginesForEngineId;

        protected IEnumerable<CourseWork.Models.AutoDealership.Brand> brandsForBrandId;

        protected IEnumerable<CourseWork.Models.AutoDealership.GearBoxType> gearBoxTypesForGearBoxId;

        protected async Task FormSubmit()
        {
            try
            {
                await AutoDealershipService.UpdateCar(Id, car);
                DialogService.Close(car);
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

            car = await AutoDealershipService.GetCarById(Id);
        }
    }
}