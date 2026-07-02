using System.Windows.Forms;

namespace CarPark.Device;

public interface ICamera : IHandContrast
{
	UserControl GetInnerControl { get; }

	event CameraImageEventHandler CameraImageEvent;

	void InitDevices();

	void InitUserControl(UserControl userControl);

	void InitUserControl(Control userControl);

	void CameraPhoto(int GateID);

	void Dispose();
}
