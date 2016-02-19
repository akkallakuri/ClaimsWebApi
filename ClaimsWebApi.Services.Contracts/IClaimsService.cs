using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimsWebApi.Models;

namespace ClaimsWebApi.Services.Contracts
{
    public interface IClaimsService
    {
        Task<string> CreateClaim(MitchellClaimType claim);
        Task<IEnumerable<MitchellClaimType>> ListClaimsAsync();
        Task<IEnumerable<MitchellClaimType>> ListClaimsAsync(string fromLossDate, string toLossDate);
        Task<IEnumerable<MitchellClaimType>> ListClaimsAsync(string ClaimNumber);
        Task<string> DeleteClaimAsync(string ClaimNumber);
        Task<string> UpdateClaim(MitchellClaimType claim);
    }
}
