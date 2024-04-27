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
    public partial class EditDatesOlap
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
            date = await AutoDealershipOLAPService.GetDateById(Id);
        }
        protected bool errorVisible;
        protected CourseWork.Models.AutoDealershipOLAP.Date date;

        protected async Task FormSubmit()
        {
            try
            {
                await AutoDealershipOLAPService.UpdateDate(Id, date);
                DialogService.Close(date);
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

            date = await AutoDealershipOLAPService.GetDateById(Id);
        }
    }
}