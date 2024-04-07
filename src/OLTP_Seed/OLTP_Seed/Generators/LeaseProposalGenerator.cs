using OLTP_Seed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTP_Seed.Generators
{
    public class LeaseProposalGenerator
    {
        private static Random random = new Random();
        private static List<string> descriptions = new List<string> { "Standard", "Premium", "Basic", "Exclusive" };

        public static LeaseProposal GenerateLeaseProposal(int id)
        {
            LeaseProposal proposal = new LeaseProposal();
            proposal.Id = id;
            proposal.LeaseTypeId = random.Next(1, 5); // Assuming there are 4 lease types
            proposal.InsuranceRequired = random.Next(2) == 0; // Randomly assign insurance requirement
            proposal.MonthlyPayment = Math.Round((decimal)(random.NextDouble() * 1000), 2); // Random monthly payment
            proposal.Description = descriptions[random.Next(descriptions.Count)]; // Random description
            proposal.CreateDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-random.Next(1, 30)));
            proposal.UpdateDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-random.Next(1, 10)));
            return proposal;
        }

        public static LeaseProposalCondition GenerateLeaseProposalCondition(int id, int proposalId)
        {
            LeaseProposalCondition condition = new LeaseProposalCondition();
            condition.Id = id;
            condition.LeaseProposalId = proposalId;
            condition.ConditionId = random.Next(1, 7); // Assuming there are 6 conditions
            condition.Value = random.Next(2) == 0; // Random boolean value
            condition.CreateDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-random.Next(1, 30)));
            condition.UpdateDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-random.Next(1, 10)));
            return condition;
        }
    }
}
