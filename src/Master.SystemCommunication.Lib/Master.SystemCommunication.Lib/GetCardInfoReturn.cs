using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
///             返回查卡信息
/// </summary>
public class GetCardInfoReturn : ProgramBase
{
	[DataMember]
	public string ErrCode { get; set; }

	public GetCardInfoReturn()
	{
	}

	public GetCardInfoReturn(string onlyID)
		: base(onlyID)
	{
	}
}
