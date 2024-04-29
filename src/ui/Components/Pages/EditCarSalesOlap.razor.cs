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
    public partial class EditCarSalesOlap
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

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            carSale = await AutoDealershipOLAPService.GetCarSaleById(Id);

            autoDealershipsForAutoDealershipId = await AutoDealershipOLAPService.GetAutoDealerships();

            brandsForBrandId = await AutoDealershipOLAPService.GetBrands();

            datesForStartDateId = await AutoDealershipOLAPService.GetDates();

            datesForEndDateId = await AutoDealershipOLAPService.GetDates();
        }
        protected bool errorVisible;
        protected CourseWork.Models.AutoDealershipOLAP.CarSale carSale;

        protected IEnumerable<CourseWork.Models.AutoDealershipOLAP.AutoDealership> autoDealershipsForAutoDealershipId;

        protected IEnumerable<CourseWork.Models.AutoDealershipOLAP.Brand> brandsForBrandId;

        protected IEnumerable<CourseWork.Models.AutoDealershipOLAP.Date> datesForStartDateId;

        protected IEnumerable<CourseWork.Models.AutoDealershipOLAP.Date> datesForEndDateId;

        protected async Task FormSubmit()
        {
            try
            {
                await AutoDealershipOLAPService.UpdateCarSale(Id, carSale);
                DialogService.Close(carSale);
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
           AutoDealershipOLAPService.Reset();
            hasChanges = false;
            canEdit = true;

            carSale = await AutoDealershipOLAPService.GetCarSaleById(Id);
        }
    }
}