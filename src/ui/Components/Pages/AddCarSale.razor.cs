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
    public partial class AddCarSale
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
            carSale = new CourseWork.Models.AutoDealership.CarSale();

            dealershipCarsForDealershipCarId = await AutoDealershipService.GetDealershipCars();

            customersForCustomerId = await AutoDealershipService.GetCustomers();

            saleStatusesForStatusId = await AutoDealershipService.GetSaleStatuses();

            employeesForEmployeeId = await AutoDealershipService.GetEmployees();

            paymentMethodsForPaymentMethodId = await AutoDealershipService.GetPaymentMethods();
        }
        protected bool errorVisible;
        protected CourseWork.Models.AutoDealership.CarSale carSale;

        protected IEnumerable<CourseWork.Models.AutoDealership.DealershipCar> dealershipCarsForDealershipCarId;

        protected IEnumerable<CourseWork.Models.AutoDealership.Customer> customersForCustomerId;

        protected IEnumerable<CourseWork.Models.AutoDealership.SaleStatus> saleStatusesForStatusId;

        protected IEnumerable<CourseWork.Models.AutoDealership.Employee> employeesForEmployeeId;

        protected IEnumerable<CourseWork.Models.AutoDealership.PaymentMethod> paymentMethodsForPaymentMethodId;

        protected async Task FormSubmit()
        {
            try
            {
                await AutoDealershipService.CreateCarSale(carSale);
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
    }
}