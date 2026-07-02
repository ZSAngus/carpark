using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 查詢月租卡是否有效參數
/// </summary>
public class GetCardInfoArgs : ProgramBase
{
	/// <summary>
	/// 車牌
	/// </summary>
	[DataMember]
	public string LicensePlate { get; set; }

	/// <summary>
	/// 卡面號或內存號
	/// </summary>
	[DataMember]
	public string CardNumber { get; set; }

	public GetCardInfoArgs()
	{
	}

	public GetCardInfoArgs(string onlyID)
		: base(onlyID)
	{
	}
}
