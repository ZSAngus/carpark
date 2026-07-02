using CarPark.Core;

namespace CarPark.Device;

public class DeviceStatusInfo
{
	public int GateID { get; set; }

	public EnumDeviceType DeviceType { get; set; }

	public string DeviceCode { get; set; }
}
