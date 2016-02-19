using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClaimsWebApi.Models;
using ClaimsWebApi.Services.Contracts;
using System.Threading.Tasks;

namespace ClaimsWebApi.Controllers
{
    [RoutePrefix("api/claims")]
    public class ClaimsController : ApiController
    {
        private readonly IClaimsService _claimsService;

        public ClaimsController(IClaimsService claimsService)
        {
            _claimsService = claimsService;
        }
        async public Task<IHttpActionResult> Post(MitchellClaimType claim)
        {
            return Ok(await _claimsService.CreateClaim(claim));
        }

        async public Task<IHttpActionResult> Get()
        {
            var claim = await _claimsService.ListClaimsAsync();
            if (claim == null)
                return NotFound();
            return Ok(claim);
        }

        [Route("{ClaimNumber}")]
        async public Task<IHttpActionResult> GetClaimInfo(string ClaimNumber)
        {
            var claim = await _claimsService.ListClaimsAsync(ClaimNumber);
            if (claim == null)
                return NotFound();
            return Ok(claim);
        }
        [Route("{LossFromDate}/{LossToDate}")]
        async public Task<IHttpActionResult> GetClaimsByLosDateRange(string LossFromDate, string LossToDate)
        {
            DateTime tryLosFromDate = new DateTime();
            DateTime tryLosToDate = new DateTime();
            if (!DateTime.TryParse(LossFromDate, out tryLosFromDate) || !DateTime.TryParse(LossToDate, out tryLosToDate))
                return BadRequest("Either LossFromDate or LossToDate not valid");

            var claims = await _claimsService.ListClaimsAsync(LossFromDate, LossToDate);
            if (claims == null)
                return NotFound();
            return Ok(claims);
        }

        async public Task<IHttpActionResult> Put(MitchellClaimType claim)
        {
            return Ok(await _claimsService.UpdateClaim(claim));
            
        }
        async public Task<IHttpActionResult> Delete(string ClaimNumber)
        {
            return Ok(await _claimsService.DeleteClaimAsync(ClaimNumber));
        }
    }
}
