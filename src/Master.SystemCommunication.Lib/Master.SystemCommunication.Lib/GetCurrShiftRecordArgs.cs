using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class GetCurrShiftRecordArgs
{
	[DataMember]
	public string PayStationName { get; set; }
}
