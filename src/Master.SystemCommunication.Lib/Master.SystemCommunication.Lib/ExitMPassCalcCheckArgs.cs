using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 澳门通参数
/// </summary>
public class ExitMPassCalcCheckArgs : Carpark2018Args
{
	[DataMember]
	public string PayStationName { get; set; }
}
