namespace CarPark.Device;

public interface IHandContrast
{
	event RecordContrastEventHandler RecordContrastEvent;

	event ExitContrastEventHandler ExitContrastEvent;

	void AgainCamera(AgainCameraInfo againCameraInfo);
}
