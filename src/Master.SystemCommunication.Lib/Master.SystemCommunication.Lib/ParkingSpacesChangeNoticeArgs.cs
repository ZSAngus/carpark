using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 車位改變通知
/// </summary>
public class ParkingSpacesChangeNoticeArgs : ProgramBase
{
	[DataMember]
	public EnumParkType parkType { get; set; }

	[DataMember]
	public int parkAreaID { get; set; }

	/// <summary>
	/// 車位數
	/// </summary>
	[DataMember]
	public int ParkingSpacesCount { get; set; }

	/// <summary>
	/// true為人手滿
	/// </summary>
	[DataMember]
	public bool ManualFull { get; set; }

	public ParkingSpacesChangeNoticeArgs()
	{
	}

	public ParkingSpacesChangeNoticeArgs(string onlyID)
		: base(onlyID)
	{
	}
}
