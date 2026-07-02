using CarPark.Core;

namespace CarPark.Device;

public class ManualChangeInfo
{
	public string ShiffCode { get; set; }

	public string SystemName { get; set; }

	public int ParkAreaExtendID { get; set; }

	public EnumParkType parkType { get; set; }

	public bool ManualFull { get; set; }
}
