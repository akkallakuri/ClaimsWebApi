using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsWebApi.Data
{
    public class Commands
    {
        public const string UpdateClaim = @"UPDATE C SET C.[ClaimantFirstName] = @ClaimantFirstName,C.[ClaimantLastName] = @ClaimantLastName,C.[StatusKey] = @StatusKey,C.[LossDate] = @LossDate,C.[AssignedAdjusterID] = @AssignedAdjusterID
                                           FROM dbo.Claims C WHERE ClaimNumber = @ClaimNumber";

        public const string UpdateLossInfo = @"UPDATE L SET L.[LossDescription] = @LossDescription,[ReportDate] = @ReportDate,[CauseOfLossKey] = @CauseOfLossKey
                                               FROM [dbo].[LossInfoType] L  WHERE L.ClaimNumber = @ClaimNumber";

        public const string InsertClaim = @"INSERT INTO [dbo].[Claims]([ClaimNumber],[ClaimantFirstName],[ClaimantLastName],[StatusKey],[LossDate],[LossInfoKey],[AssignedAdjusterID])
                                            VALUES(@ClaimNumber,@ClaimantFirstName,@ClaimantLastName,@StatusKey,@LossDate,NULL,@AssignedAdjusterID)";

        public const string InsertLossInfoType = @"INSERT INTO [dbo].[LossInfoType]([LossDescription],[ReportDate],[CauseOfLossKey],[ClaimNumber])
                                                  VALUES (@LossDescription,@ReportDate,@CauseOfLossKey, @ClaimNumber)";

        public const string UpdateLossInfoKey = @"UPDATE C SET C.LossInfoKey = L.LossInfoKey
                                                 FROM [dbo].[Claims] C INNER JOIN [dbo].[LossInfoType] L ON C.ClaimNumber = L.ClaimNumber WHERE C.ClaimNumber = @ClaimNumber";

        public const string InsertVehicleDamageInfo = @"INSERT INTO [dbo].[VehicleDamageInfo](ClaimNumber,ModelYear,MakeDescription,ModelDescription,EngineDescription,ExteriorColor,Vin,LicPlate,LicPlateState,LicPlateExpDate,DamageDescription,Mileage)
                                                      SELECT @ClaimNumber,@ModelYear,@MakeDescription,@ModelDescription,@EngineDescription,@ExteriorColor,@Vin,@LicPlate,@LicPlateState,@LicPlateExpDate,@DamageDescription,@Mileage";

        public const string UpdateVehicleInfo = @"UPDATE V SET 
                                                   V.[ModelYear] = @ModelYear
                                                  ,V.[MakeDescription] = @MakeDescription
                                                  ,[ModelDescription] = @ModelDescription
                                                  ,[EngineDescription] = @EngineDescription
                                                  ,[ExteriorColor] = @ExteriorColor
                                                  ,[Vin] = @Vin
                                                  ,[LicPlate] = @LicPlate
                                                  ,[LicPlateState] = @LicPlateState
                                                  ,[LicPlateExpDate] = @LicPlateExpDate
                                                  ,[DamageDescription] = @DamageDescription
                                                  ,[Mileage] = @Mileage
                                                FROM [dbo].[VehicleDamageInfo] V";

        public const string GetAllClaims = @"SELECT 
	                                        C.ClaimNumber
	                                        ,C.ClaimantFirstName
	                                        ,C.ClaimantLastName
	                                        ,C.StatusKey
	                                        ,C.LossDate
	                                        ,C.LossInfoKey
	                                        ,C.AssignedAdjusterID 
	                                        ,L.LossDescription
	                                        ,L.ReportDate
	                                        ,L.CauseOfLossKey
                                        FROM dbo.Claims C INNER JOIN dbo.LossInfoType L ON C.ClaimNumber = L.ClaimNumber AND C.LossInfoKey = L.LossInfoKey";

        public const string GetClaimByClaimNumber = @"SELECT 
	                                        C.ClaimNumber
	                                        ,C.ClaimantFirstName
	                                        ,C.ClaimantLastName
	                                        ,C.StatusKey
	                                        ,C.LossDate
	                                        ,C.LossInfoKey
	                                        ,C.AssignedAdjusterID 
	                                        ,L.LossDescription
	                                        ,L.ReportDate
	                                        ,L.CauseOfLossKey
                                        FROM dbo.Claims C INNER JOIN dbo.LossInfoType L ON C.ClaimNumber = L.ClaimNumber AND C.LossInfoKey = L.LossInfoKey
                                        WHERE C.ClaimNumber = '{0}'";
        public const string GetClaimsByLossDateRange = @"SELECT 
	                                        C.ClaimNumber
	                                        ,C.ClaimantFirstName
	                                        ,C.ClaimantLastName
	                                        ,C.StatusKey
	                                        ,C.LossDate
	                                        ,C.LossInfoKey
	                                        ,C.AssignedAdjusterID 
	                                        ,L.LossDescription
	                                        ,L.ReportDate
	                                        ,L.CauseOfLossKey
                                        FROM dbo.Claims C INNER JOIN dbo.LossInfoType L ON C.ClaimNumber = L.ClaimNumber AND C.LossInfoKey = L.LossInfoKey
                                        WHERE c.LossDate BETWEEN '{0}' AND '{1}'";
        public const string GetVehicleDetailsByClaim = @"SELECT 
	                                                ModelYear
	                                                ,MakeDescription
	                                                ,ModelDescription
	                                                ,EngineDescription
	                                                ,ExteriorColor
	                                                ,Vin
	                                                ,LicPlate
	                                                ,LicPlateState
	                                                ,LicPlateExpDate
	                                                ,DamageDescription
	                                                ,Mileage
                                                FROM dbo.VehicleDamageInfo 
                                                WHERE ClaimNumber = '{0}'";

        public const string GetVehicleDetailsByClaimVin = @"SELECT 
	                                                ModelYear
	                                                ,MakeDescription
	                                                ,ModelDescription
	                                                ,EngineDescription
	                                                ,ExteriorColor
	                                                ,Vin
	                                                ,LicPlate
	                                                ,LicPlateState
	                                                ,LicPlateExpDate
	                                                ,DamageDescription
	                                                ,Mileage
                                                FROM dbo.VehicleDamageInfo 
                                                WHERE ClaimNumber = '{0}' and Vin='{1}'";


        public const string DeleteClaim = @"DELETE FROM [dbo].[VehicleDamageInfo] WHERE ClaimNumber = @ClaimNumber DELETE FROM [dbo].[Claims] WHERE ClaimNumber = @ClaimNumber DELETE FROM [dbo].[LossInfoType] WHERE ClaimNumber = @ClaimNumber";

        #region commented
        //        public const string AllClaims = @"WITH XMLNAMESPACES ('http://www.mitchell.com/examples/claim' as cla)
        //                                        SELECT  c.ClaimNumber as 'cla:ClaimNumber'
        //		                                        ,c.ClaimantFirstName as 'cla:ClaimantFirstName'
        //		                                        ,c.ClaimantLastName as 'cla:ClaimantLastName'
        //		                                        ,(SELECT 
        //				                                        ClaimStatusDesc 
        //		                                          FROM 
        //				                                        dbo.ClaimStatus 
        //		                                          WHERE ClaimStatusCode = c.StatusKey
        //		                                         ) as 'cla:Status'
        //		                                        ,c.LossDate as 'cla:LossDate'
        //		                                        ,(SELECT 
        //				                                        lInfo.LossDescription as 'cla:LossDescription'
        //			                                           ,lInfo.ReportDate as 'cla:ReportedDate'
        //			                                           ,cl.CauseOfLossDesc as 'cla:CauseOfLoss' 
        //			                                        FROM 
        //			                                            dbo.LossInfoType lInfo INNER JOIN [dbo].[CauseOfLoss] cl ON lInfo.CauseOfLossKey = cl.CauseOfLossCode 
        //			                                        WHERE lInfo.ClaimNumber = c.ClaimNumber FOR XML PATH('cla:LossInfo'),TYPE)
        //		                                        ,c.AssignedAdjusterID as 'cla:AssignedAdjusterID'
        //		                                        ,(select ModelYear as 'cla:ModelYear'
        //				                                        ,MakeDescription as 'cla:MakeDescription'
        //				                                        ,ModelDescription as 'cla:ModelDescription' 
        //				                                        ,EngineDescription as 'cla:EngineDescription'
        //				                                        ,ExteriorColor as 'cla:ExteriorColor'
        //				                                        ,Vin as 'cla:Vin'
        //				                                        ,LicPlate as 'cla:LicPlate'
        //				                                        ,LicPlateState as 'cla:LicPlateState'
        //				                                        ,LicPlateExpDate as 'cla:LicPlateExpDate'
        //				                                        ,DamageDescription as 'cla:DamageDescription'
        //				                                        ,Mileage as 'cla:Mileage'
        //			                                        FROM [dbo].[VehicleDamageInfo] WHERE ClaimNumber = c.ClaimNumber FOR XML PATH('cla:VehicleDetails'),TYPE,ROOT('cla:Vehicles'))
        //                                        FROM	dbo.Claims c
        //                                        FOR XML RAW ('cla:MitchellClaimType'),ELEMENTS";    

        //        public const string ClaimByNumber = @"WITH XMLNAMESPACES ('http://www.mitchell.com/examples/claim' as cla)
        //                                        SELECT  c.ClaimNumber as 'cla:ClaimNumber'
        //		                                        ,c.ClaimantFirstName as 'cla:ClaimantFirstName'
        //		                                        ,c.ClaimantLastName as 'cla:ClaimantLastName'
        //		                                        ,(SELECT 
        //				                                        ClaimStatusDesc 
        //		                                          FROM 
        //				                                        dbo.ClaimStatus 
        //		                                          WHERE ClaimStatusCode = c.StatusKey
        //		                                         ) as 'cla:Status'
        //		                                        ,c.LossDate as 'cla:LossDate'
        //		                                        ,(SELECT 
        //				                                        lInfo.LossDescription as 'cla:LossDescription'
        //			                                           ,lInfo.ReportDate as 'cla:ReportedDate'
        //			                                           ,cl.CauseOfLossDesc as 'cla:CauseOfLoss' 
        //			                                        FROM 
        //			                                            dbo.LossInfoType lInfo INNER JOIN [dbo].[CauseOfLoss] cl ON lInfo.CauseOfLossKey = cl.CauseOfLossCode 
        //			                                        WHERE lInfo.ClaimNumber = c.ClaimNumber FOR XML PATH('cla:LossInfo'),TYPE)
        //		                                        ,c.AssignedAdjusterID as 'cla:AssignedAdjusterID'
        //		                                        ,(select ModelYear as 'cla:ModelYear'
        //				                                        ,MakeDescription as 'cla:MakeDescription'
        //				                                        ,ModelDescription as 'cla:ModelDescription' 
        //				                                        ,EngineDescription as 'cla:EngineDescription'
        //				                                        ,ExteriorColor as 'cla:ExteriorColor'
        //				                                        ,Vin as 'cla:Vin'
        //				                                        ,LicPlate as 'cla:LicPlate'
        //				                                        ,LicPlateState as 'cla:LicPlateState'
        //				                                        ,LicPlateExpDate as 'cla:LicPlateExpDate'
        //				                                        ,DamageDescription as 'cla:DamageDescription'
        //				                                        ,Mileage as 'cla:Mileage'
        //			                                        FROM [dbo].[VehicleDamageInfo] WHERE ClaimNumber = c.ClaimNumber FOR XML PATH('cla:VehicleDetails'),TYPE,ROOT('cla:Vehicles'))
        //                                        FROM	dbo.Claims c
        //                                        WHERE c.ClaimNumber = '{0}'
        //                                        FOR XML RAW ('cla:MitchellClaim'),ELEMENTS";

        //        public const string ClaimByLossDateRange = @"WITH XMLNAMESPACES ('http://www.mitchell.com/examples/claim' as cla)
        //                                        SELECT  c.ClaimNumber as 'cla:ClaimNumber'
        //		                                        ,c.ClaimantFirstName as 'cla:ClaimantFirstName'
        //		                                        ,c.ClaimantLastName as 'cla:ClaimantLastName'
        //		                                        ,(SELECT 
        //				                                        ClaimStatusDesc 
        //		                                          FROM 
        //				                                        dbo.ClaimStatus 
        //		                                          WHERE ClaimStatusCode = c.StatusKey
        //		                                         ) as 'cla:Status'
        //		                                        ,c.LossDate as 'cla:LossDate'
        //		                                        ,(SELECT 
        //				                                        lInfo.LossDescription as 'cla:LossDescription'
        //			                                           ,lInfo.ReportDate as 'cla:ReportedDate'
        //			                                           ,cl.CauseOfLossDesc as 'cla:CauseOfLoss' 
        //			                                        FROM 
        //			                                            dbo.LossInfoType lInfo INNER JOIN [dbo].[CauseOfLoss] cl ON lInfo.CauseOfLossKey = cl.CauseOfLossCode 
        //			                                        WHERE lInfo.ClaimNumber = c.ClaimNumber FOR XML PATH('cla:LossInfo'),TYPE)
        //		                                        ,c.AssignedAdjusterID as 'cla:AssignedAdjusterID'
        //		                                        ,(select ModelYear as 'cla:ModelYear'
        //				                                        ,MakeDescription as 'cla:MakeDescription'
        //				                                        ,ModelDescription as 'cla:ModelDescription' 
        //				                                        ,EngineDescription as 'cla:EngineDescription'
        //				                                        ,ExteriorColor as 'cla:ExteriorColor'
        //				                                        ,Vin as 'cla:Vin'
        //				                                        ,LicPlate as 'cla:LicPlate'
        //				                                        ,LicPlateState as 'cla:LicPlateState'
        //				                                        ,LicPlateExpDate as 'cla:LicPlateExpDate'
        //				                                        ,DamageDescription as 'cla:DamageDescription'
        //				                                        ,Mileage as 'cla:Mileage'
        //			                                        FROM [dbo].[VehicleDamageInfo] WHERE ClaimNumber = c.ClaimNumber FOR XML PATH('cla:VehicleDetails'),TYPE,ROOT('cla:Vehicles'))
        //                                        FROM	dbo.Claims c
        //                                        WHERE c.LossDate BETWEEN '{0}' AND '{1}'
        //                                        FOR XML RAW ('cla:MitchellClaimType'),ELEMENTS";


        //        public const string ClaimInfoByVin = @"WITH XMLNAMESPACES ('http://www.mitchell.com/examples/claim' as cla)
        //                                        SELECT  c.ClaimNumber as 'cla:ClaimNumber'
        //		                                        ,c.ClaimantFirstName as 'cla:ClaimantFirstName'
        //		                                        ,c.ClaimantLastName as 'cla:ClaimantLastName'
        //		                                        ,(SELECT 
        //				                                        ClaimStatusDesc 
        //		                                          FROM 
        //				                                        dbo.ClaimStatus 
        //		                                          WHERE ClaimStatusCode = c.StatusKey
        //		                                         ) as 'cla:Status'
        //		                                        ,c.LossDate as 'cla:LossDate'
        //		                                        ,(SELECT 
        //				                                        lInfo.LossDescription as 'cla:LossDescription'
        //			                                           ,lInfo.ReportDate as 'cla:ReportedDate'
        //			                                           ,cl.CauseOfLossDesc as 'cla:CauseOfLoss' 
        //			                                        FROM 
        //			                                            dbo.LossInfoType lInfo INNER JOIN [dbo].[CauseOfLoss] cl ON lInfo.CauseOfLossKey = cl.CauseOfLossCode 
        //			                                        WHERE lInfo.ClaimNumber = c.ClaimNumber FOR XML PATH('cla:LossInfo'),TYPE)
        //		                                        ,c.AssignedAdjusterID as 'cla:AssignedAdjusterID'
        //		                                        ,(select ModelYear as 'cla:ModelYear'
        //				                                        ,MakeDescription as 'cla:MakeDescription'
        //				                                        ,ModelDescription as 'cla:ModelDescription' 
        //				                                        ,EngineDescription as 'cla:EngineDescription'
        //				                                        ,ExteriorColor as 'cla:ExteriorColor'
        //				                                        ,Vin as 'cla:Vin'
        //				                                        ,LicPlate as 'cla:LicPlate'
        //				                                        ,LicPlateState as 'cla:LicPlateState'
        //				                                        ,LicPlateExpDate as 'cla:LicPlateExpDate'
        //				                                        ,DamageDescription as 'cla:DamageDescription'
        //				                                        ,Mileage as 'cla:Mileage'
        //			                                        FROM [dbo].[VehicleDamageInfo] WHERE ClaimNumber = c.ClaimNumber AND Vin = '{1}' FOR XML PATH('cla:VehicleDetails'),TYPE,ROOT('cla:Vehicles'))
        //                                        FROM	dbo.Claims c
        //                                        WHERE c.ClaimNumber = '{0}'
        //                                        FOR XML RAW ('cla:MitchellClaimType'),ELEMENTS";
        #endregion
    }
}
