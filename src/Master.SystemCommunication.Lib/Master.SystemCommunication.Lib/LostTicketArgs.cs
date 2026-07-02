using System;
using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

public class LostTicketArgs
{
	[DataMember]
	public DateTime InTime { get; set; }

	[DataMember]
	public EnumParkType parkType { get; set; }

	[DataMember]
	public string StaffCode { get; set; }

	[DataMember]
	public string systemName { get; set; }
}
