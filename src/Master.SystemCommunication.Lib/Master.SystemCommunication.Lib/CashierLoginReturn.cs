using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 傳出參數
/// </summary>
public class CashierLoginReturn
{
	[DataMember]
	public bool ISOK { get; set; }

	[DataMember]
	public string ErrCode { get; set; }
}
