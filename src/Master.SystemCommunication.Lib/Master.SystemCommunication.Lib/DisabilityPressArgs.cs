using System.Runtime.Serialization;
using CarPark.Core;

namespace Master.SystemCommunication.Lib;

/// <summary>
/// 傷殘接口參數
/// </summary>
public class DisabilityPressArgs : ProgramBase
{
	[DataMember]
	public int GateID { get; set; }

	/// <summary>
	/// 按下類型
	/// </summary>
	[DataMember]
	public EnumParkType PressParkType { get; set; }

	/// <summary>
	/// 接收者ID
	/// </summary>
	[DataMember]
	public string ReceiveID { get; set; }

	/// <summary>
	/// 操作電腦
	/// </summary>
	[DataMember]
	public string OperationPC { get; set; }

	/// <summary>
	/// 操作用戶
	/// </summary>
	[DataMember]
	public string ShiffCode { get; set; }

	/// <summary>
	/// 出票類型
	/// </summary>
	[DataMember]
	public EnumParkType PrintParkType { get; set; }

	/// <summary>
	///             是否取消出票
	///             true不出票，清標誌位
	/// </summary>
	[DataMember]
	public bool IsCancel { get; set; }

	public DisabilityPressArgs()
	{
	}

	public DisabilityPressArgs(string onlyID)
		: base(onlyID)
	{
	}

	public void GateSet(int gateID, EnumParkType parkType, string receiveID)
	{
		GateID = gateID;
		PressParkType = parkType;
		ReceiveID = receiveID;
	}

	public void CashierSet(string operationPC, string shiffCode, EnumParkType printParkType)
	{
		OperationPC = operationPC;
		ShiffCode = shiffCode;
		PrintParkType = printParkType;
	}
}
