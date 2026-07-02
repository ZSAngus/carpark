using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class EndCurrShiftRecordArgs
{
	[DataMember]
	public string PayStationName { get; set; }

	[DataMember]
	public string StaffCode { get; set; }
}
