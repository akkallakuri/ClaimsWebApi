using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimsWebApi.Services.Contracts;
using ClaimsWebApi.Models;
using ClaimsWebApi.Data.Contracts;
using ClaimsWebApi.Data;

namespace ClaimsWebApi.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IClaimsRepository _claimsRepository;

        public ClaimsService(IClaimsRepository claimsRepository)
        {
            _claimsRepository = claimsRepository;
        }
        async public Task<string> CreateClaim(MitchellClaimType claim)
        {
            return await _claimsRepository.AddAsync(claim);
        }

        async public Task<IEnumerable<MitchellClaimType>> ListClaimsAsync()
        {
            return await _claimsRepository.ListAllAsync(Commands.GetAllClaims);
        }

        async public Task<IEnumerable<MitchellClaimType>> ListClaimsAsync(string fromLossDate, string toLossDate)
        {
            return await _claimsRepository.ListAllAsync(string.Format(Commands.GetClaimsByLossDateRange, fromLossDate, toLossDate));
        }

        async public Task<IEnumerable<MitchellClaimType>> ListClaimsAsync(string ClaimNumber)
        {
            return await _claimsRepository.ListAllAsync(string.Format(Commands.GetClaimByClaimNumber, ClaimNumber));
        }
        async public Task<string> DeleteClaimAsync(string ClaimNumber)
        {
            return await _claimsRepository.DeleteAsync(ClaimNumber);
        }

        async public Task<string> UpdateClaim(MitchellClaimType claim)
        {
            return await _claimsRepository.UpdateAsync(claim);
        }
    }
}
