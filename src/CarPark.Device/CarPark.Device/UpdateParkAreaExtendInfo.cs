using CarPark.Core;

namespace CarPark.Device;

public class UpdateParkAreaExtendInfo
{
	public string ShiffCode { get; set; }

	public string SystemName { get; set; }

	public int ParkAreaExtendID { get; set; }

	public EnumParkType parkType { get; set; }

	public string NameCN { get; set; }

	public string NameEN { get; set; }

	public int TotalSupply { get; set; }

	public int TimeChargeUse { get; set; }

	public int ExtemdCount { get; set; }
}
