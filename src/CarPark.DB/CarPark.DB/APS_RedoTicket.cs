using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "APS_RedoTicket")]
public class APS_RedoTicket : EntityObject
{
	private int _ID;

	private string _TicketCode;

	private DateTime _CreateTime;

	private string _StaffCode;

	private string _PCName;

	private int _APSID;

	private int _RedoType;

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
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
	public string TicketCode
	{
		get
		{
			return _TicketCode;
		}
		set
		{
			ReportPropertyChanging("TicketCode");
			_TicketCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("TicketCode");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public DateTime CreateTime
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public string StaffCode
	{
		get
		{
			return _StaffCode;
		}
		set
		{
			ReportPropertyChanging("StaffCode");
			_StaffCode = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("StaffCode");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public string PCName
	{
		get
		{
			return _PCName;
		}
		set
		{
			ReportPropertyChanging("PCName");
			_PCName = StructuralObject.SetValidValue(value, isNullable: false);
			ReportPropertyChanged("PCName");
		}
	}

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int APSID
	{
		get
		{
			return _APSID;
		}
		set
		{
			ReportPropertyChanging("APSID");
			_APSID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("APSID");
		}
	}

	[DataMember]
	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	public int RedoType
	{
		get
		{
			return _RedoType;
		}
		set
		{
			ReportPropertyChanging("RedoType");
			_RedoType = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("RedoType");
		}
	}

	public static APS_RedoTicket CreateAPS_RedoTicket(int id, string ticketCode, DateTime createTime, string staffCode, string pCName, int aPSID, int redoType)
	{
		APS_RedoTicket aPS_RedoTicket = new APS_RedoTicket();
		aPS_RedoTicket.ID = id;
		aPS_RedoTicket.TicketCode = ticketCode;
		aPS_RedoTicket.CreateTime = createTime;
		aPS_RedoTicket.StaffCode = staffCode;
		aPS_RedoTicket.PCName = pCName;
		aPS_RedoTicket.APSID = aPSID;
		aPS_RedoTicket.RedoType = redoType;
		return aPS_RedoTicket;
	}
}
