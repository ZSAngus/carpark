using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class SaveChargeRecordReturn
{
	[DataMember]
	public bool ISOK { get; set; }

	[DataMember]
	public string ErrCode { get; set; }
}
