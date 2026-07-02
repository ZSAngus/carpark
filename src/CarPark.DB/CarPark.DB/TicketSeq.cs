using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace CarPark.DB;

[Serializable]
[DataContract(IsReference = true)]
[EdmEntityType(NamespaceName = "CarPark.DB", Name = "TicketSeq")]
public class TicketSeq : EntityObject
{
	private int _Seq;

	private string _StaffCode;

	private int _ShiftID;

	private DateTime _CreateTime;

	private bool _IsDelete;

	public string TicketCode => Seq.ToString().PadLeft(7, '0');

	[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
	[DataMember]
	public int Seq
	{
		get
		{
			return _Seq;
		}
		set
		{
			if (_Seq != value)
			{
				ReportPropertyChanging("Seq");
				_Seq = StructuralObject.SetValidValue(value);
				ReportPropertyChanged("Seq");
			}
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

	[EdmScalarProperty(EntityKeyProperty = false, IsNullable = false)]
	[DataMember]
	public int ShiftID
	{
		get
		{
			return _ShiftID;
		}
		set
		{
			ReportPropertyChanging("ShiftID");
			_ShiftID = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("ShiftID");
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
	public bool IsDelete
	{
		get
		{
			return _IsDelete;
		}
		set
		{
			ReportPropertyChanging("IsDelete");
			_IsDelete = StructuralObject.SetValidValue(value);
			ReportPropertyChanged("IsDelete");
		}
	}

	public static TicketSeq CreateTicketSeq(int seq, string staffCode, int shiftID, DateTime createTime, bool isDelete)
	{
		TicketSeq ticketSeq = new TicketSeq();
		ticketSeq.Seq = seq;
		ticketSeq.StaffCode = staffCode;
		ticketSeq.ShiftID = shiftID;
		ticketSeq.CreateTime = createTime;
		ticketSeq.IsDelete = isDelete;
		return ticketSeq;
	}
}
