using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 登錄傳入參數
/// </summary>
public class CashierLoginArgs
{
	/// <summary>
	/// 用戶名
	/// </summary>
	[DataMember]
	public string StaffCode { get; set; }

	/// <summary>
	///  密碼
	/// </summary>
	[DataMember]
	public string Password { get; set; }
}
