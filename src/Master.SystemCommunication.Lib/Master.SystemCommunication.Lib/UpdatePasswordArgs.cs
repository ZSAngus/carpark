using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

public class UpdatePasswordArgs
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
	public string OldPassword { get; set; }

	/// <summary>
	/// 新密碼
	/// </summary>
	[DataMember]
	public string NewPassword { get; set; }
}
