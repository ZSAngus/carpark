using System;
using System.Runtime.Serialization;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 手動添加在場記錄
/// </summary>
public class ManualEntryLicensePlateArgs : ProgramBase
{
	[DataMember]
	public string LicensePlate { get; set; }

	[DataMember]
	public DateTime InTime { get; set; }

	/// <summary>
	/// 場號
	/// </summary>
	[DataMember]
	public string SerialNumber { get; set; }

	/// <summary>
	/// 收費時間
	/// </summary>
	[DataMember]
	public DateTime ChargeTime { get; set; }

	[DataMember]
	public string StaffCode { get; set; }

	/// <summary>
	/// 操作PC名
	/// </summary>
	[DataMember]
	public string PayStationName { get; set; }

	/// <summary>
	/// 區域ID
	/// </summary>
	[DataMember]
	public int AreaID { get; set; }

	public ManualEntryLicensePlateArgs()
	{
	}

	public ManualEntryLicensePlateArgs(string onlyID)
		: base(onlyID)
	{
	}
}
