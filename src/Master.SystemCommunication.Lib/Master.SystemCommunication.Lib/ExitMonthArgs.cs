using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 月租出口参数
/// </summary>
public class ExitMonthArgs : Carpark2018Args
{
	/// <summary>
	/// 存在用戶號表示人手通過，可不比對車牌
	/// </summary>
	[DataMember]
	public string StaffCode { get; set; }
}
