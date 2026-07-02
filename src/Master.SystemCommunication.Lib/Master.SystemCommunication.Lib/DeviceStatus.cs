using System;
using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 設備狀態傳輸
/// </summary>
public class DeviceStatus
{
	[DataMember]
	public int GateID { get; set; }

	[DataMember]
	public EnumDeviceType DeviceType { get; set; }

	/// <summary>
	/// 閃付，澳門通，月租按Unknown=-1, Failure=0, Normal=1, TimeOut=2
	/// Ticket按ACE代號解析
	/// 其餘按各自代號解析
	/// </summary>
	[DataMember]
	public string DeviceCode { get; set; }

	[DataMember]
	public DateTime UpdateTime { get; set; }
}
