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
    public partial class AddCarDelivery
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
            carDelivery = new CourseWork.Models.AutoDealership.CarDelivery();

            carSalesForSaleId = await AutoDealershipService.GetCarSales();

            distributorsForDistributorId = await AutoDealershipService.GetDistributors();
        }
        protected bool errorVisible;
        protected CourseWork.Models.AutoDealership.CarDelivery carDelivery;

        protected IEnumerable<CourseWork.Models.AutoDealership.CarSale> carSalesForSaleId;

        protected IEnumerable<CourseWork.Models.AutoDealership.Distributor> distributorsForDistributorId;

        protected async Task FormSubmit()
        {
            try
            {
                await AutoDealershipService.CreateCarDelivery(carDelivery);
                DialogService.Close(carDelivery);
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