using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class GetCurrChargeRecordArgs
{
	[DataMember]
	public string PayStationName { get; set; }
}
