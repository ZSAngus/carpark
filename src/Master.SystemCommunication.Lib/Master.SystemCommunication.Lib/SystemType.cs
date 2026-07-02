namespace Master.SystemCommunication.Lib;

/// <summary>
/// 系統類型
/// </summary>
public enum SystemType
{
	/// <summary>
	/// 未知系統類型
	/// </summary>
	Sys_Unknown = -1,
	/// <summary>
	/// 閘機控制系統
	/// </summary>
	Sys_Gate,
	/// <summary>
	/// 車場主控服務
	/// </summary>
	Sys_GateServer,
	/// <summary>
	/// 車牌系統
	/// </summary>
	Sys_LRPS,
	/// <summary>
	/// 收費系統
	/// </summary>
	Sys_Fee,
	/// <summary>
	/// 數據採集終端
	/// </summary>
	Sys_DateCollection,
	/// <summary>
	/// 車場雲系統
	/// </summary>
	Sys_CarparkCloud,
	/// <summary>
	/// 自助收費
	/// </summary>
	Sys_APS
}
