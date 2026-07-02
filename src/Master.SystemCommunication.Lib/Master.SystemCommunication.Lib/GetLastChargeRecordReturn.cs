using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class GetLastChargeRecordReturn
{
	[DataMember]
	public bool ISOK { get; set; }

	[DataMember]
	public string ErrCode { get; set; }

	/// <summary>
	/// 格式化後的收據
	/// </summary>
	[DataMember]
	public string FormatReceiptStr { get; set; }
}
