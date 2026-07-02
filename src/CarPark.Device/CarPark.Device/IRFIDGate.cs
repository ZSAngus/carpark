using System.Windows.Forms;

namespace CarPark.Device;

public interface IRFIDGate
{
	UserControl GetInnerControl { get; }

	void InitDevices();

	void ShowDeviceStates();

	void CloseRFID();
}
