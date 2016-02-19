namespace ClaimsWebApi.Models
{

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34283")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mitchell.com/examples/claim")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.mitchell.com/examples/claim", IsNullable = true)]
    public partial class MitchellClaimType
    {
        private string claimNumberField;
        private string claimantFirstNameField;
        private string claimantLastNameField;
        private StatusCode statusField;
        private bool statusFieldSpecified;
        private System.DateTime lossDateField;
        private bool lossDateFieldSpecified;
        private LossInfoType lossInfoField;
        private int assignedAdjusterIDField;
        private bool assignedAdjusterIDFieldSpecified;
        private VehicleInfoType[] vehiclesField;
        /// 
        public string ClaimNumber
        {
            get
            {
                return this.claimNumberField;
            }
            set
            {
                this.claimNumberField = value;
            }
        }
        /// 
        public string ClaimantFirstName
        {
            get
            {
                return this.claimantFirstNameField;
            }
            set
            {
                this.claimantFirstNameField = value;
            }
        }
        /// 
        public string ClaimantLastName
        {
            get
            {
                return this.claimantLastNameField;
            }
            set
            {
                this.claimantLastNameField = value;
            }
        }
        /// 
        public StatusCode Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StatusSpecified
        {
            get
            {
                return this.statusFieldSpecified;
            }
            set
            {
                this.statusFieldSpecified = value;
            }
        }
        /// 
        public System.DateTime LossDate
        {
            get
            {
                return this.lossDateField;
            }
            set
            {
                this.lossDateField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LossDateSpecified
        {
            get
            {
                return this.lossDateFieldSpecified;
            }
            set
            {
                this.lossDateFieldSpecified = value;
            }
        }
        /// 
        public LossInfoType LossInfo
        {
            get
            {
                return this.lossInfoField;
            }
            set
            {
                this.lossInfoField = value;
            }
        }
        /// 
        public int AssignedAdjusterID
        {
            get
            {
                return this.assignedAdjusterIDField;
            }
            set
            {
                this.assignedAdjusterIDField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AssignedAdjusterIDSpecified
        {
            get
            {
                return this.assignedAdjusterIDFieldSpecified;
            }
            set
            {
                this.assignedAdjusterIDFieldSpecified = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlArrayItemAttribute("VehicleDetails", IsNullable = false)]
        public VehicleInfoType[] Vehicles
        {
            get
            {
                return this.vehiclesField;
            }
            set
            {
                this.vehiclesField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34283")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mitchell.com/examples/claim")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.mitchell.com/examples/claim", IsNullable = false)]
    public enum StatusCode
    {
        /// 
        OPEN,
        /// 
        CLOSED,
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34283")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mitchell.com/examples/claim")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.mitchell.com/examples/claim", IsNullable = true)]
    public partial class LossInfoType
    {
        private CauseOfLossCode causeOfLossField;
        private bool causeOfLossFieldSpecified;
        private System.DateTime reportedDateField;
        private bool reportedDateFieldSpecified;
        private string lossDescriptionField;
        /// 
        public CauseOfLossCode CauseOfLoss
        {
            get
            {
                return this.causeOfLossField;
            }
            set
            {
                this.causeOfLossField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CauseOfLossSpecified
        {
            get
            {
                return this.causeOfLossFieldSpecified;
            }
            set
            {
                this.causeOfLossFieldSpecified = value;
            }
        }
        /// 
        public System.DateTime ReportedDate
        {
            get
            {
                return this.reportedDateField;
            }
            set
            {
                this.reportedDateField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReportedDateSpecified
        {
            get
            {
                return this.reportedDateFieldSpecified;
            }
            set
            {
                this.reportedDateFieldSpecified = value;
            }
        }
        /// 
        public string LossDescription
        {
            get
            {
                return this.lossDescriptionField;
            }
            set
            {
                this.lossDescriptionField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34283")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mitchell.com/examples/claim")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.mitchell.com/examples/claim", IsNullable = false)]
    public enum CauseOfLossCode
    {
        /// 
        Collision,
        /// 
        Explosion,
        /// 
        Fire,
        /// 
        Hail,
        /// 
        [System.Xml.Serialization.XmlEnumAttribute("Mechanical Breakdown")]
        MechanicalBreakdown,
        /// 
        Other,
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34283")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mitchell.com/examples/claim")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.mitchell.com/examples/claim", IsNullable = true)]
    public partial class VehicleInfoType
    {
        private int modelYearField;
        private string makeDescriptionField;
        private string modelDescriptionField;
        private string engineDescriptionField;
        private string exteriorColorField;
        private string vinField;
        private string licPlateField;
        private string licPlateStateField;
        private System.DateTime licPlateExpDateField;
        private bool licPlateExpDateFieldSpecified;
        private string damageDescriptionField;
        private int mileageField;
        private bool mileageFieldSpecified;
        /// 
        public int ModelYear
        {
            get
            {
                return this.modelYearField;
            }
            set
            {
                this.modelYearField = value;
            }
        }
        /// 
        public string MakeDescription
        {
            get
            {
                return this.makeDescriptionField;
            }
            set
            {
                this.makeDescriptionField = value;
            }
        }
        /// 
        public string ModelDescription
        {
            get
            {
                return this.modelDescriptionField;
            }
            set
            {
                this.modelDescriptionField = value;
            }
        }
        /// 
        public string EngineDescription
        {
            get
            {
                return this.engineDescriptionField;
            }
            set
            {
                this.engineDescriptionField = value;
            }
        }
        /// 
        public string ExteriorColor
        {
            get
            {
                return this.exteriorColorField;
            }
            set
            {
                this.exteriorColorField = value;
            }
        }
        /// 
        public string Vin
        {
            get
            {
                return this.vinField;
            }
            set
            {
                this.vinField = value;
            }
        }
        /// 
        public string LicPlate
        {
            get
            {
                return this.licPlateField;
            }
            set
            {
                this.licPlateField = value;
            }
        }
        /// 
        public string LicPlateState
        {
            get
            {
                return this.licPlateStateField;
            }
            set
            {
                this.licPlateStateField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime LicPlateExpDate
        {
            get
            {
                return this.licPlateExpDateField;
            }
            set
            {
                this.licPlateExpDateField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LicPlateExpDateSpecified
        {
            get
            {
                return this.licPlateExpDateFieldSpecified;
            }
            set
            {
                this.licPlateExpDateFieldSpecified = value;
            }
        }
        /// 
        public string DamageDescription
        {
            get
            {
                return this.damageDescriptionField;
            }
            set
            {
                this.damageDescriptionField = value;
            }
        }
        /// 
        public int Mileage
        {
            get
            {
                return this.mileageField;
            }
            set
            {
                this.mileageField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MileageSpecified
        {
            get
            {
                return this.mileageFieldSpecified;
            }
            set
            {
                this.mileageFieldSpecified = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34283")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.mitchell.com/examples/claim")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.mitchell.com/examples/claim", IsNullable = true)]
    public partial class VehicleListType
    {
        private VehicleInfoType[] vehicleDetailsField;
        /// 
        [System.Xml.Serialization.XmlElementAttribute("VehicleDetails")]
        public VehicleInfoType[] VehicleDetails
        {
            get
            {
                return this.vehicleDetailsField;
            }
            set
            {
                this.vehicleDetailsField = value;
            }
        }
    }
}