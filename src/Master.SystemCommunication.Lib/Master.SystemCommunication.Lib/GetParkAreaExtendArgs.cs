using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 取車位詳細信息
/// </summary>
public class GetParkAreaExtendArgs : ProgramBase
{
	[DataMember]
	public int AreaID { get; set; }

	[DataMember]
	public int ParkType { get; set; }

	[DataMember]
	public int TimeChargeSupply { get; set; }

	[DataMember]
	public int TimeChargRemain { get; set; }

	public GetParkAreaExtendArgs()
	{
	}

	public GetParkAreaExtendArgs(string onlyID)
		: base(onlyID)
	{
	}
}
