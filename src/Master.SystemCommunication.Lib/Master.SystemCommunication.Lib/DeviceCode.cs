namespace Master.SystemCommunication.Lib;

/// <summary>
/// 設備狀態
/// </summary>
public enum DeviceCode
{
	/// <summary>
	///             未知
	/// </summary>
	Unknown = -1,
	/// <summary>
	/// 失敗
	/// </summary>
	Failure,
	/// <summary>
	/// 正常
	/// </summary>
	Normal,
	/// <summary>
	/// 超時
	/// </summary>
	TimeOut
}
