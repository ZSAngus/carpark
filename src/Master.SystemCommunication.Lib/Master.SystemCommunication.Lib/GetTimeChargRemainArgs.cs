using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 獲取時租車位
/// </summary>
public class GetTimeChargRemainArgs : ProgramBase
{
	[DataMember]
	public int GateID { get; set; }

	public GetTimeChargRemainArgs()
	{
	}

	public GetTimeChargRemainArgs(string onlyID)
		: base(onlyID)
	{
	}
}
