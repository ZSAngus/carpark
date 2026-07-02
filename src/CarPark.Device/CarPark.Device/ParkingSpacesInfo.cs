using CarPark.Core;

namespace CarPark.Device;

public class ParkingSpacesInfo
{
	public int AreaID { get; set; }

	public EnumParkType parkType { get; set; }

	public int CurrCount { get; set; }

	public bool ManualFull { get; set; }
}
