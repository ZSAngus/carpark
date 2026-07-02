using CarPark.Core;

namespace CarPark.Device;

public class DisabilityPressInfo
{
	public string OnlyID { get; set; }

	public int GateID { get; set; }

	public EnumParkType PressParkType { get; set; }

	public string ReceiveID { get; set; }

	public string OperationPC { get; set; }

	public string ShiffCode { get; set; }

	public EnumParkType PrintParkType { get; set; }

	public bool IsCancel { get; set; }
}
