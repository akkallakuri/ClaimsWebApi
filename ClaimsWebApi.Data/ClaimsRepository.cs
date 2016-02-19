using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimsWebApi.Data.Contracts;
using ClaimsWebApi.Models;
using System.Data.SqlClient;
using Dapper;
using System.Transactions;
using ClaimsWebApi.Data;



namespace ClaimsWebApi.Data
{
    public class ClaimsRepository : IClaimsRepository
    {
        private string ConnectionString = String.Empty;


        public ClaimsRepository()
        {
            ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["ClaimsDB"];
        }

        async public Task<IEnumerable<MitchellClaimType>> ListAllAsync(string query)
        {
            return await FindClaims(query);
        }

        async public Task<string> AddAsync(MitchellClaimType item)
        {
            return await CreateClaim(item);
        }

        async public Task<string> UpdateAsync(MitchellClaimType item)
        {
            return await UpdateClaim(item);
        }

        async public Task<string> DeleteAsync(string key)
        {
            return await DeleteClaimAync(key);
        }

        #region "Private Methods"
        async private Task<IEnumerable<MitchellClaimType>> FindClaims(string query = "")
        {
            IEnumerable<Claim> oList = null;
            List<MitchellClaimType> claims = new List<MitchellClaimType>();
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    await conn.OpenAsync();
                    oList = await conn.QueryAsync<Claim>(query == "" ? Commands.GetAllClaims : query);
                    if (oList != null && oList.Count<Claim>() > 0)
                    {
                        foreach (var item in oList)
                        {
                            MitchellClaimType claim = new MitchellClaimType();
                            claim.AssignedAdjusterID = item.AssignedAdjusterID;
                            claim.ClaimantFirstName = item.ClaimantFirstName;
                            claim.ClaimantLastName = item.ClaimantLastName;
                            claim.ClaimNumber = item.ClaimNumber;
                            claim.LossDate = item.LossDate;
                            CauseOfLossCode c = CauseOfLossCode.Collision;
                            if (item.CauseOfLossKey == 2) c = CauseOfLossCode.Explosion;
                            if (item.CauseOfLossKey == 3) c = CauseOfLossCode.Fire;
                            if (item.CauseOfLossKey == 4) c = CauseOfLossCode.Hail;
                            if (item.CauseOfLossKey == 5) c = CauseOfLossCode.MechanicalBreakdown;
                            if (item.CauseOfLossKey == 6) c = CauseOfLossCode.Other;
                            claim.LossInfo = new LossInfoType { CauseOfLoss = c, LossDescription = item.LossDescription, ReportedDate = item.ReportDate };
                            claim.Status = item.StatusKey == 1 ? StatusCode.OPEN : StatusCode.CLOSED;
                            IEnumerable<VehicleDamageInfo> vehicleInfo = await conn.QueryAsync<VehicleDamageInfo>(string.Format(Commands.GetVehicleDetailsByClaim, item.ClaimNumber));
                            if (vehicleInfo != null)
                            {
                                VehicleInfoType[] vInfo = new VehicleInfoType[vehicleInfo.Count<VehicleDamageInfo>()];
                                int j = 0;
                                foreach (var v in vehicleInfo)
                                {
                                    vInfo[j] = new VehicleInfoType()
                                    {
                                        DamageDescription = v.DamageDescription,
                                        EngineDescription = v.EngineDescription,
                                        ExteriorColor = v.ExteriorColor,
                                        LicPlate = v.LicPlate,
                                        LicPlateExpDate = v.LicPlateExpDate,
                                        MakeDescription = v.MakeDescription,
                                        ModelYear = v.ModelYear,
                                        Vin = v.Vin,
                                        LicPlateState = v.LicPlateState,
                                        ModelDescription = v.ModelDescription
                                    };

                                    j++;
                                }
                                claim.Vehicles = vInfo;
                            }
                            claims.Add(claim);

                        }
                    }
                }
            }
            catch
            {

            }
            return claims.AsEnumerable();
        }

        async private Task<string> UpdateClaim(MitchellClaimType claim)
        {
            string operationStatus = "Failure";
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        DynamicParameters claimParams = new DynamicParameters();
                        claimParams.Add("@ClaimNumber", claim.ClaimNumber);
                        IEnumerable<MitchellClaimType> claimFound = await conn.QueryAsync<MitchellClaimType>(string.Format(Commands.GetClaimByClaimNumber, claim.ClaimNumber));
                        if (claimFound.Count<MitchellClaimType>() == 0)
                            return "Claim does not found.";

                        claimParams.Add("@ClaimantFirstName", claim.ClaimantFirstName);
                        claimParams.Add("@ClaimantLastName", claim.ClaimantLastName);
                        claimParams.Add("@StatusKey", claim.Status == StatusCode.OPEN ? 1 : 2);
                        claimParams.Add("@LossDate", claim.LossDate);
                        claimParams.Add("@AssignedAdjusterID", claim.AssignedAdjusterID);

                        await conn.ExecuteAsync(Commands.UpdateClaim, claimParams);

                        DynamicParameters LossInfoParams = new DynamicParameters();
                        LossInfoParams.Add("@LossDescription", claim.LossInfo.LossDescription);
                        LossInfoParams.Add("@ReportDate", claim.LossInfo.ReportedDate);
                        LossInfoParams.Add("@ClaimNumber", claim.ClaimNumber);
                        int causeOfLoss = 1;

                        if (claim.LossInfo.CauseOfLoss == CauseOfLossCode.Explosion) causeOfLoss = 2;
                        if (claim.LossInfo.CauseOfLoss == CauseOfLossCode.Fire) causeOfLoss = 3;
                        if (claim.LossInfo.CauseOfLoss == CauseOfLossCode.Hail) causeOfLoss = 4;
                        if (claim.LossInfo.CauseOfLoss == CauseOfLossCode.MechanicalBreakdown) causeOfLoss = 5;
                        if (claim.LossInfo.CauseOfLoss == CauseOfLossCode.Other) causeOfLoss = 6;

                        LossInfoParams.Add("@CauseOfLossKey", causeOfLoss);

                        await conn.ExecuteAsync(Commands.UpdateLossInfo, LossInfoParams);

                        if (claim.Vehicles != null)
                        {
                            foreach (VehicleInfoType v in claim.Vehicles)
                            {
                                DynamicParameters vehicleParams = new DynamicParameters();
                                vehicleParams.Add("@ClaimNumber", claim.ClaimNumber);
                                // assumed update claim does not have a new vehicle.
                                IEnumerable<MitchellClaimType> claimByVin = await conn.QueryAsync<MitchellClaimType>(string.Format(Commands.GetVehicleDetailsByClaimVin, claim.ClaimNumber, v.Vin));
                                if (claimFound.Count<MitchellClaimType>() == 0)
                                {
                                    scope.Dispose();
                                    return "No vehicle exists with this Vin:" + v.Vin;
                                }

                                vehicleParams.Add("@ModelYear", v.ModelYear);
                                vehicleParams.Add("@MakeDescription", v.MakeDescription);
                                vehicleParams.Add("@EngineDescription", v.EngineDescription);
                                vehicleParams.Add("@ExteriorColor", v.ExteriorColor);
                                vehicleParams.Add("@Vin", v.Vin);
                                vehicleParams.Add("@LicPlate", v.LicPlate);
                                vehicleParams.Add("@LicPlateState", v.LicPlateState);
                                vehicleParams.Add("@DamageDescription", v.DamageDescription);
                                vehicleParams.Add("@LicPlateExpDate", v.LicPlateExpDate);
                                vehicleParams.Add("@Mileage", v.Mileage);

                                await conn.ExecuteAsync(Commands.UpdateVehicleInfo, vehicleParams);
                            }
                        }
                    }
                    scope.Complete();
                }
                operationStatus = "Success";
            }
            catch (Exception ex)
            {
                operationStatus = ex.Message;
            }
            return operationStatus;
        }
        async private Task<string> DeleteClaimAync(string ClaimNumber)
        {
            string operationStatus = "Failure";
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        IEnumerable<MitchellClaimType> claimFound = await conn.QueryAsync<MitchellClaimType>(string.Format(Commands.GetClaimByClaimNumber, ClaimNumber));
                        if (claimFound.Count<MitchellClaimType>() == 0)
                        {
                            scope.Dispose();
                            return "Claim does not found.";
                        }
                        
                        DynamicParameters claimParams = new DynamicParameters();
                        claimParams.Add("@ClaimNumber", ClaimNumber);
                        await conn.ExecuteAsync(Commands.DeleteClaim, claimParams);

                    }
                    scope.Complete();
                }
                operationStatus = "Success";
            }
            catch (Exception ex)
            {
                operationStatus = ex.Message;
            }
            return operationStatus;
        }
        async private Task<string> CreateClaim(MitchellClaimType claim)
        {
            string operationStatus = "Failure";
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (var conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        DynamicParameters claimParams = new DynamicParameters();
                        claimParams.Add("@ClaimNumber", claim.ClaimNumber);
                        claimParams.Add("@ClaimantFirstName", claim.ClaimantFirstName);
                        claimParams.Add("@ClaimantLastName", claim.ClaimantLastName);
                        claimParams.Add("@StatusKey", claim.Status == StatusCode.OPEN ? 1 : 2);
                        claimParams.Add("@LossDate", claim.LossDate);
                        claimParams.Add("@AssignedAdjusterID", claim.AssignedAdjusterID);

                        await conn.ExecuteAsync(Commands.InsertClaim, claimParams);

                        DynamicParameters LossInfoParams = new DynamicParameters();
                        LossInfoParams.Add("@LossDescription", claim.LossInfo.LossDescription);
                        LossInfoParams.Add("@ReportDate", claim.LossInfo.ReportedDate);
                        LossInfoParams.Add("@ClaimNumber", claim.ClaimNumber);
                        int causeOfLoss = 1;

                        if (claim.LossInfo.CauseOfLoss == CauseOfLossCode.Explosion) causeOfLoss = 2;
                        if (claim.LossInfo.CauseOfLoss == CauseOfLossCode.Fire) causeOfLoss = 3;
                        if (claim.LossInfo.CauseOfLoss == CauseOfLossCode.Hail) causeOfLoss = 4;
                        if (claim.LossInfo.CauseOfLoss == CauseOfLossCode.MechanicalBreakdown) causeOfLoss = 5;
                        if (claim.LossInfo.CauseOfLoss == CauseOfLossCode.Other) causeOfLoss = 6;

                        LossInfoParams.Add("@CauseOfLossKey", causeOfLoss);

                        await conn.ExecuteAsync(Commands.InsertLossInfoType, LossInfoParams);
                        await conn.ExecuteAsync(Commands.UpdateLossInfoKey, claimParams);

                        foreach (VehicleInfoType v in claim.Vehicles)
                        {
                            DynamicParameters vehicleParams = new DynamicParameters();
                            vehicleParams.Add("@ClaimNumber", claim.ClaimNumber);
                            vehicleParams.Add("@ModelYear", v.ModelYear);
                            vehicleParams.Add("@ModelDescription", v.ModelDescription);
                            vehicleParams.Add("@MakeDescription", v.MakeDescription);
                            vehicleParams.Add("@EngineDescription", v.EngineDescription);
                            vehicleParams.Add("@ExteriorColor", v.ExteriorColor);
                            vehicleParams.Add("@Vin", v.Vin);
                            vehicleParams.Add("@LicPlate", v.LicPlate);
                            vehicleParams.Add("@LicPlateState", v.LicPlateState);
                            vehicleParams.Add("@DamageDescription", v.DamageDescription);
                            vehicleParams.Add("@LicPlateExpDate", v.LicPlateExpDate);
                            vehicleParams.Add("@Mileage", v.Mileage);

                            await conn.ExecuteAsync(Commands.InsertVehicleDamageInfo, vehicleParams);
                        }

                    }
                    scope.Complete();
                    operationStatus = "Success";
                }

            }
            catch (Exception ex)
            {
                operationStatus = ex.Message;
            }
            return operationStatus;
        }

        #endregion

    }
}
