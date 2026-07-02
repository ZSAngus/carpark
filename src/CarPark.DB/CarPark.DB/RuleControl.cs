using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "RuleControl")]
public class RuleControl : EntityObject
{
	private string _ControlObj;

	private bool _IgnoreLoop;

	private int _ID;

	private string _RuleNo;

	private int _UseCycle;

	private DateTime? _ForbidTimes;

	private string _AcceptGateID;

	private bool _CheckRegistrationLP;

	private bool _IsLPInOut;

	private DateTimeOffset _CreateTime;

	private string _CreateStaffCode;

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public string ControlObj
	{
		get
		{
			return _ControlObj;
		}
		set
		{
			ReportPropertyChanging("ControlObj");
			_ControlObj = StructuralObject.SetValidValue(value, isNullable: true);
			ReportPropertyChanged("ControlObj");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool IgnoreLoop
	{
		get
		{
			return _IgnoreLoop;
		}
		set
		{
			ReportPropertyChanging("IgnoreLoop");
			_IgnoreLoop = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IgnoreLoop");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int ID
	{
		get
		{
			return _ID;
		}
		set
		{
			if (_ID != value)
			{
				ReportPropertyChanging("ID");
				_ID = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("ID");
			}
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string RuleNo
	{
		get
		{
			return _RuleNo;
		}
		set
		{
			ReportPropertyChanging("RuleNo");
			_RuleNo = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("RuleNo");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int UseCycle
	{
		get
		{
			return _UseCycle;
		}
		set
		{
			ReportPropertyChanging("UseCycle");
			_UseCycle = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("UseCycle");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = true)]
	[DataMember]
	public DateTime? ForbidTimes
	{
		get
		{
			return _ForbidTimes;
		}
		set
		{
			ReportPropertyChanging("ForbidTimes");
			_ForbidTimes = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ForbidTimes");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string AcceptGateID
	{
		get
		{
			return _AcceptGateID;
		}
		set
		{
			ReportPropertyChanging("AcceptGateID");
			_AcceptGateID = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("AcceptGateID");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public bool CheckRegistrationLP
	{
		get
		{
			return _CheckRegistrationLP;
		}
		set
		{
			ReportPropertyChanging("CheckRegistrationLP");
			_CheckRegistrationLP = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CheckRegistrationLP");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public bool IsLPInOut
	{
		get
		{
			return _IsLPInOut;
		}
		set
		{
			ReportPropertyChanging("IsLPInOut");
			_IsLPInOut = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsLPInOut");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public DateTimeOffset CreateTime
	{
		get
		{
			return _CreateTime;
		}
		set
		{
			ReportPropertyChanging("CreateTime");
			_CreateTime = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("CreateTime");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string CreateStaffCode
	{
		get
		{
			return _CreateStaffCode;
		}
		set
		{
			ReportPropertyChanging("CreateStaffCode");
			_CreateStaffCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("CreateStaffCode");
		}
	}

	public static RuleControl CreateRuleControl(int id, string ruleNo, int useCycle, string acceptGateID, bool checkRegistrationLP, bool isLPInOut, DateTimeOffset createTime, string createStaffCode, bool ignoreLoop)
	{
		RuleControl ruleControl = new RuleControl();
		ruleControl.ID = id;
		ruleControl.RuleNo = ruleNo;
		ruleControl.UseCycle = useCycle;
		ruleControl.AcceptGateID = acceptGateID;
		ruleControl.CheckRegistrationLP = checkRegistrationLP;
		ruleControl.IsLPInOut = isLPInOut;
		ruleControl.CreateTime = createTime;
		ruleControl.CreateStaffCode = createStaffCode;
		ruleControl.IgnoreLoop = ignoreLoop;
		return ruleControl;
	}
}
