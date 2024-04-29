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
    public partial class AddLeaseProposalCondition
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
            leaseProposalCondition = new CourseWork.Models.AutoDealership.LeaseProposalCondition();

            leaseProposalsForLeaseProposalId = await AutoDealershipService.GetLeaseProposals();

            conditionsForConditionId = await AutoDealershipService.GetConditions();
        }
        protected bool errorVisible;
        protected CourseWork.Models.AutoDealership.LeaseProposalCondition leaseProposalCondition;

        protected IEnumerable<CourseWork.Models.AutoDealership.LeaseProposal> leaseProposalsForLeaseProposalId;

        protected IEnumerable<CourseWork.Models.AutoDealership.Condition> conditionsForConditionId;

        protected async Task FormSubmit()
        {
            try
            {
                await AutoDealershipService.CreateLeaseProposalCondition(leaseProposalCondition);
                DialogService.Close(leaseProposalCondition);
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