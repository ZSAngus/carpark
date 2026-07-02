using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 車位改變參數
/// 雲後臺用
/// </summary>
public class ParkingSpacesChangeArgs : ProgramBase
{
	[DataMember]
	public EnumParkType parkType { get; set; }

	[DataMember]
	public int privateCount { get; set; }
}
