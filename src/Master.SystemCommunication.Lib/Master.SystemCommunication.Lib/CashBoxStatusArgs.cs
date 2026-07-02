using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class CashBoxStatusArgs
{
	[DataMember]
	public string CashBoxCode { get; set; }

	[DataMember]
	public string CashBoxName { get; set; }

	[DataMember]
	public int CashBoxType { get; set; }

	[DataMember]
	public string CashDetails { get; set; }

	[DataMember]
	public string CashValue { get; set; }
}
