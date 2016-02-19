using System;
using ClaimsWebApi.Controllers;
using ClaimsWebApi.Data.Contracts;
using ClaimsWebApi.Data;
using ClaimsWebApi.Services;
using ClaimsWebApi.Services.Contracts;
using ClaimsWebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Web.Http.Results;

namespace ClaimsWebApi.Tests.Controllers
{
    [TestClass]
    public class ClaimsControllerTest
    {
        IClaimsRepository claimRepository = null;
        IClaimsService claimService = null;
        ClaimsController controller = null;

        public ClaimsControllerTest()
        {
            claimRepository = new ClaimsRepository();
            claimService = new ClaimsService(claimRepository);
            controller = new ClaimsController(claimService);
        }

        [TestMethod]
        async public Task GetAllClaims()
        {
            var result = await claimService.ListClaimsAsync();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        async public Task GetClaimsLossDateRange()
        {
            var result = await claimService.ListClaimsAsync("", "");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        async public Task DeleteClaim()
        {
            var result = await claimService.DeleteClaimAsync("22c9c23bac142856018ce14a26b6c2991");
            Assert.AreEqual("Success", result);
        }
        
        [TestMethod]
        async public Task UpdateClaim()
        {
            var claim = new MitchellClaimType();
            claim.ClaimNumber = "22c9c23bac142856018ce14a26b6c2991";
            claim.ClaimantFirstName = "George Anil";
            claim.ClaimantLastName = "Washington";
            claim.Status = StatusCode.OPEN;
            claim.LossDate = Convert.ToDateTime("2014-07-09T17:19:13.631-07:00");
            claim.LossInfo = new LossInfoType()
            {
                CauseOfLoss = CauseOfLossCode.Collision,
                ReportedDate = Convert.ToDateTime("2014-07-10T17:19:13.676-07:00"),
                LossDescription = "Crashed into an apple tree"
            };
            claim.AssignedAdjusterID = 23424;

            var result = await claimService.UpdateClaim(claim);
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public async Task GetByClaimNumber()
        {
            var result = await claimService.ListClaimsAsync("22c9c23bac142856018ce14a26b6c2991");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task  CreateClaim()
        {
            var claim = new MitchellClaimType();
            claim.ClaimNumber = "22c9c23bac142856018ce14a26b6c2991";
            claim.ClaimantFirstName = "George";
            claim.ClaimantLastName = "Washington";
            claim.Status = StatusCode.OPEN;
            claim.LossDate = Convert.ToDateTime("2014-07-09T17:19:13.631-07:00");
            claim.LossInfo = new LossInfoType()
            {
                CauseOfLoss = CauseOfLossCode.Collision,
                ReportedDate = Convert.ToDateTime("2014-07-10T17:19:13.676-07:00"),
                LossDescription = "Crashed into an apple tree"
            };
            claim.AssignedAdjusterID = 23424;
            
            VehicleInfoType[] vehiclesInfo = new VehicleInfoType[1];
            vehiclesInfo[0] = new VehicleInfoType()
            {
                Vin = "1M8GDM9AXKP042788",
                ModelYear = 2015,
                MakeDescription = "Ford",
                ModelDescription = "Mustang",
                EngineDescription = "EcoBoost",
                ExteriorColor = "Deep Impact Blue",
                LicPlate = "NO1PRES",
                LicPlateState = "VA",
                LicPlateExpDate = Convert.ToDateTime("2015-03-10-07:00"),
                DamageDescription = "Front end smashed in. Apple dents in roof.",
                Mileage = 1776
            };
            claim.Vehicles = vehiclesInfo;
            var result = await controller.Post(claim);
            OkNegotiatedContentResult<string> conNegResult = result as OkNegotiatedContentResult<string>;
            Assert.AreEqual("Success", conNegResult.Content);
        
        }
    }
}
