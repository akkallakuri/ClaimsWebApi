using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimsWebApi.Models;

namespace ClaimsWebApi.Data.Contracts
{
    public interface IClaimsRepository : IRepositoryAsync<MitchellClaimType>
    {
    }
}
