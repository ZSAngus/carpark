using System;
using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

public class TimeCompensationFareArgs
{
	[DataMember]
	public string StaffCode { get; set; }

	[DataMember]
	public int ShiftID { get; set; }

	[DataMember]
	public DateTime InTime { get; set; }

	[DataMember]
	public EnumParkType parkType { get; set; }

	[DataMember]
	public string PayStationName { get; set; }

	[DataMember]
	public int AreaID { get; set; }
}
