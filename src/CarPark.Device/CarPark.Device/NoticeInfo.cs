using CarPark.Core;

namespace CarPark.Device;

public class NoticeInfo
{
	public int GateID { get; set; }

	public string Content { get; set; }

	public EnumPassStatus PassStatus { get; set; }
}
