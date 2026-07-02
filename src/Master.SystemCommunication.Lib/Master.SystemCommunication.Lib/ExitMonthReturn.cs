using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 月租出口返回参数
/// </summary>
public class ExitMonthReturn : FuncReturn
{
	/// <summary>
	/// 是否離線，true為離線
	/// </summary>
	[DataMember]
	public bool ISOffline { get; set; }
}
