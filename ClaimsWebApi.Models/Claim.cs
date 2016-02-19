using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsWebApi.Models
{
    public class Claim
    {
        public string ClaimNumber { get; set; }
        public string ClaimantFirstName { get; set; }
        public string ClaimantLastName { get; set; }
        public int StatusKey { get; set; }
        public DateTime LossDate { get; set; }
        public int LossInfoKey { get; set; }
        public int AssignedAdjusterID { get; set; }

        public string LossDescription { get; set; }
        public DateTime ReportDate { get; set; }
        public int CauseOfLossKey { get; set; }

    }
}
