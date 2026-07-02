using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 改車位參數
/// </summary>
public class ParkAreaExtendArgs : ProgramBase
{
	/// <summary>
	/// 操作用戶
	/// </summary>
	[DataMember]
	public string ShiffCode { get; set; }

	/// <summary>
	/// 系統名
	/// </summary>
	[DataMember]
	public string SystemName { get; set; }

	/// <summary>
	/// ParkAreaExtend表ID
	/// </summary>
	[DataMember]
	public int ParkAreaExtendID { get; set; }

	/// <summary>
	/// 車類型
	/// </summary>
	[DataMember]
	public EnumParkType parkType { get; set; }

	/// <summary>
	/// 中文名
	/// </summary>
	[DataMember]
	public string NameCN { get; set; }

	/// <summary>
	/// 英文名
	/// </summary>
	[DataMember]
	public string NameEN { get; set; }

	/// <summary>
	/// 總供應
	/// </summary>
	[DataMember]
	public int TotalSupply { get; set; }

	/// <summary>
	/// 時租占用
	/// </summary>
	[DataMember]
	public int TimeChargeUse { get; set; }

	/// <summary>
	/// 擴展車位
	/// </summary>
	[DataMember]
	public int ExtendCount { get; set; }

	public ParkAreaExtendArgs()
	{
	}

	public ParkAreaExtendArgs(string onlyID)
		: base(onlyID)
	{
	}
}
