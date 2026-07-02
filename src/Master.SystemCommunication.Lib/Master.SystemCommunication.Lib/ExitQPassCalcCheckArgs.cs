using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 闪付参数
/// </summary>
public class ExitQPassCalcCheckArgs : Carpark2018Args
{
	[DataMember]
	public string PayStationName { get; set; }
}
